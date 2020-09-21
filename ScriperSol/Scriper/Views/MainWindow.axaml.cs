using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Scriper.Extensions;
using Scriper.ViewModels;

namespace Scriper.Views
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Closed += MainWindow_Closed;
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void MainWindow_Closed(object sender, System.EventArgs e)
        {
            var viewModel = this.DataContext as MainWindowVM;
            if(viewModel != null)
            {
                viewModel.SaveConfig();
                App.Current.CloseWindows();
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
