using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Scriper.Views
{
    public class ManualWindow : Window
    {
        public ManualWindow()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
