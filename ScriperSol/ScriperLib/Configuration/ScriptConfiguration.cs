using ScriperLib.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using ScriperLib.Configuration.Base;
using ScriperLib.Configuration.Outputs;

namespace ScriperLib.Configuration
{
    [Serializable]
    internal class ScriptConfiguration : ConfigurationElement, IScriptConfiguration
    {
        [ConfigurationAttribute("name", true)]
        public string Name { get; set; }

        [ConfigurationAttribute("description")]
        public string Description { get; set; }

        [ConfigurationAttribute("path", true)]
        public string Path { get; set; }

        [ConfigurationAttribute("arguments")]
        public string Arguments { get; set; }

        [ConfigurationAttribute("inSystemTray")]
        public bool InSystemTray { get; set; }

        [ConfigurationAttribute("outputWindow")]
        public bool OutputWindow { get; set; } = true;

        [ConfigurationAttribute("lastRun", false)]
        public string LastRun { get; set; }

        [ConfigurationAttribute("order", false)]
        public int Order { get; set; } = -1;

        [ConfigurationCollection("TimeSchedules", "TimeSchedule")]
        public ICollection<ITimeTriggerConfiguration> TimeScheduleConfigurations { get; private set; }

        [ConfigurationElement("FileOuput")]
        public IFileOutputConfiguration FileOutputConfiguration { get; set; }

        public ScriptConfiguration(XElement element)
            : base(element)
        {

        }

        public ScriptConfiguration()
        {
            TimeScheduleConfigurations = new List<ITimeTriggerConfiguration>();
        }
    }
}
