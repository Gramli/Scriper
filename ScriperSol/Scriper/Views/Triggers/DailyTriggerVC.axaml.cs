using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Scriper.ViewModels;

namespace Scriper.Views.Triggers
{
    public partial class DailyTriggerVC : UserControl
    {
        public DailyTriggerVC()
        {
            InitializeComponent();
        }

        public DailyTriggerVC(ViewModelBase viewModel)
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
