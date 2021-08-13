using Avalonia.Collections;
using DynamicData;
using NLog;
using ReactiveUI;
using Scriper.Converters;
using Scriper.Extensions;
using Scriper.Models;
using Scriper.SystemTray;
using Scriper.TimeSchedule;
using Scriper.ViewModels.Validation;
using Scriper.Views;
using ScriperLib;
using ScriperLib.Configuration;
using ScriperLib.Extensions;
using System;
using System.Linq;
using System.Reactive;

namespace Scriper.ViewModels
{
    public class ScriptManagerVM : ViewModelBase, IScriptManagerVM
    {
        public AvaloniaList<ScriptVM> Scripts { get; private set; }
        public ReactiveCommand<string, Unit> EditScriptCmd { get; }
        public ReactiveCommand<string, Unit> RunScriptCmd { get; }
        public ReactiveCommand<string, Unit> RemoveScriptCmd { get; }
        public ReactiveCommand<string, Unit> EditScriptContentCmd { get; }

        private readonly IScriptManager _scriptManager;
        private readonly IScriptRunner _scriptRunner;
        private readonly ISystemTrayMenu _systemTrayMenu;
        private readonly IScriptSchedulerManagerAdapter _schedulerManagerAdapter;
        private readonly IOpenEditorScriptCreator _openEditorScriptCreator;
        private readonly IScriptConfigurationFactory _scriptConfigurationCreator;
        private readonly Func<IScriptConfiguration, IAddEditScriptVM> _createAddEditScriptVM;
        private readonly ScriptTypeToAssetNameConverter _scriptTypeToAssetNameConverter = new ScriptTypeToAssetNameConverter(); //TODO THIS

        private static readonly Logger _logger = NLogFactoryProxy.Instance.GetLogger();

        public ScriptManagerVM(IScriptManager scriptManager,
            IScriptRunner scriptRunner,
            IScriptConfigurationFactory scriptConfigurationCreator,
            ISystemTrayMenu systemTrayMenu,
            IScriptSchedulerManagerAdapter schedulerManagerAdapter,
            IOpenEditorScriptCreator openEditorScriptCreator,
            Func<IScriptConfiguration, IAddEditScriptVM> createAddEditScriptVM)
        {
            _scriptManager = scriptManager;
            _scriptRunner = scriptRunner;
            EditScriptCmd = ReactiveCommand.Create<string>(EditScript).CatchError(_logger);
            RunScriptCmd = ReactiveCommand.Create<string>(RunScript).CatchError(_logger);
            RemoveScriptCmd = ReactiveCommand.Create<string>(RemoveScript).CatchError(_logger);
            EditScriptContentCmd = ReactiveCommand.Create<string>(EditScriptContent).CatchError(_logger);
            _systemTrayMenu = systemTrayMenu;
            _schedulerManagerAdapter = schedulerManagerAdapter;
            _openEditorScriptCreator = openEditorScriptCreator;
            _scriptConfigurationCreator = scriptConfigurationCreator;
            _createAddEditScriptVM = createAddEditScriptVM;
            InitializeScripts();
        }

        public void CreateScript()
        {
            try
            {
                var scriptConfiguration = _scriptConfigurationCreator.CreateEmptyScriptConfiguration();
                var scriptViewModel = _createAddEditScriptVM(scriptConfiguration);
                var scriptControl = new ScriptVC(scriptViewModel);
                var dialogWindow = DialogWindowExtensions.CreateAddScriptDialogWindow(scriptControl);

                scriptViewModel.Close += (sender, args) =>
                {
                    if (!args.Cancel)
                    {
                        if (Scripts.Any(item => item.ScriptConfiguration.Name == args.Result.Configuration.Name))
                        {
                            scriptViewModel.InvalidName("Invalid script name, script name already exists.");
                            return;
                        }
                        _scriptManager.AddScript(args.Result);
                        _schedulerManagerAdapter.Replace(args.Result.Configuration);
                        var newScriptVM = new ScriptVM(args.Result);  //TODO THIS
                        Scripts.Add(newScriptVM);
                        EditContextMenuByInSystemTray(newScriptVM.Script);
                    }

                    dialogWindow.Close();
                };

                dialogWindow.ShowDialog(App.Current.GetMainWindow());
            }
            catch (Exception ex)
            {
                MessageBoxExtensions.ShowDialog(ex.Message);
                _logger.Error(ex);
            }
        }

        public void MoveScriptUp(string name)
        {
            var scriptVM = Scripts.Single(x => x.ScriptConfiguration.Name == name);
            var index = Scripts.IndexOf(scriptVM);
            if (index > 0)
            {
                var newIndex = index - 1;
                //move doesnt work in UI
                //Scripts.Move(index, newOrder);
                Scripts.Remove(scriptVM);
                Scripts.Insert(newIndex, scriptVM);
                scriptVM.ScriptConfiguration.Order = newIndex;
                Scripts[index].ScriptConfiguration.Order = index;
            }
        }

        private void InitializeScripts()
        {
            try
            {
                Scripts = new AvaloniaList<ScriptVM>();
                foreach (var script in _scriptManager.Scripts)
                {
                    var vm = new ScriptVM(script);
                    Scripts.Add(vm);
                    EditContextMenuByInSystemTray(script);
                }
            }
            catch (Exception ex)
            {
                MessageBoxExtensions.ShowDialog(ex.Message);
                _logger.Error(ex);
            }
        }

        private void RunScript(string name)
        {
            try
            {
                var scriptVM = GetScriptVM(name);
                var script = scriptVM.Script;

                if (scriptVM.ScriptConfiguration.OutputWindow)
                {
                    var outputVM = new OutputVM();  //TODO THIS
                    var outputVC = new OutputVC(outputVM);
                    script.Outputs.Add(outputVM);
                    var dialogWindow = DialogWindowExtensions.CreateRunScriptDialogWindow(script.Configuration.Name, outputVC); 
                    dialogWindow.Closed += (sender, args) => { script.Outputs.Remove(outputVM); };
                    dialogWindow.Show();
                }
                RunScript(script);
                scriptVM.LastRun = DateTime.Now.ToString();
            }
            catch (Exception ex)
            {
                MessageBoxExtensions.ShowDialog(ex.Message);
                _logger.Error(ex);
            }
        }

        private void EditScript(string name)
        {
            try
            {
                var script = GetScriptVM(name).Script;
                var scriptViewModel = _createAddEditScriptVM(script.Configuration.DeepClone());
                var scriptControl = new ScriptVC(scriptViewModel);
                var dialogWindow = DialogWindowExtensions.CreateEditScriptDialogWindow(scriptControl);

                scriptViewModel.Close += (sender, args) =>
                {
                    if (!args.Cancel)
                    {
                        var oldScriptVM = Scripts.Single(item => item.ScriptConfiguration.Name == script.Configuration.Name);
                        var newScriptVM = new ScriptVM(args.Result);
                        TryRemoveFromContextMenu(script);
                        EditContextMenuByInSystemTray(newScriptVM.Script);
                        Scripts.Replace(oldScriptVM, newScriptVM);
                        _schedulerManagerAdapter.Replace(args.Result.Configuration);
                        _scriptManager.ReplaceScript(script, args.Result);
                    }

                    dialogWindow.Close();
                };

                dialogWindow.ShowDialog(App.Current.GetMainWindow());
            }
            catch (Exception ex)
            {
                MessageBoxExtensions.ShowDialog(ex.Message);
                _logger.Error(ex);
            }
        }

        private void RemoveScript(string name)
        {
            try
            {
                var scriptVM = GetScriptVM(name);
                Scripts.Remove(scriptVM);
                EditContextMenuByInSystemTray(scriptVM.Script);
                _scriptManager.RemoveScript(scriptVM.Script);
                _schedulerManagerAdapter.Remove(scriptVM.ScriptConfiguration.Name);
            }
            catch (Exception ex)
            {
                MessageBoxExtensions.ShowDialog(ex.Message);
                _logger.Error(ex);
            }
        }

        private void EditScriptContent(string name)
        {
            try
            {
                if (_openEditorScriptCreator.IsTextEditorSet())
                {
                    MessageBoxExtensions.ShowDialog("Text editor is not set. If you want to edit scripts from Scriper set path to your text editor in settings.");
                    return;
                }

                var scriptVM = GetScriptVM(name);
                var scriptToRun = _openEditorScriptCreator.CreateOpenScriptEditorScript(scriptVM.ScriptConfiguration.Path);
                _scriptRunner.Run(scriptToRun);
            }
            catch (Exception ex)
            {
                MessageBoxExtensions.ShowDialog(ex.Message);
                _logger.Error(ex);
            }
        }

        private async void RunScript(IScript script)
        {
            try
            {
                await _scriptRunner.RunAsync(script);
            }
            catch (Exception ex)
            {
                MessageBoxExtensions.ShowDialog(ex.Message);
                _logger.Error(ex);
            }
        }

        private ScriptVM GetScriptVM(string name)
        {
            return Scripts.Single(script => script.ScriptConfiguration.Name == name);
        }

        private void EditContextMenuByInSystemTray(IScript script)
        {
            if (script.Configuration.InSystemTray)
            {
                var imageName = _scriptTypeToAssetNameConverter.Convert(script.ScriptType);
                _systemTrayMenu?.TryInsertClickContextMenuItem(script.Configuration.Name, RunScript, imageName);
            }
            else
            {
                TryRemoveFromContextMenu(script);
            }
        }

        private void TryRemoveFromContextMenu(IScript script)
        {
            _systemTrayMenu?.TryRemoveContextMenuItem(script.Configuration.Name);
        }
    }
}
