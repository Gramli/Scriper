using System;
using System.Xml.Linq;

namespace ScriperLib.Configuration
{
    internal class ScriptConfiguration : ConfigurationElement, IScriptConfiguration
    {
        [XmlRepresentation("name")]
        public string Name { get; private set; }

        [XmlRepresentation("description")]
        public string Description { get; private set; }

        [XmlRepresentation("path")]
        public string Path { get; private set; }

        [XmlRepresentation("inSystemTray")]
        public bool InSystemTray { get; private set; }

        [XmlRepresentation("TimeSchedule", true)]
        public ITimeScheduleConfiguration TimeScheduleConfiguration { get; private set; }

        public ScriptConfiguration(string rawElement)
            : base(rawElement)
        {

        }

        public ScriptConfiguration(XElement element)
            : base(element)
        {

        }

        public override XElement Save()
        {
            throw new NotImplementedException();
        }
    }
}
