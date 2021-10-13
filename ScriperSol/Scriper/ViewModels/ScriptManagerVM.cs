using Avalonia.Collections;
using Avalonia.Media.Imaging;
using DynamicData;
using NLog;
using ReactiveUI;
using Scriper.AssetsAccess;
using Scriper.Converters;
using Scriper.CustomScripts;
using Scriper.Dialogs;
using Scriper.Extensions;
using Scriper.SystemTray;
using Scriper.TimeSchedule;
using Scriper.ViewModels.Script;
using Scriper.Views;
using ScriperLib;
using ScriperLib.Clone;
using ScriperLib.Configuration;
using ScriperLib.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Reactive;

namespace Scriper.ViewModels
{
    public class ScriptManagerVM : ViewModelBase, IScriptManagerVM
    {
        public AvaloniaList<IScriptVM> Scripts { get; private set; }
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
        private readonly Func<IOutputVM> _createOutputVM;
        private readonly Func<IScript, IBitmap, IScriptVM> _createScriptVM;
        private readonly IAssets _assets;
        private readonly IScriptToImageConverter _scriptToImageConverter;
        private readonly IScriperFileDialogOpener _scriperFileDialogOpener;
        private readonly IScriptFactory _scriptCreator;
        private readonly IDeepCloneAdapter _deepCloneAdapter;

        private static readonly Logger _logger = NLogFactoryProxy.Instance.GetLogger();

        public ScriptManagerVM(IScriptManager scriptManager,
            IScriptRunner scriptRunner,
            IScriptConfigurationFactory scriptConfigurationCreator,
            ISystemTrayMenu systemTrayMenu,
            IScriptSchedulerManagerAdapter schedulerManagerAdapter,
            IOpenEditorScriptCreator openEditorScriptCreator,
            Func<IScriptConfiguration, IAddEditScriptVM> createAddEditScriptVM,
            Func<IOutputVM> createOutputVM,
            Func<IScript, IBitmap, IScriptVM> createScriptVM,
            IAssets assets,
            IScriptToImageConverter scriptToImageConverter,
            IScriperFileDialogOpener scriperFileDialogOpener,
            IScriptFactory scriptCreator,
            IDeepCloneAdapter deepCloneAdapter)
        {
            _scriptManager = scriptManager;
            _scriptRunner = scriptRunner;
            _assets = assets;
            EditScriptCmd = ReactiveCommand.Create<string>(EditScript).CatchError(_logger);
            RunScriptCmd = ReactiveCommand.Create<string>(RunScript).CatchError(_logger);
            RemoveScriptCmd = ReactiveCommand.Create<string>(RemoveScript).CatchError(_logger);
            EditScriptContentCmd = ReactiveCommand.Create<string>(EditScriptContent).CatchError(_logger);
            _systemTrayMenu = systemTrayMenu;
            _schedulerManagerAdapter = schedulerManagerAdapter;
            _openEditorScriptCreator = openEditorScriptCreator;
            _scriptConfigurationCreator = scriptConfigurationCreator;
            _createAddEditScriptVM = createAddEditScriptVM;
            _scriptToImageConverter = scriptToImageConverter;
            _createOutputVM = createOutputVM;
            _createScriptVM = createScriptVM;
            _scriperFileDialogOpener = scriperFileDialogOpener;
            _scriptCreator = scriptCreator;
            _deepCloneAdapter = deepCloneAdapter;
        }

        public void Init()
        {
            InitializeScripts();
        }

        public async void FastCreateScript()
        {
            var result = await _scriperFileDialogOpener.OpenScriptFileDialogAsync();
            if (result.ok)
            {
                var scriptConfiguration = _scriptConfigurationCreator.CreateEmptyScriptConfiguration();
                scriptConfiguration.Name = Path.GetFileNameWithoutExtension(result.file);
                scriptConfiguration.Path = result.file;

                if (Scripts.Any(item => item.ScriptConfiguration.Name == scriptConfiguration.Name))
                {
                    MessageBoxExtensions.ShowDialog("Invalid script name, script name already exists.");
                    return;
                }

                var script = _scriptCreator.Create(scriptConfiguration);
                CreateScript(script);
            }
        }

        public void CreateScript()
        {
            try
            {
                var scriptConfiguration = _scriptConfigurationCreator.CreateEmptyScriptConfiguration();
                var scriptViewModel = _createAddEditScriptVM(scriptConfiguration);
                var scriptControl = new ScriptVC(scriptViewModel);
                var dialogWindow = DialogWindowExtensions.CreateAddScriptDialogWindow(scriptControl, _assets);

                scriptViewModel.Close += (sender, args) =>
                {
                    if (!args.Cancel)
                    {
                        if (Scripts.Any(item => item.ScriptConfiguration.Name == args.Result.Configuration.Name))
                        {
                            scriptViewModel.InvalidName("Invalid script name, script name already exists.");
                            return;
                        }
                        CreateScript(args.Result);
                    }

                    dialogWindow.Close();
                };

                dialogWindow.ShowDialog(App.Current.GetMainWindow());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                MessageBoxExtensions.ShowDialog(ex.Message);
            }
        }

        private void CreateScript(IScript script)
        {
            _scriptManager.AddScript(script);
            _schedulerManagerAdapter.Replace(script.Configuration);
            var scriptImage = _scriptToImageConverter.Convert(script);
            var newScriptVM = _createScriptVM(script, scriptImage);
            Scripts.Add(newScriptVM);
            EditContextMenuByInSystemTray(newScriptVM.Script);
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
                Scripts = new AvaloniaList<IScriptVM>();
                foreach (var script in _scriptManager.Scripts)
                {
                    var scriptImage = _scriptToImageConverter.Convert(script);
                    var vm = _createScriptVM(script, scriptImage);
                    Scripts.Add(vm);
                    EditContextMenuByInSystemTray(script);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                MessageBoxExtensions.ShowDialog(ex.Message);
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
                    var outputVM = _createOutputVM();
                    var outputVC = new OutputVC(outputVM);
                    script.Outputs.Add(outputVM);
                    var dialogWindow = DialogWindowExtensions.CreateRunScriptDialogWindow(script.Configuration.Name, outputVC, _assets); 
                    dialogWindow.Closed += (sender, args) => { script.Outputs.Remove(outputVM); };
                    dialogWindow.Show();
                }
                RunScript(script);
                scriptVM.LastRun = DateTime.Now.ToString();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                MessageBoxExtensions.ShowDialog(ex.Message);
            }
        }

        private void EditScript(string name)
        {
            try
            {
                var script = GetScriptVM(name).Script;
                var newScriptConfig = _deepCloneAdapter.DeepClone(script.Configuration);
                var scriptViewModel = _createAddEditScriptVM(newScriptConfig);
                var scriptControl = new ScriptVC(scriptViewModel);
                var dialogWindow = DialogWindowExtensions.CreateEditScriptDialogWindow(scriptControl, _assets);

                scriptViewModel.Close += (sender, args) =>
                {
                    if (!args.Cancel)
                    {
                        var oldScriptVM = Scripts.Single(item => item.ScriptConfiguration.Name == script.Configuration.Name);
                        var newScriptImage = _scriptToImageConverter.Convert(args.Result);
                        var newScriptVM = _createScriptVM(args.Result, newScriptImage);
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
                _logger.Error(ex);
                MessageBoxExtensions.ShowDialog(ex.Message);
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
                _logger.Error(ex);
                MessageBoxExtensions.ShowDialog(ex.Message);
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
                _logger.Error(ex);
                MessageBoxExtensions.ShowDialog(ex.Message);
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
                _logger.Error(ex);
                MessageBoxExtensions.ShowDialog(ex.Message);
            }
        }

        private IScriptVM GetScriptVM(string name)
        {
            return Scripts.Single(script => script.ScriptConfiguration.Name == name);
        }

        private void EditContextMenuByInSystemTray(IScript script)
        {
            if (script.Configuration.InSystemTray)
            {
                var newScriptImage = _scriptToImageConverter.GetImagePath(script);
                _systemTrayMenu?.TryInsertClickContextMenuItem(script.Configuration.Name, RunScript, newScriptImage);
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
