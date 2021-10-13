using Avalonia.Platform;
using NLog;
using ReactiveUI;
using Scriper.Configuration;
using Scriper.Extensions;
using Scriper.OperationSystem;
using Scriper.SystemTray;
using Scriper.Views;
using ScriperLib.Clone;
using ScriperLib.Extensions;
using System;
using System.Reactive;

namespace Scriper.ViewModels
{
    public class MainVM : ViewModelBase, IMainVM
    {
        public IScriptManagerVM ScriptManagerVM { get; }
        public ReactiveCommand<Unit, Unit> CreateScriptCmd { get; }
        public ReactiveCommand<Unit, Unit> FastCreateScriptCmd { get; }
        public ReactiveCommand<Unit, Unit> ExitCmd { get; }
        public ReactiveCommand<Unit, Unit> OpenSettingsCmd { get; }
        public ReactiveCommand<Unit, Unit> HideCmd { get; }
        public bool HidingEnabled {  get => OperationSystemInformation.GetOperatingSystemType == OperatingSystemType.WinNT; }
        public IScriperUIConfiguration ActualUiConfiguration { get; private set; }

        private static readonly Logger _logger = NLogFactoryProxy.Instance.GetLogger();

        private readonly ISystemTrayMenu _systemTrayMenu;
        private readonly Func<IScriperUIConfiguration, ISettingsVM> _createSettingsVM;
        private readonly IDeepCloneAdapter _deepCloneAdapter;

        public MainVM(IScriperUIConfiguration uiConfig,
            ISystemTrayMenu systemTrayMenu,
            IScriptManagerVM scriptManagerVM,
            Func<IScriperUIConfiguration, ISettingsVM> createSettingsVM,
            IDeepCloneAdapter deepCloneAdapter)
        {
            ActualUiConfiguration = uiConfig;
            ScriptManagerVM = scriptManagerVM;
            CreateScriptCmd = ReactiveCommand.Create(ScriptManagerVM.CreateScript).CatchError(_logger);
            ExitCmd = ReactiveCommand.Create(Exit);
            OpenSettingsCmd = ReactiveCommand.Create(OpenSettings);
            HideCmd = ReactiveCommand.Create(Hide);
            FastCreateScriptCmd = ReactiveCommand.Create(ScriptManagerVM.FastCreateScript).CatchError(_logger);
            _systemTrayMenu = systemTrayMenu;
            _createSettingsVM = createSettingsVM;
            _deepCloneAdapter = deepCloneAdapter;
        }

        public void Init()
        {
            ScriptManagerVM.Init();
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

        private void OpenSettings()
        {
            try
            {
                var newUIConfig = _deepCloneAdapter.DeepClone(ActualUiConfiguration);
                var settingsVM = _createSettingsVM(newUIConfig);
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
                _logger.Error(ex);
                MessageBoxExtensions.ShowDialog(ex.Message);
            }
        }
    }
}
