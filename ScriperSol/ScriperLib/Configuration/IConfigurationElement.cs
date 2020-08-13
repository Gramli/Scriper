using System.Xml.Linq;

namespace ScriperLib.Configuration
{
    public interface IConfigurationElement
    {
        XElement Save();
    }
}
