using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Scriper.ViewModels;
using Scriper.ViewModels.Script;

namespace Scriper.Views
{
    public class OutputVC : UserControl
    {
        public OutputVC()
        {
            this.InitializeComponent();
        }

        public OutputVC(IOutputVM viewModel)
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
