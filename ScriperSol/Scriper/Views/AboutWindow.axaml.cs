using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Scriper.ViewModels;
using System;
using Avalonia.Interactivity;
using ScriperLib.Extensions;

namespace Scriper.Views
{
    public class AboutWindow : Window
    {
        public AboutWindow()
        {
            this.InitializeComponent();

            this.DataContext = new AboutVM();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnReportClick(object sender, RoutedEventArgs e)
        {
            BrowserExtensions.OpenBrowserByPlatform("https://github.com/Gramli/Scriper/discussions");
        }
    }
}
