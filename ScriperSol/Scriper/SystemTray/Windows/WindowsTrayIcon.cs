using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using Scriper.Extensions;
using System.IO;

namespace Scriper.SystemTray.Windows
{
    internal class WindowsTrayIcon : IWindowsTrayIcon
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
                Visible = true,
                ContextMenuStrip = _contextMenuStrip,
                Text = text,
                Icon = GetAssetsIcon("icons8_console.ico"),
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

        private Icon GetAssetsIcon(string name)
        {
            var avaloniaIcon = AssetsExtensions.GetAssetsIcon(name);
            using var iconStream = new MemoryStream();
            avaloniaIcon.Save(iconStream);
            return new Icon(iconStream); 
        }
    }
}
