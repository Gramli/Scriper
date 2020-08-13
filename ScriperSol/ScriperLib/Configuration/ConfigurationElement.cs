using ScriperLib.Configuration.Attributes;
using ScriperLib.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace ScriperLib.Configuration
{
    internal abstract class ConfigurationElement : IConfigurationElement
    {
        public ConfigurationElement(XElement element)
        {
            Parse(element);
        }

        public void Parse(XElement element)
        {
            var properties = GetType().GetProperties();
            foreach (var property in properties)
            {
                var attribute = GetAttribute(property);

                switch(attribute)
                {
                    case XmlAttributeAttribute attributeAttribute:
                        SetAttributeProperty(property, attributeAttribute.Name, element);
                        break;
                    case XmlElementAttribute elementAttribute:
                        SetElementProperty(property, elementAttribute.Name, element);
                        break;
                    case XmlCollectionAttribute collectionAttribute:
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
                switch (attribute)
                {
                    case XmlAttributeAttribute attributeAttribute:
                        SetElementAttribute(property, attributeAttribute.Name, element);
                        break;
                    case XmlElementAttribute elementAttribute:
                        SetElementElement(property, elementAttribute.Name, element);
                        break;
                    case XmlCollectionAttribute collectionAttribute:
                        SetElementCollection(property, collectionAttribute, element);
                        break;
                }
            }
        }

        private void SetElementCollection(PropertyInfo property, XmlCollectionAttribute collectionAttribute, XElement element)
        {
            var value = property.GetValue(this);
            var iEnumerable = value as IEnumerable ?? throw new ConfigurationException($"Property {property.Name} is not IEnumerable");

            foreach(var item in iEnumerable)
            {
                var xmlItem = item as IConfigurationElement ?? throw new ConfigurationException($"Item in {property.Name} collection is not ConfigurationElement");
                var itemElement = new XElement(collectionAttribute.CollectionItemName);
                xmlItem.Save(itemElement);
                element.Add(itemElement);
            }
        }

        private void SetElementElement(PropertyInfo property, string name, XElement element)
        {
            var value = property.GetValue(this);
            element.Add(new XElement(name, value));
        }

        private void SetElementAttribute(PropertyInfo property, string name, XElement element)
        {
           var value = property.GetValue(this);
            element.Add(new XAttribute(name, value));
        }

        private Type GetImplementedType(Type parrentType)
        {
            return GetType().Assembly.GetTypes().Single(t => parrentType.IsAssignableFrom(t)
            && !t.IsInterface);
        }

        private XmlRepresentationAttribute GetAttribute(PropertyInfo property)
        {
            return (XmlRepresentationAttribute)property.GetCustomAttributes(typeof(XmlRepresentationAttribute), false).Single();
        }

        private void SetAttributeProperty(PropertyInfo property, string attributeName, XElement element)
        {
            property.SetValue(this, Convert.ChangeType(element.Attribute(attributeName).Value, property.PropertyType));

        }

        private void SetElementProperty(PropertyInfo property, string attributeName, XElement element)
        {
            var propertyValue = CreateInstanceOfProperty(property.PropertyType, attributeName, element);
            property.SetValue(this, propertyValue);
        }

        private object CreateInstanceOfProperty(Type propertyType, string attributeName, XElement element)
        {
            var childElement = element.Element(attributeName);

            var instanceType = propertyType;

            if (propertyType.IsInterface || propertyType.IsAbstract)
            {
                instanceType = GetImplementedType(propertyType);
            }

            return Activator.CreateInstance(instanceType, childElement);

        }

        private void SetCollectionProperty(PropertyInfo property, XmlCollectionAttribute attribute, XElement element)
        {
            var childElements = element.Elements(attribute.Name);

            if(!typeof(ICollection).IsAssignableFrom(property.PropertyType))
            {
                throw new ConfigurationException($"Property {property.Name} do not inherit from ICollection.");
            }

            var argumentType = property.PropertyType.GetGenericArguments().SingleOrDefault() ?? throw new ConfigurationException("Configuration support one generic arguments collection.");


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

            foreach(var childElement in childElements)
            {
                var childItemInstance = CreateInstanceOfProperty(argumentType, attribute.CollectionItemName, childElement);
                addMethod.Invoke(propertyValue, new[] { childItemInstance });
            }

        }
    }
}
