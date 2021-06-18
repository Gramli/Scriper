using ReactiveUI;
using ScriperLib.Configuration;
using System;

namespace Scriper.ViewModels.Triggers
{
    class TimeTriggerVM : TriggerVM
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

        private DateTimeOffset _date;
        public DateTimeOffset Date
        {
            get => _date;
            set
            {
                _date = value;
                this.RaiseAndSetIfChanged(ref _date, value);
            }
        }

        public TimeTriggerVM(ITimeTriggerConfiguration configuration)
            : base(configuration)
        {
            Time = configuration.Time.TimeOfDay;
            Date = new DateTimeOffset(configuration.Time);
        }

        public override ITimeTriggerConfiguration GetTriggerConfiguration()
        {
            _configuration.Time = new DateTime(Date.Year, Date.Month, Date.Day, Time.Hours, Time.Minutes, Time.Seconds);
            return _configuration;
        }
    }
}
