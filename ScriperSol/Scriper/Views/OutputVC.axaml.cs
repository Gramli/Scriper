using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Scriper.ViewModels;

namespace Scriper.Views
{
    public class OutputVC : UserControl
    {
        public OutputVC()
        {
            this.InitializeComponent();
        }

        public OutputVC(ViewModelBase viewModel)
        {
            this.InitializeComponent();
            this.DataContext = viewModel;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
