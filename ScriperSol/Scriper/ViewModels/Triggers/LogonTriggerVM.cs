using ReactiveUI;
using ScriperLib.Configuration.TimeTrigger;

namespace Scriper.ViewModels.Triggers
{
    public class LogonTriggerVM : TriggerVM
    {
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
            Delay = configuration.DelayInSeconds;
        }

        public override ITimeTriggerConfiguration GetTriggerConfiguration()
        {
            _configuration.DelayInSeconds = Delay;

            return _configuration;
        }
    }
}
