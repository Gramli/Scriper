using System;
using ScriperLib.Configuration;

namespace Scriper.ViewModels.Triggers
{
    public abstract class Trigger : ViewModelBase
    {
        public abstract DateTime Time { get; set; }

        protected ITimeTriggerConfiguration _configuration;

        protected Trigger(ITimeTriggerConfiguration configuration)
        {
            _configuration = configuration;
        }

        public abstract ITimeTriggerConfiguration GetTriggerConfiguration();
    }
}
