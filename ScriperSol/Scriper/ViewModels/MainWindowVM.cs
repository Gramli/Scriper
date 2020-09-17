using ScriperLib;
using ScriperLib.Configuration;

namespace Scriper.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {
        public MainVM MainVM { get; private set; }

        private IScriperLibContainer _container;
        public MainWindowVM(string config)
        {
            _container = new ScriperLibContainer(config);
            MainVM = new MainVM(_container);
        }

        public void SaveConfig()
        {
            _container.GetInstance<IScriperConfiguration>().Save();
        }
    }
}
