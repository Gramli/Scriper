using ReactiveUI;
using ScriperLib.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Scripting.Utils;

namespace Scriper.ViewModels.Triggers
{
    public class WeeklyTriggerVM : TriggerVM
    {
        private DateTime _time;
        public DateTime Time
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
        public ObservableCollection<string> SelectedDaysOfWeek { get; } = new ObservableCollection<string>();

        public WeeklyTriggerVM(ITimeTriggerConfiguration configuration) 
            : base(configuration)
        {
        }

        public override ITimeTriggerConfiguration GetTriggerConfiguration()
        {
            _configuration.Time = Time;
            _configuration.Interval = Interval;

            return _configuration;
        }
    }
}
