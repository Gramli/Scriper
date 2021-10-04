using ReactiveUI;
using ScriperLib.Configuration.TimeTrigger;
using System;

namespace Scriper.ViewModels.TimeSchedule.Triggers
{
    public class DailyTriggerVM : TriggerVM
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

        public DailyTriggerVM(ITimeTriggerConfiguration configuration) 
            : base(configuration)
        {
            Time = configuration.Time.TimeOfDay;
            Interval = configuration.Interval == 0 ? (short)1 : configuration.Interval;
        }

        public override ITimeTriggerConfiguration GetTriggerConfiguration()
        {
            _configuration.Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Time.Hours, Time.Minutes, Time.Seconds);
            //1 is every day, 2 every other day
            _configuration.Interval = Interval;
            return _configuration;
        }
    }
}
