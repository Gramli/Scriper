using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Scriper.ViewModels;

namespace Scriper.Views.Triggers
{
    public partial class WeeklyTriggerVC : UserControl
    {
        public WeeklyTriggerVC()
        {
            InitializeComponent();
        }

        public WeeklyTriggerVC(ViewModelBase viewModel)
        :this()
        {
            this.DataContext = viewModel;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
