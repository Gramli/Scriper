using ScriperLib;

namespace Scriper.ViewModels
{
    public class MainVM : ViewModelBase
    {
        public ScriptManagerVM ScriptManagerVM { get; private set; }

        private IScriperLibContainer _container;
        public MainVM(IScriperLibContainer container)
        {
            _container = container;
            ScriptManagerVM = new ScriptManagerVM(container);
        }
    }
}
