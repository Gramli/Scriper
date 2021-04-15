using System;
using System.Collections.Generic;
using ScriperLib.Configuration.Base;
using ScriperLib.Enums;

namespace ScriperLib.Configuration
{
    public interface ITimeScheduleConfiguration : IConfigurationElement
    {
        DateTime Time { get; set; }

        ScriptTriggerType ScriptTriggerType { get; set; }
        ICollection<DayOfWeek> RepeatInDays { get; }
    }
}
