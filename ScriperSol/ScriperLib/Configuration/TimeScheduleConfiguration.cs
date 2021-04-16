using ScriperLib.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using ScriperLib.Configuration.Base;
using ScriperLib.Enums;

namespace ScriperLib.Configuration
{
    [Serializable]
    internal class TimeScheduleConfiguration : ConfigurationElement, ITimeScheduleConfiguration
    {
        [ConfigurationElement("Time", true)]
        public DateTime Time { get; set; }

        [ConfigurationElement("ScriptTriggerType", true)]
        public ScriptTriggerType ScriptTriggerType { get; set; }

        [ConfigurationElement("DaysInterval", false)]
        public short DaysInterval { get; set; }

        public TimeScheduleConfiguration(XElement element) 
            : base(element)
        {
        }
        public TimeScheduleConfiguration()
        {

        }
    }
}
