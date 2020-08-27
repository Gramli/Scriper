using ScriperLib.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using ScriperLib.Configuration.Base;

namespace ScriperLib.Configuration
{
    internal class ScriptConfiguration : ConfigurationElement, IScriptConfiguration
    {
        [ConfigurationAttribute("name")]
        public string Name { get; set; }

        [ConfigurationAttribute("description")]
        public string Description { get; set; }

        [ConfigurationAttribute("path")]
        public string Path { get; set; }

        [ConfigurationAttribute("inSystemTray")]
        public bool InSystemTray { get; set; }

        [ConfigurationAttribute("createLog")]
        public bool CreateLog { get; set; }

        [ConfigurationAttribute("runInNewWindow")]
        public bool RunInNewWindow { get; set; }

        [ConfigurationCollection("TimeSchedules", "TimeSchedule")]
        public ICollection<ITimeScheduleConfiguration> TimeScheduleConfigurations { get; private set; }

        internal ScriptConfiguration(XElement element)
            : base(element)
        {

        }

        public ScriptConfiguration()
        {
            TimeScheduleConfigurations = new List<ITimeScheduleConfiguration>();
        }
    }
}
