using System.Xml.Linq;

namespace ScriperLib.Configuration
{
    public interface IConfigurationElement
    {
        public void Parse(XElement element);
        public void Save(XElement element);
    }
}
