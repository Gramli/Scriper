using DynamicData;
using ReactiveUI;
using Scriper.Extensions;
using Scriper.Views;
using ScriperLib;
using ScriperLib.Configuration;
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
            Scripts = new ObservableCollection<ScriptVM>();
            foreach(var script in _scriptManager.Scripts)
            {
                var vm = new ScriptVM(Container, script);
                Scripts.Add(vm);
            }
        }

        public void RunScript(string name)
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

        public void EditScript(string name)
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

        public void RemoveScript(string name)
        {
            var scriptVM = Get(name);
            Scripts.Remove(scriptVM);
            _scriptManager.RemoveScript(scriptVM.GetScript());
        }

        public void CreateScript()
        {
            var scriptConfiguration = Container.GetInstance<IScriptConfiguration>();
            var scriptViewModel = new ScriptVM(Container, _scriptManager.CreateScript, scriptConfiguration);
            var scriptControl = new ScriptVC(scriptViewModel);
            var dialogWindow = new DialogWindow(600, 550, "Add Script", scriptControl, AssetsExtensions.GetAssetsIcon("icons8_file_1.ico"));

            scriptViewModel.Close += (sender, args) =>
            {
                if (!args.Cancel)
                {
                    _scriptManager.AddScript(args.Result);
                    Scripts.Add(scriptViewModel);
                }

                dialogWindow.Close();
            };

            dialogWindow.ShowDialog(App.Current.GetMainWindow());
        }

        public void Replace(IScript oldScript, IScript newScript)
        {
            _scriptManager.ReplaceScript(oldScript, newScript);
        }

        public ScriptVM Get(string name)
        {
            return Scripts.Single(script => script.Name == name);
        }
    }
}
