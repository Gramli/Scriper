using ScriperLib.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using ScriperLib.Configuration.Base;
using ScriperLib.Enums;

namespace ScriperLib.Configuration
{
    [Serializable]
    internal class TimeTriggerConfiguration : ConfigurationElement, ITimeTriggerConfiguration
    {
        [ConfigurationElement("Name", true)]
        public string Name { get; set; }

        [ConfigurationElement("Time", true)]
        public DateTime Time { get; set; } = DateTime.Now;

        [ConfigurationElement("Delay", false)]
        public long DelayInSeconds { get; set; }

        [ConfigurationElement("ScriptTriggerType", true)]
        public ScriptTriggerType ScriptTriggerType { get; set; }

        [ConfigurationElement("Interval", false)]
        public short Interval { get; set; }

        [ConfigurationCollection("DaysOfTheWeek", "Name")]
        public ICollection<string> DaysOfTheWeek { get; set; }

        public TimeTriggerConfiguration(XElement element) 
            : base(element)
        {
        }

        public TimeTriggerConfiguration()
        {

        }
    }
}
