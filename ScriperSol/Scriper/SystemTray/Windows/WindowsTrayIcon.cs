using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace Scriper.SystemTray.Windows
{
    internal class WindowsTrayIcon : IDisposable, IWindowsTrayIcon
    {
        public static IWindowsTrayIcon Current => _windowsTrayIcon ??= new WindowsTrayIcon("Scriper");
        private static IWindowsTrayIcon _windowsTrayIcon;

        private readonly NotifyIcon _notifyIcon;
        private readonly ContextMenuStrip _contextMenuStrip;
        private readonly IContainer _components;

        private WindowsTrayIcon(string text)
        {
            _components = new Container();
            _contextMenuStrip = new ContextMenuStrip();
            _notifyIcon = new NotifyIcon(_components)
            {
                ContextMenuStrip = _contextMenuStrip,
                Text = text,
                Icon = new Icon(@"C:\github\Scriper\ScriperSol\Scriper\Assets\icons8_console.ico"),
            };

        }

        public void AddRangeContextMenuItems(Dictionary<string, Action<string>> actionsDict)
        {
            foreach (var actionItem in actionsDict)
            {
                AddContextMenuItem(actionItem.Key, actionItem.Value);
            }
        }

        public void AddContextMenuItem(string name, Action<string> action)
        {
            var toolStripMenuItem = new ToolStripMenuItem()
            {
                Name = name,
                Text = name,
                CheckOnClick = false,
            };
            toolStripMenuItem.Click += (sender, eventArgs) => { action(name); };
            _contextMenuStrip.Items.Add(toolStripMenuItem);
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
