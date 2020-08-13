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

        public static ScriperConfiguration Load(string fileName)
        {
            var fileInString = File.ReadAllText(fileName);
            var element = XElement.Parse(fileInString);
            var scriptManagerConfiguration = new ScriptManagerConfiguration(element);
            return new ScriperConfiguration(scriptManagerConfiguration);
        }
    }
}
