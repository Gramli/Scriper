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
using Scriper.SystemTray;
using Scriper.SystemTray.Windows;

namespace Scriper.ViewModels
{
    public class MainWindowVM : ViewModelBase, IDisposable
    {
        private MainVM _mainVm;
        public MainVM MainVM
        {
            get => _mainVm;
            set => this.RaiseAndSetIfChanged(ref _mainVm, value);
        }

        private bool _dataVisible;
        public bool DataVisible
        {
            get => _dataVisible;
            set => this.RaiseAndSetIfChanged(ref _dataVisible, value);
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, $"Scriper: {Path.GetFileName(value)}");
        }

        public List<string> Configs { get; private set; }
        public ReactiveCommand<string, Unit> OkCmd { get; }

        private static readonly Logger logger = NLogExtensions.LogFactory.GetCurrentClassLogger();

        private readonly string _uiConfigPath;

        private ISystemTrayMenu _systemTrayMenu;
        private IScriperLibContainer _container;
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

        public void SaveConfigs()
        {
            if (_container == null)
            {
                return;
            }

            _scriperConfigPath ??= "Config/defaultScriper.config";
            _container.GetInstance<IScriperConfiguration>().Save(_scriperConfigPath);
            var uiConfig = MainVM.ActualUiConfiguration;
            uiConfig.Save(_uiConfigPath);
        }

        public void Dispose()
        {
            _systemTrayMenu.Dispose();
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
                _systemTrayMenu = new SystemTrayMenu(new WindowsSystemTrayMenu());
                AddCloseButtonToSystemTray();
                MainVM = new MainVM(_container, ScriperUIConfiguration.Load(_uiConfigPath), _systemTrayMenu);
                DataVisible = true;
                Title = config;
            }
            catch(Exception ex)
            {
                MessageBoxExtensions.Show(ex.Message);
                logger.Error(ex);
            }
        }

        private void AddCloseButtonToSystemTray()
        {
            _systemTrayMenu.TryInsertClickContextMenuItem("Exit", (param) => App.Current.Close(), "icons8_close_window.ico");
            _systemTrayMenu.InsertContextMenuSeparator("ExitSep");
        }
    }
}
