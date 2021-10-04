using Microsoft.Scripting.Utils;
using Microsoft.Win32.TaskScheduler;
using ReactiveUI;
using ScriperLib.Configuration.TimeTrigger;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Scriper.ViewModels.TimeSchedule.Triggers
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

        private bool _lastDayInMonth;
        public bool LastDayInMonth
        {
            get => _lastDayInMonth;
            set
            {
                _lastDayInMonth = value;
                this.RaiseAndSetIfChanged(ref _lastDayInMonth, value);
            }
        }

        public IEnumerable<int> DaysOfMonth { get; } = new int[31].Select((item, index) => index);
        public ObservableCollection<int> SelectedDaysOfMonth { get; }

        public IEnumerable<string> MonthsOfYear { get; } = Enum.GetValues(typeof(MonthsOfTheYear)).Select(item => item.ToString());
        public ObservableCollection<string> SelectedMonthsOfYear { get; }

        public MonthlyTriggerVM(ITimeTriggerConfiguration configuration) 
            : base(configuration)
        {
            Time = configuration.Time.TimeOfDay;
            SelectedDaysOfMonth = new ObservableCollection<int>(configuration.DaysOfMonth);
            SelectedMonthsOfYear = new ObservableCollection<string>(configuration.MonthsOfYear);
        }

        public override ITimeTriggerConfiguration GetTriggerConfiguration()
        {
            _configuration.Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Time.Hours, Time.Minutes, Time.Seconds);
            _configuration.DaysOfMonth = SelectedDaysOfMonth.ToList();
            _configuration.MonthsOfYear = SelectedMonthsOfYear.ToList();

            return _configuration;
        }
    }
}
