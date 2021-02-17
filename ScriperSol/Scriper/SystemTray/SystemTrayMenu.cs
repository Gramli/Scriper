using Avalonia;
using Avalonia.Platform;
using NLog;
using Scriper.Extensions;
using Scriper.SystemTray.Windows;
using System;
using System.Collections.Generic;

namespace Scriper.SystemTray
{
    internal class SystemTrayMenu : ISystemTrayMenu
    {
        private static readonly Logger logger = NLogExtensions.LogFactory.GetCurrentClassLogger();
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
                    logger.Log(LogLevel.Warn, $"Scriper do not support SystemTrayMenu for current operation system: {operatingSystem}");
                    return;
            }

            var appName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            _osSpecificSystemTrayMenu.Init(appName);
        }

        public void Dispose()
        {
            _osSpecificSystemTrayMenu?.Dispose();
        }

        public void AddRangeContextMenuItems(Dictionary<string, Action<string>> actionsDict)
        {
            _osSpecificSystemTrayMenu?.AddRangeContextMenuItems(actionsDict);
        }

        public bool TryAddContextMenuItem(string name, Action<string> action)
        {
            return _osSpecificSystemTrayMenu?.TryAddContextMenuItem(name, action) ?? false;
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
    }
}
