using ScriperLib;
using System.Collections.Generic;

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
    }
}
