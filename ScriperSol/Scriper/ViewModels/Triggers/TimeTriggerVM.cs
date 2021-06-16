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

        public TimeTriggerVM(ITimeTriggerConfiguration configuration)
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
