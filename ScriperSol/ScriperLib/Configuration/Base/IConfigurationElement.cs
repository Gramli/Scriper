using System;
using System.Xml.Linq;

namespace ScriperLib.Configuration.Base
{
    public interface IConfigurationElement
    {
        public void Parse(XElement element);
        public void Save(XElement element);
    }
}
