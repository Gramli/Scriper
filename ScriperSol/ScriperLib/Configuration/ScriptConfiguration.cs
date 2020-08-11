using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;

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

        protected override void Parse(XElement scriptElement)
        {
            var properties = this.GetType().GetProperties();
            foreach (var property in properties)
            {
                var attribute = (XmlRepresentationAttribute)property.GetCustomAttributes(typeof(XmlRepresentationAttribute), false).Single();
                if (!attribute.IsElement)
                {
                    property.SetValue(this, Convert.ChangeType(scriptElement.Attribute(attribute.Name).Value, property.PropertyType));
                }
                else
                {
                    var flags = BindingFlags.NonPublic | BindingFlags.Instance;
                    var element = scriptElement.Element(attribute.Name);
                    var propertyValue = Activator.CreateInstance(property.PropertyType, flags, null, element, CultureInfo.InvariantCulture);
                    property.SetValue(this, propertyValue);
                }
            }

            Name = scriptElement.Attribute("name").Value;
            Description = scriptElement.Attribute("description").Value;
            Path = scriptElement.Attribute("path").Value;
            InSystemTray = Convert.ToBoolean(scriptElement.Attribute("inSystemTray").Value);

            var timeScheduleElement = scriptElement.Element("TimeSchedule");
            if (timeScheduleElement != null)
            {
                TimeScheduleConfiguration = new TimeScheduleConfiguration();
                TimeScheduleConfiguration.Parse(timeScheduleElement);
            }
        }

        public override XElement Save()
        {
            throw new NotImplementedException();
        }
    }
}
