using ScriperLib.Configuration.Attributes;
using ScriperLib.Configuration.Base;
using System.Xml.Linq;

namespace ScriperLib.Configuration.Outputs
{
    internal class FileOutputConfiguration : ConfigurationElement, IFileOutputConfiguration
    {
        [ConfigurationAttribute("path", true)]
        public string Path { get; set; }
        public FileOutputConfiguration(XElement element) 
            : base(element)
        {
        }

        internal FileOutputConfiguration()
        {
        }
    }
}
