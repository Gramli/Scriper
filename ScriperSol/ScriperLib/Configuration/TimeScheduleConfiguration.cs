using ScriperLib.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using ScriperLib.Configuration.Base;

namespace ScriperLib.Configuration
{
    internal class TimeScheduleConfiguration : ConfigurationElement, ITimeScheduleConfiguration
    {
        [ConfigurationElement("Time", true)]
        public DateTime Time { get; set; }

        [ConfigurationCollection("RepeatInDays", "DayOfWeek")]
        public ICollection<DayOfWeek> RepeatInDays { get; private set; }
        public TimeScheduleConfiguration(XElement element) 
            : base(element)
        {
        }
        public TimeScheduleConfiguration()
        {

        }
    }
}
