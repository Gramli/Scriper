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
        public string Name { get; private set; }

        [ConfigurationAttribute("description")]
        public string Description { get; private set; }

        [ConfigurationAttribute("path")]
        public string Path { get; private set; }

        [ConfigurationAttribute("inSystemTray")]
        public bool InSystemTray { get; private set; }

        [ConfigurationAttribute("createLog")]
        public bool CreateLog { get; private set; }

        [ConfigurationAttribute("runInNewWindow")]
        public bool RunInNewWindow { get; private set; }

        [ConfigurationCollection("TimeSchedules", "TimeSchedule")]
        public ICollection<ITimeScheduleConfiguration> TimeScheduleConfigurations { get; private set; }

        public ScriptConfiguration(XElement element)
            : base(element)
        {

        }
    }
}
