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
        public TimeScheduleVC(ITimeScheduleVM timeScheduleVm)
        :this()
        {
            timeScheduleVm.OnTriggerChanged += OnTriggerChanged;
            timeScheduleVm.OnTriggerApplied += TimeScheduleVm_OnTriggerApplied;
            this.DataContext = timeScheduleVm;
            InitializeContentControl(timeScheduleVm);
        }

        private void InitializeContentControl(ITimeScheduleVM timeScheduleVm)
        {
            timeScheduleVm.InvokeSelection();
        }

        private void TimeScheduleVm_OnTriggerApplied(object sender, System.EventArgs e)
        {
            var contentControl = this.FindControl<ContentControl>("contentControl");
            contentControl.Content = null;
        }

        private void OnTriggerChanged(object sender, TriggerChangedEventArgs eventArgs)
        {
            var contentControl = this.FindControl<ContentControl>("contentControl");

            switch (eventArgs.ScriptTriggerType)
            {
                case ScriptTriggerType.Daily:
                    contentControl.Content = new DailyTriggerVC(eventArgs.TriggerVM);
                    break;
                case ScriptTriggerType.Logon:
                    contentControl.Content = new LogonTriggerVC(eventArgs.TriggerVM);
                    break;
                case ScriptTriggerType.Monthly:
                    contentControl.Content = new MonthlyTriggerVC(eventArgs.TriggerVM);
                    break;
                case ScriptTriggerType.Time:
                    contentControl.Content = new TimeTriggerVC(eventArgs.TriggerVM);
                    break;
                case ScriptTriggerType.Weekly:
                    contentControl.Content = new WeeklyTriggerVC(eventArgs.TriggerVM);
                    break;
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
