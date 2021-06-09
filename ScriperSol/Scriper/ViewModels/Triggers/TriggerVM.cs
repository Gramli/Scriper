﻿using System;
using ScriperLib.Configuration;

namespace Scriper.ViewModels.Triggers
{
    public abstract class TriggerVM : ViewModelBase
    {
        public abstract DateTime Time { get; set; }

        protected ITimeTriggerConfiguration _configuration;

        protected TriggerVM(ITimeTriggerConfiguration configuration)
        {
            _configuration = configuration;
        }

        public abstract ITimeTriggerConfiguration GetTriggerConfiguration();
    }
}