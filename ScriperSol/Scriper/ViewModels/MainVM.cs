using Avalonia.Platform;
using NLog;
using ReactiveUI;
using Scriper.Configuration;
using Scriper.Extensions;
using Scriper.OperationSystem;
using Scriper.SystemTray;
using Scriper.Views;
using ScriperLib.Extensions;
using System;
using System.Reactive;

namespace Scriper.ViewModels
{
    public class MainVM : ViewModelBase, IMainVM
    {
        public IScriptManagerVM ScriptManagerVM { get; }
        public ReactiveCommand<string, Unit> CreateScriptCmd { get; }
        public ReactiveCommand<Unit, Unit> ExitCmd { get; }
        public ReactiveCommand<Unit, Unit> OpenSettingsCmd { get; }
        public ReactiveCommand<Unit, Unit> HideCmd { get; }
        public bool HidingEnabled {  get => OperationSystemInformation.GetOperatingSystemType == OperatingSystemType.WinNT; }
        public IScriperUIConfiguration ActualUiConfiguration { get; private set; }

        private static readonly Logger _logger = NLogFactoryProxy.Instance.GetLogger();

        private readonly ISystemTrayMenu _systemTrayMenu;
        private readonly Func<IScriperUIConfiguration, ISettingsVM> _createSettingsVM;

        public MainVM(IScriperUIConfiguration uiConfig,
            ISystemTrayMenu systemTrayMenu,
            IScriptManagerVM scriptManagerVM,
            Func<IScriperUIConfiguration, ISettingsVM> createSettingsVM)
        {
            ActualUiConfiguration = uiConfig;
            ScriptManagerVM = scriptManagerVM;
            CreateScriptCmd = ReactiveCommand.Create<string>(CreateScript);
            ExitCmd = ReactiveCommand.Create(Exit);
            OpenSettingsCmd = ReactiveCommand.Create(OpenSettings);
            HideCmd = ReactiveCommand.Create(Hide);
            _systemTrayMenu = systemTrayMenu;
            _createSettingsVM = createSettingsVM;
        }

        public void Exit()
        {
            App.Current.Close();
        }

        private void Hide()
        {
            var showItemName = "Show";
            var showSeparatorName = "ShowSep";
            _systemTrayMenu.InsertContextMenuSeparator(showSeparatorName);
            _systemTrayMenu.TryInsertClickContextMenuItem(showItemName, (param) =>
            { 
                App.Current.Show();
                _systemTrayMenu.RemoveContextMenuItem(showItemName);
                _systemTrayMenu.RemoveContextMenuItem(showSeparatorName);
            }, "icons8_advertisement_page_96px.png");
            App.Current.Hide();
        }

        private void CreateScript(string argument)
        {
            try
            {
                ScriptManagerVM.CreateScript();
            }
            catch (Exception ex)
            {
                MessageBoxExtensions.ShowDialog(ex.Message);
                _logger.Error(ex);
            }
        }

        private void OpenSettings()
        {
            try
            {
                var settingsVM = _createSettingsVM(ActualUiConfiguration.DeepClone());
                var settingsWindow = new SettingsWindow(settingsVM);

                settingsVM.Close += (sender, args) =>
                {
                    if(!args.Cancel)
                    {
                        ActualUiConfiguration = args.Result;
                    }
                    settingsWindow.Close();
                };

                settingsWindow.ShowDialog(App.Current.GetMainWindow());
            }
            catch(Exception ex)
            {
                MessageBoxExtensions.ShowDialog(ex.Message);
                _logger.Error(ex);
            }
        }
    }
}
