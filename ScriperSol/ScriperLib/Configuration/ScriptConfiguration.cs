using ScriperLib.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using ScriperLib.Configuration.Base;
using ScriperLib.Configuration.Outputs;

namespace ScriperLib.Configuration
{
    internal class ScriptConfiguration : ConfigurationElement, IScriptConfiguration
    {
        [ConfigurationAttribute("name", true)]
        public string Name { get; set; }

        [ConfigurationAttribute("description")]
        public string Description { get; set; }

        [ConfigurationAttribute("path", true)]
        public string Path { get; set; }

        [ConfigurationAttribute("arguments", true)]
        public string Arguments { get; set; }

        [ConfigurationAttribute("inSystemTray")]
        public bool InSystemTray { get; set; }

        [ConfigurationAttribute("runInNewWindow")]
        public bool RunInNewWindow { get; set; }

        [ConfigurationCollection("TimeSchedules", "TimeSchedule")]
        public ICollection<ITimeScheduleConfiguration> TimeScheduleConfigurations { get; private set; }

        [ConfigurationElement("ConsoleOutput")]
        public IConsoleOutputConfiguration ConsoleOutputConfiguration { get; set; }

        [ConfigurationElement("FileOuput")]
        public IFileOutputConfiguration FileOutputConfiguration { get; set; }

        public ScriptConfiguration(XElement element)
            : base(element)
        {

        }

        public ScriptConfiguration()
        {
            TimeScheduleConfigurations = new List<ITimeScheduleConfiguration>();
        }
    }
}
