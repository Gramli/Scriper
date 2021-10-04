using ScriperLib.Configuration.TimeTrigger;

namespace Scriper.ViewModels.TimeSchedule.Triggers
{
    public abstract class TriggerVM : ViewModelBase
    {
        protected ITimeTriggerConfiguration _configuration;

        protected TriggerVM(ITimeTriggerConfiguration configuration)
        {
            _configuration = configuration;
        }

        public abstract ITimeTriggerConfiguration GetTriggerConfiguration();
    }
}
