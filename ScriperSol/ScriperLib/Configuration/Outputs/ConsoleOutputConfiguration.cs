using ScriperLib.Configuration.Attributes;
using ScriperLib.Configuration.Base;
using System;
using System.Xml.Linq;

namespace ScriperLib.Configuration.Outputs
{
    internal class ConsoleOutputConfiguration : ConfigurationElement, IConsoleOutputConfiguration
    {
        [ConfigurationAttribute("color")]
        public ConsoleColor Color { get; set; }
        public ConsoleOutputConfiguration(XElement element)
            : base(element)
        {
        }

        internal ConsoleOutputConfiguration()
        {
        }
    }
}
