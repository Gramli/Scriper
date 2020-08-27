using ScriperLib.Configuration.Attributes;
using ScriperLib.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace ScriperLib.Configuration.Base
{
    /// <summary>
    /// Base element for configuration
    /// Use reflection and Attributes to regognize what to parse or save
    /// </summary>
    internal abstract class ConfigurationElement : IConfigurationElement
    {
        public ConfigurationElement(XElement element)
        {
            Parse(element);
        }

        protected internal ConfigurationElement()
        {

        }

        public void Parse(XElement element)
        {
            var properties = GetType().GetProperties();
            foreach (var property in properties)
            {
                var attribute = GetAttribute(property);

                switch (attribute)
                {
                    case ConfigurationAttributeAttribute attributeAttribute:
                        SetAttributeProperty(property, attributeAttribute.Name, element);
                        break;
                    case ConfigurationElementAttribute elementAttribute:
                        SetElementProperty(property, elementAttribute.Name, element);
                        break;
                    case ConfigurationCollectionAttribute collectionAttribute:
                        SetCollectionProperty(property, collectionAttribute, element);
                        break;
                }
            }
        }

        public void Save(XElement element)
        {
            var properties = GetType().GetProperties();
            foreach (var property in properties)
            {
                var attribute = GetAttribute(property);
                XObject childElement;
                switch (attribute)
                {
                    case ConfigurationAttributeAttribute attributeAttribute:
                        childElement = CreateElementAttribute(property, attributeAttribute.Name);
                        break;
                    case ConfigurationElementAttribute elementAttribute:
                        childElement = CreateElementElement(property, elementAttribute.Name);
                        break;
                    case ConfigurationCollectionAttribute collectionAttribute:
                        childElement = CreateElementCollection(property, collectionAttribute);
                        break;
                    default:
                        throw new ConfigurationException("Can't recoginze attribute.");
                }
                element.Add(childElement);
            }
        }

        /// <summary>
        /// Create new collection element with items
        /// </summary>
        private XElement CreateElementCollection(PropertyInfo property, ConfigurationCollectionAttribute collectionAttribute)
        {
            var value = property.GetValue(this);
            var iEnumerable = value as IEnumerable ?? throw new ConfigurationException($"Property {property.Name} is not IEnumerable");

            var collectionElement = new XElement(collectionAttribute.Name);
            foreach (var item in iEnumerable)
            {
                var itemType = item.GetType();
                if (itemType.IsValueType)
                {
                    collectionElement.Add(new XElement(collectionAttribute.CollectionItemName, item));
                }
                else
                {
                    var childElement = CreateConfigurationElement(item, collectionAttribute.CollectionItemName);
                    collectionElement.Add(childElement);
                }
            }

            return collectionElement;
        }

        /// <summary>
        /// Create XElement from property value
        /// </summary>
        private XElement CreateElementElement(PropertyInfo property, string name)
        {
            var value = property.GetValue(this);
            if (property.PropertyType.IsValueType)
            {
                return new XElement(name, value);
            }

            return CreateConfigurationElement(value, name);
        }

        /// <summary>
        /// Create new XElement from value. Expects that value is IConfigurationElement ann call Save method in it, otherwise throws exception
        /// </summary>
        private XElement CreateConfigurationElement(object value, string elementName)
        {
            var xmlItem = value as IConfigurationElement ?? throw new ConfigurationException($"Item in {value} is not ConfigurationElement");
            var itemElement = new XElement(elementName);
            xmlItem.Save(itemElement);
            return itemElement;
        }

        /// <summary>
        /// Create XAttribute from property value
        /// </summary>
        private XAttribute CreateElementAttribute(PropertyInfo property, string name)
        {
            var value = property.GetValue(this);
            return new XAttribute(name, value);
        }

        private Type GetImplementedType(Type parrentType)
        {
            return GetType().Assembly.GetTypes().Single(t => parrentType.IsAssignableFrom(t)
            && !t.IsInterface);
        }

        private ConfigurationBaseAttribute GetAttribute(PropertyInfo property)
        {
            return (ConfigurationBaseAttribute)property.GetCustomAttributes(typeof(ConfigurationBaseAttribute), false).SingleOrDefault() ??
                throw new ConfigurationException("Expects only one ConfigurationBaseAttribute");
        }

        private void SetAttributeProperty(PropertyInfo property, string attributeName, XElement element)
        {
            property.SetValue(this, Convert.ChangeType(element.Attribute(attributeName).Value, property.PropertyType));

        }

        private void SetElementProperty(PropertyInfo property, string attributeName, XElement element)
        {
            var childElement = element.Element(attributeName);
            var propertyValue = CreateInstanceOfProperty(property.PropertyType, childElement);
            property.SetValue(this, propertyValue);
        }

        private object CreateInstanceOfProperty(Type propertyType, XElement element)
        {
            if (propertyType.IsValueType)
            {
                return propertyType.IsEnum ? Enum.Parse(propertyType, element.Value) : Convert.ChangeType(element.Value, propertyType, CultureInfo.InvariantCulture);
            }

            var instanceType = propertyType;

            if (propertyType.IsInterface || propertyType.IsAbstract)
            {
                instanceType = GetImplementedType(propertyType);
            }

            return Activator.CreateInstance(instanceType, element);


        }

        private void SetCollectionProperty(PropertyInfo property, ConfigurationCollectionAttribute attribute, XElement element)
        {
            var childElements = element.Descendants(attribute.Name);

            if (!typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
            {
                throw new ConfigurationException($"Property {property.Name} do not inherit from ICollection.");
            }

            var argumentType = property.PropertyType.GetGenericArguments().SingleOrDefault() ?? throw new ConfigurationException("Configuration support one generic arguments collection.");

            //create collection instance
            object propertyValue;
            var addMethodType = property.PropertyType;
            if (property.PropertyType.IsInterface)
            {
                var constructed = typeof(List<>).MakeGenericType(argumentType);
                propertyValue = Activator.CreateInstance(constructed);
                addMethodType = constructed;
            }
            else
            {
                propertyValue = Activator.CreateInstance(property.PropertyType);
            }

            property.SetValue(this, propertyValue);
            
            var addMethod = addMethodType.GetMethod("Add") ?? throw new ConfigurationException($"Cant find Add method in {property.PropertyType} collection.");

            foreach (var childElement in childElements.Descendants(attribute.CollectionItemName))
            {
                var childItemInstance = CreateInstanceOfProperty(argumentType, childElement);
                addMethod.Invoke(propertyValue, new[] { childItemInstance });
            }

        }
    }
}
