using ReactiveUI;
using ScriperLib.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Scripting.Utils;

namespace Scriper.ViewModels.Triggers
{
    public class WeeklyTriggerVM : TriggerVM
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

        private short _interval;
        public short Interval
        {
            get => _interval;
            set
            {
                _interval = value;
                this.RaiseAndSetIfChanged(ref _interval, value);
            }
        }

        public IEnumerable<string> DaysOfWeek { get; } = Enum.GetValues(typeof(DayOfWeek)).Select(item => item.ToString());
        public ObservableCollection<string> SelectedDaysOfWeek { get; }

        public WeeklyTriggerVM(ITimeTriggerConfiguration configuration) 
            : base(configuration)
        {
            Time = configuration.Time.TimeOfDay;
            Interval = configuration.Interval;
            SelectedDaysOfWeek = new ObservableCollection<string>(configuration.DaysOfTheWeek);
        }

        public override ITimeTriggerConfiguration GetTriggerConfiguration()
        {
            _configuration.Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Time.Hours, Time.Minutes, Time.Seconds);
            _configuration.Interval = Interval;
            _configuration.DaysOfTheWeek = SelectedDaysOfWeek.ToList();
            return _configuration;
        }
    }
}
