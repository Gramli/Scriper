using System;

namespace ScriperLib.Configuration.Attributes
{
    internal abstract class ConfigurationBaseAttribute : Attribute
    {
        public string Name { get; private set; }

        public ConfigurationBaseAttribute(string name)
        {
            Name = name;
        }
    }
}
