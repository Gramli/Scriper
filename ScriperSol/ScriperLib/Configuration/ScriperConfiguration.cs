using System.IO;
using System.Xml.Linq;

namespace ScriperLib.Configuration
{
    public class ScriperConfiguration : IScriperConfiguration
    {
        public IScriptManagerConfiguration ScriptManagerConfiguration { get; private set; }

        private ScriperConfiguration(IScriptManagerConfiguration scriptManagerConfiguration)
        {
            ScriptManagerConfiguration = scriptManagerConfiguration;
        }

        private ScriperConfiguration()
        {
            ScriptManagerConfiguration = new ScriptManagerConfiguration();
        }

        public void Save(string path)
        {
            var scriperEl = new XElement("Scriper");
            ScriptManagerConfiguration.Save(scriperEl);
            File.WriteAllText(path, scriperEl.ToString());
        }

        public static IScriperConfiguration Load(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
            {
                var fileInString = File.ReadAllText(fileName);
                var element = XElement.Parse(fileInString);
                var scriptManagerConfiguration = new ScriptManagerConfiguration(element);
                return new ScriperConfiguration(scriptManagerConfiguration);
            }

            return new ScriperConfiguration();
        }
    }
}
