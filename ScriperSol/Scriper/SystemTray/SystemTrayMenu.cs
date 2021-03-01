using Avalonia;
using Avalonia.Platform;
using NLog;
using Scriper.SystemTray.Windows;
using System;

namespace Scriper.SystemTray
{
    internal class SystemTrayMenu : ISystemTrayMenu
    {
        private static readonly Logger _logger = NLogFactoryProxy.Instance.GetLogger();
        private readonly IWindowsSystemTrayMenu _windowsSystemTrayMenu;
        private IOperationSystemTrayMenu _osSpecificSystemTrayMenu;

        public SystemTrayMenu(IWindowsSystemTrayMenu windowsSystemTrayMenu)
        {
            _windowsSystemTrayMenu = windowsSystemTrayMenu;
            InitByOperationSystem();
        }

        private void InitByOperationSystem()
        {
            var operatingSystem =
                AvaloniaLocator.Current.GetService<IRuntimePlatform>().GetRuntimeInfo().OperatingSystem;
            switch (operatingSystem)
            {
                case OperatingSystemType.WinNT:
                    _osSpecificSystemTrayMenu = _windowsSystemTrayMenu;
                    break;
                default:
                    _logger.Log(LogLevel.Warn, $"Scriper do not support SystemTrayMenu for current operation system: {operatingSystem}");
                    return;
            }

            var appName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            _osSpecificSystemTrayMenu.Init(appName);
        }

        public void Dispose()
        {
            _osSpecificSystemTrayMenu?.Dispose();
        }

        public bool TryInsertClickContextMenuItem(string name, Action<string> action, string imageName)
        {
            return _osSpecificSystemTrayMenu?.TryInsertClickContextMenuItem(name, action, imageName) ?? false;
        }

        public void RemoveContextMenuItem(string name)
        {
            _osSpecificSystemTrayMenu?.RemoveContextMenuItem(name);
        }

        public void ClearContextMenu()
        {
            _osSpecificSystemTrayMenu?.ClearContextMenu();
        }

        public void Show()
        {
            _osSpecificSystemTrayMenu?.Show();
        }

        public void Hide()
        {
            _osSpecificSystemTrayMenu?.Hide();
        }

        public void InsertContextMenuSeparator(string name)
        {
            _osSpecificSystemTrayMenu?.InsertContextMenuSeparator(name);
        }
    }
}
