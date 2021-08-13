using ScriperLib.Configuration.Base;
using ScriperLib.Enums;
using System;
using System.Collections.Generic;

namespace ScriperLib.Configuration.TimeTrigger
{
    public interface ITimeTriggerConfiguration : IConfigurationElement
    {
        string Name { get; set; }
        ScriptTriggerType ScriptTriggerType { get; set; }
        DateTime Time { get; set; }
        long DelayInSeconds { get; set; }
        short Interval { get; set; }
        ICollection<string> DaysOfTheWeek { get; set; }
        ICollection<int> DaysOfMonth { get; set; }
        ICollection<string> MonthsOfYear { get; set; }
    }
}
