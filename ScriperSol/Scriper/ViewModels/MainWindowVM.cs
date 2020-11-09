using NLog;
using ReactiveUI;
using Scriper.Configuration;
using Scriper.Extensions;
using ScriperLib;
using ScriperLib.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
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

        private string title;
        public string Title
        {
            get => title;
            set
            {
                this.RaiseAndSetIfChanged(ref title, $"Scriper: {Path.GetFileName(value)}");
            }
        }

        public List<string> Configs { get; private set; }

        public ReactiveCommand<string, Unit> OkCmd { get; }

        private static readonly Logger logger = NLogExtensions.LogFactory.GetCurrentClassLogger();

        private IScriperLibContainer _container;

        private IScriperUIConfiguration _uiConfig;

        private readonly string _uiConfigPath;

        private string _scriperConfigPath;
        public MainWindowVM(List<string> configs, string uiConfigPath)
        {
            OkCmd = ReactiveCommand.Create<string>(Ok);
            _uiConfigPath = uiConfigPath;

            if (configs.Count < 2)
            {
                var config = configs.Count > 0 ? configs[0] : null;
                Init(config);
            }
            else
            {
                DataVisible = false;
                Configs = configs;
            }
        }

        private void Ok(string config)
        {
            Init(config);
        }

        private void Init(string config)
        {
            try
            {
                _scriperConfigPath = config;
                _container = new ScriperLibContainer(config);
                _uiConfig = ScriperUIConfiguration.Load(_uiConfigPath);
                MainVM = new MainVM(_container, _uiConfig);
                DataVisible = true;
                Title = config;
            }
            catch(Exception ex)
            {
                MessageBoxExtensions.Show(ex.Message);
                logger.Error(ex);
            }
        }

        public void SaveConfig()
        {
            if (_container != null)
            {
                _container.GetInstance<IScriperConfiguration>().Save(_scriperConfigPath);
                _uiConfig.Save(_uiConfigPath);
            }
        }
    }
}
