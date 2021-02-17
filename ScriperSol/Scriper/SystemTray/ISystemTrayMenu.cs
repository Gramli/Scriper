using System;
using System.Collections.Generic;
using System.Text;

namespace Scriper.SystemTray
{
    public interface ISystemTrayMenu : IDisposable
    {
        void AddRangeContextMenuItems(Dictionary<string, Action<string>> actionsDict);
        bool TryAddContextMenuItem(string name, Action<string> action);
        void RemoveContextMenuItem(string name);
        void ClearContextMenu();
        void Show();
        void Hide();
    }
}
