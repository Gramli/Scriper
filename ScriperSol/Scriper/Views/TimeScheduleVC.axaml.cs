using System.Management.Automation.Remoting;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Scriper.ViewModels;
using Scriper.Views.Triggers;
using ScriperLib.Enums;

namespace Scriper.Views
{
    public class TimeScheduleVC : UserControl
    {
        public TimeScheduleVC()
        {
            InitializeComponent();
        }
        //there is only map of VM and VC
        //catch VM event about trigger type change
        public TimeScheduleVC(TimeScheduleVM timeScheduleVm)
        :this()
        {
            timeScheduleVm.OnTriggerChanged += OnTriggerChanged;
            this.DataContext = timeScheduleVm;
        }

        private void OnTriggerChanged(object sender, TriggerChangedEventArgs eventArgs)
        {
            var contentControl = this.FindControl<ContentControl>("contentControl");

            switch (eventArgs.ScriptTriggerType)
            {
                case ScriptTriggerType.Daily:
                    break;
                case ScriptTriggerType.Logon:
                    contentControl.Content = new LogonTriggerVC();
                    break;
                case ScriptTriggerType.Monthly:
                    break;
                case ScriptTriggerType.Time:
                    contentControl.Content = new TimeTriggerVC();
                    break;
                case ScriptTriggerType.Weekly:
                    break;
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
