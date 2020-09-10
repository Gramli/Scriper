using Avalonia;
using ReactiveUI;
using Scriper.Views;
using ScriperLib;
using ScriperLib.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using Scriper.Extensions;

namespace Scriper.ViewModels
{
    public class ScriptManagerVM : ViewModelBase
    {
        public IScriperLibContainer Container { get; private set; }
        public IReadOnlyCollection<IScript> Scripts => _scriptManager.Scripts;
        public ReactiveCommand<string, Unit> EditScriptCmd { get; }
        public ReactiveCommand<string, Unit> RunScriptCmd { get; }

        private IScriptManager _scriptManager;

        public ScriptManagerVM(IScriperLibContainer container)
        {
            Container = container;
            _scriptManager = container.GetInstance<IScriptManager>();
            EditScriptCmd = ReactiveCommand.Create<string>(EditScript);
            RunScriptCmd = ReactiveCommand.Create<string>(RunScript);
        }

        public void RunScript(string name)
        {
            var script = Get(name);
            _scriptManager.RunScript(script);
        }

        public void EditScript(string name)
        {
            var script = Get(name);

            var scriptViewModel = new ScriptVM((IScriptConfiguration)script.Configuration.Clone());
            var scriptControl = new ScriptVC(scriptViewModel);
            var dialogWindow = new DialogWindow(600,500,"Edit Script", scriptControl);

            scriptViewModel.Close += (sender, args) =>
            {
                if (args.Cancel)
                {
                    return;
                }

                Replace(script, args.Result);
                dialogWindow.Close();
            };

            dialogWindow.ShowDialog(App.Current.GetMainWindow());
        }

        public void CreateScript()
        {
            var script = Container.GetInstance<IScriptConfiguration>();

            var scriptViewModel = new ScriptVM(script);
            var scriptControl = new ScriptVC(scriptViewModel);
            var dialogWindow = new DialogWindow(600, 500, "Add Script", scriptControl);

            scriptViewModel.Close += (sender, args) =>
            {
                if (args.Cancel)
                {
                    return;
                }

                _scriptManager.AddScript(script);
                dialogWindow.Close();
            };

            dialogWindow.ShowDialog(App.Current.GetMainWindow());
        }

        public void Replace(IScript oldScript, IScriptConfiguration newScriptConfiguration)
        {
            _scriptManager.ReplaceScript(oldScript, newScriptConfiguration);
        }

        public IScript Get(string name)
        {
            return Scripts.Single(script => script.Configuration.Name == name);
        }
    }
}
