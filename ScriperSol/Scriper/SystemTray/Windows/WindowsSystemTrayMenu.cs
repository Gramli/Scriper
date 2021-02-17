using Avalonia.Platform;
using Scriper.AssetsAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Scriper.SystemTray.Windows
{
    internal class WindowsSystemTrayMenu : IWindowsSystemTrayMenu
    {
        private NotifyIcon _notifyIcon;
        private ContextMenuStrip _contextMenuStrip;
        private IContainer _components;

        public OperatingSystemType OperatingSystemType => OperatingSystemType.WinNT;
        
        public void Init(string name)
        {
            _components = new Container();
            _contextMenuStrip = new ContextMenuStrip();
            _notifyIcon = new NotifyIcon(_components)
            {
                Visible = true,
                ContextMenuStrip = _contextMenuStrip,
                Text = name,
                Icon = WindowsAssetsAccess.Instance.GetAssetsIcon("icons8_console.ico"),
            };
        }

        public void AddRangeContextMenuItems(Dictionary<string, Action<string>> actionsDict)
        {
            foreach (var actionItem in actionsDict)
            {
                TryAddContextMenuItem(actionItem.Key, actionItem.Value);
            }
        }

        public bool TryAddContextMenuItem(string name, Action<string> action)
        {
            if(_contextMenuStrip.Items.ContainsKey(name))
            {
                return false;
            }

            var toolStripMenuItem = new ToolStripMenuItem()
            {
                Name = name,
                Text = name,
                CheckOnClick = false,
            };
            toolStripMenuItem.Click += (sender, eventArgs) => { action(name); };
            _contextMenuStrip.Items.Add(toolStripMenuItem);
            return true;
        }

        public void RemoveContextMenuItem(string name)
        {
            _contextMenuStrip.Items.RemoveByKey(name);
        }

        public void Show()
        {
            _notifyIcon.Visible = true;
        }

        public void Hide()
        {
            _notifyIcon.Visible = false;
        }

        public void ClearContextMenu()
        {
            _contextMenuStrip.Items.Clear();
        }

        public void Dispose()
        {
            _components.Dispose();
        }
    }
}
