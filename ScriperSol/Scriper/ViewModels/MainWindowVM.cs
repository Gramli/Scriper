﻿using NLog;
using ReactiveUI;
using Scriper.Configuration;
using Scriper.Configuration.Finders;
using Scriper.Extensions;
using Scriper.SystemTray;
using Scriper.SystemTray.Windows;
using ScriperLib;
using ScriperLib.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive;

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

        public IList<string> Configs { get; private set; }
        public ReactiveCommand<string, Unit> OkCmd { get; }

        private ISystemTrayMenu _systemTrayMenu;
        private IScriperLibContainer _container;
        private string _scriperConfigPath;
        private string _uiConfigPath;

        private readonly ScriperConfigFinder _scriperConfigFinder;
        private readonly ScriperUIConfigFinder _scriperUiConfigFinder;

        private static readonly Logger _logger = NLogFactoryProxy.Instance.GetLogger();

        public MainWindowVM()
        {
            OkCmd = ReactiveCommand.Create<string>(Ok);
            _scriperConfigFinder = new ScriperConfigFinder();
            _scriperUiConfigFinder = new ScriperUIConfigFinder();

            InitUIConfig();
            InitConfigs();

        }

        public void SaveConfigs()
        {
            if (_container == null)
            {
                return;
            }

            _scriperConfigPath ??= _scriperConfigFinder.GetDefaultConfigPath();
            _container.GetInstance<IScriperConfiguration>().Save(_scriperConfigPath);
            var uiConfig = MainVM.ActualUiConfiguration;
            uiConfig.Save(_uiConfigPath);
        }

        public void Dispose()
        {
            _systemTrayMenu.Dispose();
        }

        private void InitUIConfig()
        {
            _uiConfigPath = _scriperUiConfigFinder.FindConfig();
        }

        private void InitConfigs()
        {
            var configs = _scriperConfigFinder.FindConfigs();

            if (configs.Count < 2)
            {
                var config = configs.Count > 0 ? configs[0] : null;
                InitWithSelectedConfig(config);
            }
            else
            {
                DataVisible = false;
                Configs = configs;
            }
        }

        private void Ok(string config)
        {
            InitWithSelectedConfig(config);
        }

        private void InitWithSelectedConfig(string config)
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
                _logger.Error(ex);
            }
        }

        private void AddCloseButtonToSystemTray()
        {
            _systemTrayMenu.TryInsertClickContextMenuItem("Exit", (param) => App.Current.Close(), "icons8_close_window.ico");
            _systemTrayMenu.InsertContextMenuSeparator("ExitSep");
        }
    }
}
