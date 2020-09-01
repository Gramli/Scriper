using System;

namespace ScriperLib.Configuration.Attributes
{
    internal abstract class ConfigurationBaseAttribute : Attribute
    {
        public string Name { get; private set; }

        public bool Mandatory { get; private set; }

        public ConfigurationBaseAttribute(string name)
        {
            Name = name;
        }

        public ConfigurationBaseAttribute(string name, bool mandatory)
        {
            Name = name;
            Mandatory = mandatory;
        }
    }
}
