using ReactiveUI;
using ScriperLib.Configuration;
using System;

namespace Scriper.ViewModels.Triggers
{
    class TimeTriggerVM : TriggerVM
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

        private DateTime _date;
        public DateTime Date
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
        }

        public override ITimeTriggerConfiguration GetTriggerConfiguration()
        {
            _configuration.Time = new DateTime(Date.Year, Date.Month, Date.Day, Time.Hour, Time.Minute, Time.Second);
            return _configuration;
        }
    }
}
