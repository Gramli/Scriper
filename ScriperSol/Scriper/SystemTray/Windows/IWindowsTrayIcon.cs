using System;
using System.Collections.Generic;

namespace Scriper.SystemTray.Windows
{
    interface IWindowsTrayIcon : IDisposable
    {
        void AddRangeContextMenuItems(Dictionary<string, Action<string>> actionsDict);
        bool TryAddContextMenuItem(string name, Action<string> action);
        void RemoveContextMenuItem(string name);
        void ClearContextMenu();
        void Show();
        void Hide();
    }
}
