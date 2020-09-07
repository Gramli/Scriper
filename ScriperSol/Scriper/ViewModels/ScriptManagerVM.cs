using ScriperLib;
using System.Collections.Generic;
using System.Linq;

namespace Scriper.ViewModels
{
    public class ScriptManagerVM : ViewModelBase
    {
        public IReadOnlyCollection<IScript> Scripts => _scriptManager.Scripts;

        private IScriptManager _scriptManager;
        public ScriptManagerVM(IScriperLibContainer container)
        {
            _scriptManager = container.GetInstance<IScriptManager>();
        }

        public void Run(string name)
        {
            var script = Scripts.Single(script => script.Configuration.Name == name);
            _scriptManager.RunScript(script);
        }
    }
}
