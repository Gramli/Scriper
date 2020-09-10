using ReactiveUI;
using Scriper.Extensions;
using ScriperLib;
using System.Reactive;

namespace Scriper.ViewModels
{
    public class MainVM : ViewModelBase
    {
        public ScriptManagerVM ScriptManagerVM { get; private set; }
        public ReactiveCommand<string, Unit> CreateScriptCmd { get; }

        public ReactiveCommand<Unit, Unit> ExitCmd { get; }

        private IScriperLibContainer _container;
        public MainVM(IScriperLibContainer container)
        {
            _container = container;
            ScriptManagerVM = new ScriptManagerVM(container);
            CreateScriptCmd = ReactiveCommand.Create<string>(CreateScript);
            ExitCmd = ReactiveCommand.Create(Exit);
        }

        public void Exit()
        {
            App.Current.Close();
        }

        public void CreateScript(string argument)
        {
            ScriptManagerVM.CreateScript();
        }
    }
}
