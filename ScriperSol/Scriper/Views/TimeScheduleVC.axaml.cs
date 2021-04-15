using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Scriper.Views
{
    public class TimeScheduleVC : UserControl
    {
        public TimeScheduleVC()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
