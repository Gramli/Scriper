using System;
using System.Collections.Generic;

namespace Scriper.SystemTray.Windows
{
    interface IWindowsTrayIcon
    {
        void AddRangeContextMenuItems(Dictionary<string, Action<string>> actionsDict);
        void AddContextMenuItem(string name, Action<string> action);
        void RemoveContextMenuItem(string name);
        void ClearContextMenu();
        void Show();
        void Hide();
    }
}
