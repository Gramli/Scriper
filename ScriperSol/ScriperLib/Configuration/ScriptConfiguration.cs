using ScriperLib.Configuration.Attributes;
using System;
using System.Xml.Linq;

namespace ScriperLib.Configuration
{
    internal class ScriptConfiguration : ConfigurationElement, IScriptConfiguration
    {
        [XmlAttribute("name")]
        public string Name { get; private set; }

        [XmlAttribute("description")]
        public string Description { get; private set; }

        [XmlAttribute("path")]
        public string Path { get; private set; }

        [XmlAttribute("inSystemTray")]
        public bool InSystemTray { get; private set; }

        [XmlElement("TimeSchedule")]
        public ITimeScheduleConfiguration TimeScheduleConfiguration { get; private set; }

        public ScriptConfiguration(XElement element)
            : base(element)
        {

        }
    }
}
