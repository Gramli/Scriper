using System.Xml.Linq;

namespace ScriperLib.Configuration
{
    public interface IConfigurationElement
    {
        void Parse(XElement element);
        XElement Save();
    }
}
