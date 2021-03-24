using System;

namespace Scriper.SystemTray
{
    internal class SystemTrayMenu : ISystemTrayMenu
    {
        private readonly IOperationSystemTrayMenu _osSpecificSystemTrayMenu;

        public SystemTrayMenu(IOperatingSystemTrayMenuFactory windowsSystemTrayMenuFactory)
        {
            _osSpecificSystemTrayMenu = windowsSystemTrayMenuFactory.CreateOperationSystemTrayMenu();
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

        public bool TryRemoveContextMenuItem(string name)
        {
           return _osSpecificSystemTrayMenu?.TryRemoveContextMenuItem(name) ?? false;
        }
    }
}
