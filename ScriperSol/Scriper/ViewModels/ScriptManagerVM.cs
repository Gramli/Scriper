using DynamicData;
using NLog;
using ReactiveUI;
using Scriper.Extensions;
using Scriper.Views;
using ScriperLib;
using ScriperLib.Configuration;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;

namespace Scriper.ViewModels
{
    public class ScriptManagerVM : ViewModelBase
    {
        public IScriperLibContainer Container { get; private set; }
        public ObservableCollection<ScriptVM> Scripts { get; private set; }
        public ReactiveCommand<string, Unit> EditScriptCmd { get; }
        public ReactiveCommand<string, Unit> RunScriptCmd { get; }
        public ReactiveCommand<string, Unit> RemoveScriptCmd { get; }

        private IScriptManager _scriptManager;

        private static readonly Logger logger = NLogExtensions.LogFactory.GetCurrentClassLogger();

        public ScriptManagerVM(IScriperLibContainer container)
        {
            Container = container;
            _scriptManager = container.GetInstance<IScriptManager>();
            EditScriptCmd = ReactiveCommand.Create<string>(EditScript);
            RunScriptCmd = ReactiveCommand.Create<string>(RunScript);
            RemoveScriptCmd = ReactiveCommand.Create<string>(RemoveScript);
            InitializeScripts();
        }

        private void InitializeScripts()
        {
            try
            {
                Scripts = new ObservableCollection<ScriptVM>();
                foreach (var script in _scriptManager.Scripts)
                {
                    var vm = new ScriptVM(Container, script);
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
                var scriptVM = Get(name);
                var script = scriptVM.GetScript();

                if (scriptVM.OutputWindow)
                {
                    var outputVM = new OutputVM();
                    var outputVC = new OutputVC(outputVM);
                    script.Outputs.Add(outputVM);
                    var dialogWindow = new DialogWindow(500, 500, script.Configuration.Name, outputVC, AssetsExtensions.GetAssetsIcon("icons8_console.ico"));
                    dialogWindow.Closed += (sender, args) => { script.Outputs.Remove(outputVM); };
                    dialogWindow.Show();
                }
                Task.Factory.StartNew(() => _scriptManager.RunScript(script));
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
                var script = Get(name).GetScript();

                var scriptViewModel = new ScriptVM(Container, (IScript)script.Clone());
                var scriptControl = new ScriptVC(scriptViewModel);
                var dialogWindow = new DialogWindow(600, 550, "Edit Script", scriptControl, AssetsExtensions.GetAssetsIcon("icons8_edit_property.ico"));

                scriptViewModel.Close += (sender, args) =>
                {
                    if (!args.Cancel)
                    {
                        Replace(script, args.Result);
                        var oldVM = Scripts.Single(item => item.Name == script.Configuration.Name);
                        Scripts.Replace(oldVM, scriptViewModel);
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
                var scriptVM = Get(name);
                Scripts.Remove(scriptVM);
                _scriptManager.RemoveScript(scriptVM.GetScript());
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
                var scriptViewModel = new ScriptVM(Container, _scriptManager.CreateScript, scriptConfiguration);
                var scriptControl = new ScriptVC(scriptViewModel);
                var dialogWindow = new DialogWindow(600, 550, "Add Script", scriptControl, AssetsExtensions.GetAssetsIcon("icons8_file_1.ico"));

                scriptViewModel.Close += (sender, args) =>
                {
                    if (Scripts.Any(item => item.Name == args.Result.Configuration.Name))
                    {
                        scriptViewModel.InvalidName("Invalid script name, script name already exists.");
                        return;
                    }
                    if (!args.Cancel)
                    {
                        _scriptManager.AddScript(args.Result);
                        Scripts.Add(scriptViewModel);
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

        public void Replace(IScript oldScript, IScript newScript)
        {
            try
            {
                _scriptManager.ReplaceScript(oldScript, newScript);
            }
            catch (Exception ex)
            {
                MessageBoxExtensions.Show(ex.Message);
                logger.Error(ex);
            }
        }

        public ScriptVM Get(string name)
        {
            return Scripts.Single(script => script.Name == name);
        }
    }
}
