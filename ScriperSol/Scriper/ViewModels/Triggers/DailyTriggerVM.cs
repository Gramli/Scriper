using System;
using ReactiveUI;
using ScriperLib.Configuration;

namespace Scriper.ViewModels.Triggers
{
    public class DailyTrigger : Trigger
    {
        private DateTime _time;
        public override DateTime Time
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

        public DailyTrigger(ITimeTriggerConfiguration configuration) 
            : base(configuration)
        {
        }

        public override ITimeTriggerConfiguration GetTriggerConfiguration()
        {
            _configuration.Interval = Interval;
            _configuration.Time = Time;

            return _configuration;
        }
    }
}
