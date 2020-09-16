using ScriperLib.Configuration.Attributes;
using ScriperLib.Configuration.Base;
using System.Xml.Linq;

namespace ScriperLib.Configuration.Outputs
{
    internal class ConsoleOutputConfiguration : ConfigurationElement, IConsoleOutputConfiguration
    {
        [ConfigurationAttribute("background")]
        public string Background { get; set; }
        [ConfigurationAttribute("foreground")]
        public string Foreground { get; set; }

        public ConsoleOutputConfiguration(XElement element)
            : base(element)
        {
        }

        internal ConsoleOutputConfiguration()
        {
        }
    }
}
