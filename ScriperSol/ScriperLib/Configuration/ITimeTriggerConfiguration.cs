using ScriperLib.Configuration.Base;
using ScriperLib.Enums;
using System;
using System.Collections.Generic;

namespace ScriperLib.Configuration
{
    public interface ITimeTriggerConfiguration : IConfigurationElement
    {
        ScriptTriggerType ScriptTriggerType { get; set; }
        DateTime Time { get; set; }
        long DelayInSeconds { get; set; }
        short Interval { get; set; }
        ICollection<string> DaysOfTheWeek { get; set; }
    }
}
