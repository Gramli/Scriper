using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using ScriperLib.Configuration;

namespace Scriper.ViewModels.Triggers
{
    public class LogonTriggerVM : TriggerVM
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

        private long _delay;
        public long Delay
        {
            get => _delay;
            set
            {
                _delay = value;
                this.RaiseAndSetIfChanged(ref _delay, value);
            }
        }

        public LogonTriggerVM(ITimeTriggerConfiguration configuration) : base(configuration)
        {
        }
        public override ITimeTriggerConfiguration GetTriggerConfiguration()
        {
            _configuration.DelayInSeconds = Delay;
            _configuration.Time = Time;

            return _configuration;
        }
    }
}
