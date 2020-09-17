﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using System;

namespace Scriper.Views
{
    public class DialogWindow : Window
    {
        private DockPanel _myPanel;

        public DialogWindow()
        {
            this.InitializeComponent();

#if DEBUG
            this.AttachDevTools();
#endif
        }

        public DialogWindow(int width, int height, string title, IControl control, WindowIcon icon)
            : this(control)
        {
            this.Width = width;
            this.Height = height;
            this.Title = title;
            this.Icon = icon;
        }

        public DialogWindow(IControl control)
            : this()
        {
            _myPanel.Children.Add(control);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            _myPanel = this.FindControl<DockPanel>("basePanel");
        }
    }
}
