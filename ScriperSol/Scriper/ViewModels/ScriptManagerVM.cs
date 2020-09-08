using Scriper.Views;
using ScriperLib;
using ScriperLib.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Scriper.ViewModels
{
    public class ScriptManagerVM : ViewModelBase
    {
        public IScriperLibContainer Container { get; private set; }
        public IReadOnlyCollection<IScript> Scripts => _scriptManager.Scripts;

        private IScriptManager _scriptManager;
        public ScriptManagerVM(IScriperLibContainer container)
        {
            Container = container;
            _scriptManager = container.GetInstance<IScriptManager>();
        }

        public void Run(string name)
        {
            var script = Get(name);
            _scriptManager.RunScript(script);
        }

        public void EditScript(string name)
        {
            var script = Get(name);

            var scriptViewModel = new ScriptVM((IScriptConfiguration)script.Configuration.Clone());
            var scriptControl = new ScriptVC(scriptViewModel);
            var dialogWindow = new DialogWindow(scriptControl);

            scriptViewModel.Close += (sender, args) =>
            {
                if (args.Cancel)
                {
                    return;
                }

                Replace(script, args.Result);

            };

            dialogWindow.Show();
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
