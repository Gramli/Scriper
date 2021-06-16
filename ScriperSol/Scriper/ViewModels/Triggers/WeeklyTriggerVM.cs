using ReactiveUI;
using ScriperLib.Configuration;
using System;
using System.Collections.Generic;

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

        private IList<string> _daysOfWeek;
        public IList<string> DaysOfWeek
        {
            get => _daysOfWeek;
            set { _daysOfWeek = value; }
        }

        public WeeklyTriggerVM(ITimeTriggerConfiguration configuration) : base(configuration)
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
