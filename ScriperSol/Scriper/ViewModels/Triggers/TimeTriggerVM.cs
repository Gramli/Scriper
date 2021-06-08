using ReactiveUI;
using ScriperLib.Configuration;
using System;

namespace Scriper.ViewModels.Triggers
{
    class TimeTrigger : Trigger
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

        public TimeTrigger(ITimeTriggerConfiguration configuration)
            : base(configuration)
        {
        }

        public override ITimeTriggerConfiguration GetTriggerConfiguration()
        {
            _configuration.Time = Time;

            return _configuration;
        }
    }
}
