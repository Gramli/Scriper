using DynamicData;
using NLog;
using ReactiveUI;
using Scriper.Configuration;
using Scriper.Extensions;
using Scriper.Views;
using ScriperLib;
using ScriperLib.Configuration;
using ScriperLib.Extensions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;

namespace Scriper.ViewModels
{
    public class ScriptManagerVM : ViewModelBase
    {
        public IScriperLibContainer Container { get; private set; }
        public ObservableCollection<ScriptVM> Scripts { get; private set; }
        public ReactiveCommand<string, Unit> EditScriptCmd { get; }
        public ReactiveCommand<string, Unit> RunScriptCmd { get; }
        public ReactiveCommand<string, Unit> RemoveScriptCmd { get; }

        private readonly IScriptManager _scriptManager;

        private readonly IScriptRunner _scriptRunner;

        private readonly IScriperUIConfiguration _uiConfig;

        private static readonly Logger logger = NLogExtensions.LogFactory.GetCurrentClassLogger();

        public ScriptManagerVM(IScriperLibContainer container, IScriperUIConfiguration uiConfig)
        {
            Container = container;
            _scriptManager = container.GetInstance<IScriptManager>();
            _scriptRunner = container.GetInstance<IScriptRunner>();
            EditScriptCmd = ReactiveCommand.Create<string>(EditScript);
            RunScriptCmd = ReactiveCommand.Create<string>(RunScript);
            RemoveScriptCmd = ReactiveCommand.Create<string>(RemoveScript);
            _uiConfig = uiConfig;
            InitializeScripts();
        }

        private void InitializeScripts()
        {
            try
            {
                Scripts = new ObservableCollection<ScriptVM>();
                foreach (var script in _scriptManager.Scripts)
                {
                    var vm = new ScriptVM(script);
                    Scripts.Add(vm);
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
                    var dialogWindow = new DialogWindow(500, 500, script.Configuration.Name, outputVC, AssetsExtensions.GetAssetsIcon("icons8_console.ico"));
                    dialogWindow.Closed += (sender, args) => { script.Outputs.Remove(outputVM); };
                    dialogWindow.Show();
                }
                _scriptRunner.RunAsync(script);
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
                var dialogWindow = new DialogWindow(600, 550, "Edit Script", scriptControl, AssetsExtensions.GetAssetsIcon("icons8_edit_property.ico"));

                scriptViewModel.Close += (sender, args) =>
                {
                    if (!args.Cancel)
                    {
                        var oldVM = Scripts.Single(item => item.ScriptConfiguration.Name == script.Configuration.Name);
                        Scripts.Replace(oldVM, new ScriptVM(args.Result));
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
                var dialogWindow = new DialogWindow(600, 550, "Add Script", scriptControl, AssetsExtensions.GetAssetsIcon("icons8_file_1.ico"));

                scriptViewModel.Close += (sender, args) =>
                {
                    if (Scripts.Any(item => item.ScriptConfiguration.Name == args.Result.Configuration.Name))
                    {
                        scriptViewModel.InvalidName("Invalid script name, script name already exists.");
                        return;
                    }
                    if (!args.Cancel)
                    {
                        _scriptManager.AddScript(args.Result);
                        Scripts.Add(new ScriptVM(args.Result));
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

        private ScriptVM GetScriptVM(string name)
        {
            return Scripts.Single(script => script.ScriptConfiguration.Name == name);
        }
    }
}
