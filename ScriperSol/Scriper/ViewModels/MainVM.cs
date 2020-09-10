using ReactiveUI;
using ScriperLib;
using System.Reactive;

namespace Scriper.ViewModels
{
    public class MainVM : ViewModelBase
    {
        public ScriptManagerVM ScriptManagerVM { get; private set; }
        public ReactiveCommand<string, Unit> CreateScriptCmd { get; }

        private IScriperLibContainer _container;
        public MainVM(IScriperLibContainer container)
        {
            _container = container;
            ScriptManagerVM = new ScriptManagerVM(container);
            CreateScriptCmd = ReactiveCommand.Create<string>(CreateScript);
        }

        public void CreateScript(string argument)
        {
            ScriptManagerVM.CreateScript();
        }
    }
}
