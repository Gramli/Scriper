using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ScriperLib;

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
    }
}
