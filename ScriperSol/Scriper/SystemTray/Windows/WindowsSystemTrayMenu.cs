﻿using Avalonia.Platform;
using Scriper.AssetsAccess;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Scriper.SystemTray.Windows
{
    internal class WindowsSystemTrayMenu : IWindowsSystemTrayMenu
    {
        public OperatingSystemType OperatingSystemType => OperatingSystemType.WinNT;

        private NotifyIcon _notifyIcon;
        private ContextMenuStrip _contextMenuStrip;
        private IContainer _components;

        private readonly IAssets _assets;
        public WindowsSystemTrayMenu(IAssets assets)
        {
            _assets = assets;
        }

        public void Init(string name)
        {
            _components = new Container();
            _contextMenuStrip = new ContextMenuStrip();
            _notifyIcon = new NotifyIcon(_components)
            {
                Visible = true,
                ContextMenuStrip = _contextMenuStrip,
                Text = name,
                Icon = new Icon(_assets.GetAssetsImageMemoryStream("icons8_console.ico"))
            };
        }

        public bool TryInsertClickContextMenuItem(string name, Action<string> action, string imageName)
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
                Image = new Bitmap(_assets.GetAssetsImageMemoryStream(imageName))
            };
            toolStripMenuItem.Click += (sender, eventArgs) => { action(name); };
            _contextMenuStrip.Items.Insert(0,toolStripMenuItem);
            return true;
        }


        public void InsertContextMenuSeparator(string name)
        {
            var toolStripMenuSeparator = new ToolStripSeparator()
            {
                Name = name,
                Visible = true,
            };
            _contextMenuStrip.Items.Insert(0, toolStripMenuSeparator);
        }

        public bool TryRemoveContextMenuItem(string name)
        {
            if(_contextMenuStrip.Items.ContainsKey(name))
            {
                RemoveContextMenuItem(name);
                return true;
            }
            return false;
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
