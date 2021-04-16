using ScriperLib.Configuration.Base;
using ScriperLib.Enums;
using System;

namespace ScriperLib.Configuration
{
    public interface ITimeScheduleConfiguration : IConfigurationElement
    {
        DateTime Time { get; set; }
        ScriptTriggerType ScriptTriggerType { get; set; }
        short DaysInterval { get; set; }
    }
}
