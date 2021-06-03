using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Scriper.ViewModels;

namespace Scriper.Views
{
    public class SettingsWindow : Window
    {
        public SettingsWindow(ViewModelBase viewModel)
            : this()
        {
            this.DataContext = viewModel;
        }

        public SettingsWindow()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
