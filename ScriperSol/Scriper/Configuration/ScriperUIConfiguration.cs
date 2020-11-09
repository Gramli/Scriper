using ScriperLib.Configuration.Attributes;
using ScriperLib.Configuration.Base;
using ScriperLib.Exceptions;
using System;
using System.IO;
using System.Xml.Linq;

namespace Scriper.Configuration
{
    [Serializable]
    internal class ScriperUIConfiguration : ConfigurationElement, IScriperUIConfiguration
    {
        [ConfigurationElement("TextEditor")]
        public ITextEditorConfiguration TextEditor { get; set; }

        private ScriperUIConfiguration(XElement source)
            :base(source)
        {

        }

        public void Save(string path)
        {
            var scriperEl = new XElement("ScriperUI");
            Save(scriperEl);
            File.WriteAllText(path, scriperEl.ToString());
        }

        public static IScriperUIConfiguration Load(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
            {
                var fileInString = File.ReadAllText(fileName);
                var element = XElement.Parse(fileInString);
                return new ScriperUIConfiguration(element);
            }

            throw new ConfigurationException("FileName is empty or does not exist's");
        }
    }
}
