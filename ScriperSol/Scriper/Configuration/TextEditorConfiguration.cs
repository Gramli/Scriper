using ScriperLib.Configuration.Attributes;
using ScriperLib.Configuration.Base;
using System;
using System.Xml.Linq;

namespace Scriper.Configuration
{
    [Serializable]
    public class TextEditorConfiguration : ConfigurationElement, ITextEditorConfiguration
    {
        [ConfigurationAttribute("path")]
        public string Path { get; set; }

        public TextEditorConfiguration(XElement sourceElement)
            : base(sourceElement)
        {
        }
    }
}
