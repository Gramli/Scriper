using System;
using System.Collections.Generic;
using System.Text;

namespace ScriperLib.Configuration
{
    internal class XmlRepresentationAttribute : Attribute
    {
        public string Name { get; private set; }
        public bool IsElement { get; private set; }

        public XmlRepresentationAttribute(string name, bool isElement = false)
        {
            Name = name;
            IsElement = isElement;
        }
    }
}
