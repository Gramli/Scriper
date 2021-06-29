using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Scriper.Extensions;
using ScriperLib.Extensions;

namespace Scriper.Views
{
    public class MainVC : UserControl
    {
        public MainVC()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnAboutClick(object sender, RoutedEventArgs e)
        {
            var aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog(App.Current.GetMainWindow());
        }

        private void OnManualClick(object sender, RoutedEventArgs e)
        {
            BrowserExtensions.OpenBrowserByPlatform("https://github.com/Gramli/Scriper/wiki");
        }
    }
}
