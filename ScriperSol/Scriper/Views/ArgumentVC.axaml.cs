using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Scriper.Views
{
    public partial class ArgumentVC : UserControl
    {
        public ArgumentVC()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
