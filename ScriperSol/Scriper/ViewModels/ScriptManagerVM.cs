using Avalonia.Collections;
using DynamicData;
using NLog;
using ReactiveUI;
using Scriper.Configuration;
using Scriper.Extensions;
using Scriper.SystemTray;
using Scriper.Views;
using ScriperLib;
using ScriperLib.Configuration;
using ScriperLib.Extensions;
using System;
using System.Linq;
using System.Reactive;
using Scriper.AssetsAccess;

namespace Scriper.ViewModels
{
    public class ScriptManagerVM : ViewModelBase
    {
        public IScriperLibContainer Container { get; }
        public AvaloniaList<ScriptVM> Scripts { get; private set; }
        public ReactiveCommand<string, Unit> EditScriptCmd { get; }
        public ReactiveCommand<string, Unit> RunScriptCmd { get; }
        public ReactiveCommand<string, Unit> RemoveScriptCmd { get; }
        public ReactiveCommand<string, Unit> EditScriptContentCmd { get; }

        private AvaloniaAssets AvaloniaAssets => AvaloniaAssets.Instance;

        private readonly IScriptManager _scriptManager;
        private readonly IScriptRunner _scriptRunner;
        private readonly IScriperUIConfiguration _uiConfig;
        private readonly ISystemTrayMenu _systemTrayMenu;
        private readonly string _openScriptEditorScript = "OpenScriptEditorScript";

        private static readonly Logger logger = NLogExtensions.LogFactory.GetCurrentClassLogger();

        public ScriptManagerVM(IScriperLibContainer container, IScriperUIConfiguration uiConfig, ISystemTrayMenu systemTrayMenu)
        {
            Container = container;
            _scriptManager = container.GetInstance<IScriptManager>();
            _scriptRunner = container.GetInstance<IScriptRunner>();
            EditScriptCmd = ReactiveCommand.Create<string>(EditScript);
            RunScriptCmd = ReactiveCommand.Create<string>(RunScript);
            RemoveScriptCmd = ReactiveCommand.Create<string>(RemoveScript);
            EditScriptContentCmd = ReactiveCommand.Create<string>(EditScriptContent);
            _systemTrayMenu = systemTrayMenu;
            _uiConfig = uiConfig;
            InitializeScripts();
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
                MessageBoxExtensions.Show(ex.Message);
                logger.Error(ex);
            }
        }

        public void RunScript(string name)
        {
            try
            {
                var scriptVM = GetScriptVM(name);
                var script = scriptVM.Script;

                if (scriptVM.ScriptConfiguration.OutputWindow)
                {
                    var outputVM = new OutputVM();
                    var outputVC = new OutputVC(outputVM);
                    script.Outputs.Add(outputVM);
                    var dialogWindow = new DialogWindow(500, 500, script.Configuration.Name, outputVC, AvaloniaAssets.GetAssetsIcon("icons8_console.ico"));
                    dialogWindow.Closed += (sender, args) => { script.Outputs.Remove(outputVM); };
                    dialogWindow.Show();
                }
                RunScript(script);
                scriptVM.LastRun = DateTime.Now.ToString();
            }
            catch (Exception ex)
            {
                MessageBoxExtensions.Show(ex.Message);
                logger.Error(ex);
            }
        }

        public void EditScript(string name)
        {
            try
            {
                var script = GetScriptVM(name).Script;
                var scriptViewModel = new AddEditScriptVM(Container, script.Configuration.DeepClone());
                var scriptControl = new ScriptVC(scriptViewModel);
                var dialogWindow = new DialogWindow(600, 525, "Edit Script", scriptControl, AvaloniaAssets.GetAssetsIcon("icons8_edit_property.ico"));

                scriptViewModel.Close += (sender, args) =>
                {
                    if (!args.Cancel)
                    {
                        var oldScriptVM = Scripts.Single(item => item.ScriptConfiguration.Name == script.Configuration.Name);
                        var newScriptVM = new ScriptVM(args.Result);
                        EditContextMenuByInSystemTray(oldScriptVM.Script);
                        EditContextMenuByInSystemTray(newScriptVM.Script);
                        Scripts.Replace(oldScriptVM, newScriptVM);
                        _scriptManager.ReplaceScript(script, args.Result);
                    }

                    dialogWindow.Close();
                };

                dialogWindow.ShowDialog(App.Current.GetMainWindow());
            }
            catch (Exception ex)
            {
                MessageBoxExtensions.Show(ex.Message);
                logger.Error(ex);
            }
        }

        public void RemoveScript(string name)
        {
            try
            {
                var scriptVM = GetScriptVM(name);
                Scripts.Remove(scriptVM);
                EditContextMenuByInSystemTray(scriptVM.Script);
                _scriptManager.RemoveScript(scriptVM.Script);
            }
            catch (Exception ex)
            {
                MessageBoxExtensions.Show(ex.Message);
                logger.Error(ex);
            }
        }

        public void CreateScript()
        {
            try
            {
                var scriptConfiguration = Container.GetInstance<IScriptConfiguration>();
                var scriptViewModel = new AddEditScriptVM(Container, scriptConfiguration);
                var scriptControl = new ScriptVC(scriptViewModel);
                var dialogWindow = new DialogWindow(600, 525, "Add Script", scriptControl, AvaloniaAssets.GetAssetsIcon("icons8_file_1.ico"));

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
                        var newScriptVM = new ScriptVM(args.Result);
                        Scripts.Add(newScriptVM);
                        EditContextMenuByInSystemTray(newScriptVM.Script);
                    }

                    dialogWindow.Close();
                };

                dialogWindow.ShowDialog(App.Current.GetMainWindow());
            }
            catch (Exception ex)
            {
                MessageBoxExtensions.Show(ex.Message);
                logger.Error(ex);
            }
        }

        public void EditScriptContent(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(_uiConfig.TextEditor.Path))
                {
                    MessageBoxExtensions.Show("Text editor is not set. If you want to edit scripts from Scriper set path to your text editor in settings.");
                    return;
                }

                var scriptVM = GetScriptVM(name);
                var scriptToRun = CreateOpenScriptEditorScript(scriptVM, _uiConfig);
                _scriptRunner.Run(scriptToRun);
            }
            catch (Exception ex)
            {
                MessageBoxExtensions.Show(ex.Message);
                logger.Error(ex);
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

        private async void RunScript(IScript script)
        {
            try
            {
                await _scriptRunner.RunAsync(script);
            }
            catch (Exception ex)
            {
                MessageBoxExtensions.Show(ex.Message);
                logger.Error(ex);
            }
        }

        private IScript CreateOpenScriptEditorScript(ScriptVM scriptVM, IScriperUIConfiguration config)
        {
            var newScriptConfig = Container.GetInstance<IScriptConfiguration>();
            newScriptConfig.Name = _openScriptEditorScript;
            newScriptConfig.Arguments = scriptVM.ScriptConfiguration.Path;
            newScriptConfig.Path = config.TextEditor.Path;
            return Container.GetInstance<IScriptCreator>().Create(newScriptConfig);
        }

        private ScriptVM GetScriptVM(string name)
        {
            return Scripts.Single(script => script.ScriptConfiguration.Name == name);
        }

        private void EditContextMenuByInSystemTray(IScript script)
        {
            if (script.Configuration.InSystemTray)
            {
                _systemTrayMenu?.TryAddContextMenuItem(script.Configuration.Name, RunScript);
            }
            else
            {
                _systemTrayMenu?.RemoveContextMenuItem(script.Configuration.Name);
            }
        }
    }
}
