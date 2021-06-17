using ReactiveUI;
using ScriperLib.Configuration;

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

        }

        public override ITimeTriggerConfiguration GetTriggerConfiguration()
        {
            _configuration.DelayInSeconds = Delay;

            return _configuration;
        }
    }
}
