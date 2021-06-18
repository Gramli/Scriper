using Microsoft.Scripting.Utils;
using Microsoft.Win32.TaskScheduler;
using ScriperLib.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ReactiveUI;

namespace Scriper.ViewModels.Triggers
{
    public class MonthlyTriggerVM : TriggerVM
    {
        private TimeSpan _time;
        public TimeSpan Time
        {
            get => _time;
            set
            {
                _time = value;
                this.RaiseAndSetIfChanged(ref _time, value);
            }
        }

        public IEnumerable<int> DaysOfMonth { get; } = new int[31].Select((item, index) => index);
        public ObservableCollection<int> SelectedDaysOfMonth { get; } = new ObservableCollection<int>();

        public IEnumerable<string> MonthsOfYear { get; } = Enum.GetValues(typeof(MonthsOfTheYear)).Select(item => item.ToString());
        public ObservableCollection<string> SelectedMonthsOfYear { get; } = new ObservableCollection<string>();

        public MonthlyTriggerVM(ITimeTriggerConfiguration configuration) 
            : base(configuration)
        {
            Time = configuration.Time.TimeOfDay;
        }

        public override ITimeTriggerConfiguration GetTriggerConfiguration()
        {
            _configuration.Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Time.Hours, Time.Minutes, Time.Seconds);

            return _configuration;
        }
    }
}
