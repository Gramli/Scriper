using System;

namespace ScriperLib.Configuration.Attributes
{
    internal abstract class XmlRepresentationAttribute : Attribute
    {
        public string Name { get; private set; }

        public XmlRepresentationAttribute(string name)
        {
            Name = name;
        }
    }
}
