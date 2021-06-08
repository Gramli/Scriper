using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Scriper.Views
{
    public class TimeScheduleVC : UserControl
    {
        //there is only map of VM and VC
        //catch VM event about trigger type change
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
