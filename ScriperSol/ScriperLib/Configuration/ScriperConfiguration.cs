using System.IO;
using System.Xml.Linq;

namespace ScriperLib.Configuration
{
    public class ScriperConfiguration : IScriperConfiguration
    {
        private string fileName = "defaultScriper.config";
        public IScriptManagerConfiguration ScriptManagerConfiguration { get; private set; }

        private ScriperConfiguration(string fileName, IScriptManagerConfiguration scriptManagerConfiguration)
        {
            this.fileName = fileName;
            ScriptManagerConfiguration = scriptManagerConfiguration;
        }

        private ScriperConfiguration()
        {
            ScriptManagerConfiguration = new ScriptManagerConfiguration();
        }

        public void Save()
        {
            var scriperEl = new XElement("Scriper");
            ScriptManagerConfiguration.Save(scriperEl);
            File.WriteAllText(fileName, scriperEl.ToString());
        }

        public static IScriperConfiguration Load(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
            {
                var fileInString = File.ReadAllText(fileName);
                var element = XElement.Parse(fileInString);
                var scriptManagerConfiguration = new ScriptManagerConfiguration(element);
                return new ScriperConfiguration(fileName, scriptManagerConfiguration);
            }

            return new ScriperConfiguration();
        }
    }
}
