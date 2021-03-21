using System;

namespace Scriper.SystemTray
{
    public interface ISystemTrayMenu : IDisposable
    {
        void InsertContextMenuSeparator(string name);
        bool TryInsertClickContextMenuItem(string name, Action<string> action, string iconName);
        void RemoveContextMenuItem(string name);
        bool TryRemoveContextMenuItem(string name);
        void ClearContextMenu();
        void Show();
        void Hide();
    }
}
