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
        [ConfigurationAttribute("Name", true)]
        public string Name { get; set; }

        [ConfigurationAttribute("Time", true)]
        public DateTime Time { get; set; } = DateTime.Now;

        [ConfigurationAttribute("Delay", false)]
        public long DelayInSeconds { get; set; }

        [ConfigurationAttribute("ScriptTriggerType", true)]
        public ScriptTriggerType ScriptTriggerType { get; set; }

        [ConfigurationAttribute("Interval", false)]
        public short Interval { get; set; }

        [ConfigurationCollection("DaysOfTheWeek", "Day")]
        public ICollection<string> DaysOfTheWeek { get; set; } = new List<string>();

        [ConfigurationCollection("DaysOfMonth", "DayNumber")]
        public ICollection<int> DaysOfMonth { get; set; } = new List<int>();

        [ConfigurationCollection("MonthsOfYear", "Month")]
        public ICollection<string> MonthsOfYear { get; set; } = new List<string>();

        public TimeTriggerConfiguration(XElement element) 
            : base(element)
        {
        }

        public TimeTriggerConfiguration()
        {
        }
    }
}
