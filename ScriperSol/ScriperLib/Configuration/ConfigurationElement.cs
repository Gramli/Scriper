using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace ScriperLib.Configuration
{
    public abstract class ConfigurationElement : IConfigurationElement
    {
        public ConfigurationElement(string rawElement)
        {
            var element = XElement.Parse(rawElement);
            Parse(element);
        }

        public ConfigurationElement(XElement element)
        {
            Parse(element);
        }

        public virtual void Parse(XElement element)
        {
            var properties = this.GetType().GetProperties();
            foreach (var property in properties)
            {
                var attribute = GetAttribute(property);
                if (attribute.IsElement)
                {
                    SetObjectProperty(property, attribute, element);
                }
                else
                {
                    SetSimpleProperty(property, attribute, element);
                }
            }
        }

        public virtual XElement Save()
        {

        }

        private Type GetImplementedType(Type parrentType)
        {
            return GetType().Assembly.GetTypes().Single(t => parrentType.IsAssignableFrom(t) 
            && !t.IsInterface 
            && t.IsAssignableFrom(typeof(ConfigurationElement)));
        }

        private XmlRepresentationAttribute GetAttribute(PropertyInfo property)
        {
            return (XmlRepresentationAttribute)property.GetCustomAttributes(typeof(XmlRepresentationAttribute), false).Single();
        }

        private void SetSimpleProperty(PropertyInfo property, XmlRepresentationAttribute attribute, XElement element)
        {
            if (attribute.IsElement) return;
            property.SetValue(this, Convert.ChangeType(element.Attribute(attribute.Name).Value, property.PropertyType));

        }

        private void SetObjectProperty(PropertyInfo property, XmlRepresentationAttribute attribute, XElement element)
        {
            if (!attribute.IsElement) throw new ArgumentException("XmlRepresentationAttribute is not element!");

            var flags = BindingFlags.NonPublic | BindingFlags.Instance;
            var childElement = element.Element(attribute.Name);

            var instanceType = property.PropertyType;

            if(property.PropertyType.IsInterface || property.PropertyType.IsAbstract)
            {
                instanceType = GetImplementedType(property.PropertyType);
            }

            var propertyValue = Activator.CreateInstance(instanceType, flags, null, childElement, CultureInfo.InvariantCulture);
            property.SetValue(this, propertyValue);
        }
    }
}
