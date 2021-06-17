using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Scriper.ViewModels;

namespace Scriper.Views.Triggers
{
    public partial class LogonTriggerVC : UserControl
    {
        public LogonTriggerVC()
        {
            InitializeComponent();
        }

        public LogonTriggerVC(ViewModelBase viewModel)
            : this()
        {
            this.DataContext = viewModel;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
