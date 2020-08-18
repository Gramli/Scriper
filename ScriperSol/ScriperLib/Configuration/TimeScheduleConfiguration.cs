using ScriperLib.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace ScriperLib.Configuration
{
    internal class TimeScheduleConfiguration : ConfigurationElement, ITimeScheduleConfiguration
    {
        [ConfigurationElement("Time")]
        public DateTime Time { get; private set; }

        [ConfigurationCollection("RepeatInDays", "DayOfWeek")]
        public ICollection<DayOfWeek> RepeatInDays { get; private set; }
        public TimeScheduleConfiguration(XElement element) : base(element)
        {
        }
    }
}
