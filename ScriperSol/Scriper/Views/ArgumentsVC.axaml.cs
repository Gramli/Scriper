using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Scriper.Views
{
    public partial class ArgumentsVC : UserControl
    {
        public ArgumentsVC()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
