using System;
using System.Collections.Generic;
using System.Text;
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

        protected abstract void Parse(XElement element);
        public abstract XElement Save();
    }
}
