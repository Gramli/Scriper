using ReactiveUI;
using ScriperLib;
using ScriperLib.Configuration;
using System;
using System.Collections.Generic;
using System.Reactive;

namespace Scriper.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {
        private MainVM mainVM;
        public MainVM MainVM
        {
            get => mainVM;
            set
            {
                this.RaiseAndSetIfChanged(ref mainVM, value);
            }
        }

        private bool dataVisible = false;
        public bool DataVisible
        {
            get => dataVisible;
            set
            {
                this.RaiseAndSetIfChanged(ref dataVisible, value);
            }
        }

        public List<string> Configs { get; private set; }

        public ReactiveCommand<string, Unit> OkCmd { get; }

        private IScriperLibContainer _container;
        public MainWindowVM(List<string> configs)
        {
            OkCmd = ReactiveCommand.Create<string>(Ok);

            if (configs.Count < 2)
            {
                var config = configs.Count > 0 ? configs[0] : null;
                _container = new ScriperLibContainer(config);
                MainVM = new MainVM(_container);
                DataVisible = true;
            }
            else
            {
                DataVisible = false;
                Configs = configs;
            }
        }

        private void Ok(string config)
        {
            _container = new ScriperLibContainer(config);
            MainVM = new MainVM(_container);
            DataVisible = true;
        }

        public void SaveConfig()
        {
            if (_container != null)
            {
                _container.GetInstance<IScriperConfiguration>().Save();
            }
        }
    }
}
