using System;
using System.Collections.Generic;
using ScriperLib.Configuration.Base;

namespace ScriperLib.Configuration
{
    public interface ITimeScheduleConfiguration : IConfigurationElement
    {
        DateTime Time { get; set; }

        ICollection<DayOfWeek> RepeatInDays { get; }
    }
}
