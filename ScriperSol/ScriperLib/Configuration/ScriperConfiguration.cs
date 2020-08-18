using System.IO;
using System.Xml.Linq;

namespace ScriperLib.Configuration
{
    public class ScriperConfiguration : IScriperConfiguration
    {
        private string fileName;
        public IScriptManagerConfiguration ScriptManagerConfiguration { get; private set; }

        private ScriperConfiguration(string fileName, IScriptManagerConfiguration scriptManagerConfiguration)
        {
            this.fileName = fileName;
            ScriptManagerConfiguration = scriptManagerConfiguration;
        }

        public void Save()
        {
            var scriperEl = new XElement("Scriper");
            ScriptManagerConfiguration.Save(scriperEl);
            File.WriteAllText(fileName, scriperEl.ToString());
        }

        public static ScriperConfiguration Load(string fileName)
        {
            var fileInString = File.ReadAllText(fileName);
            var element = XElement.Parse(fileInString);
            var scriptManagerConfiguration = new ScriptManagerConfiguration(element);
            return new ScriperConfiguration(fileName, scriptManagerConfiguration);
        }
    }
}
