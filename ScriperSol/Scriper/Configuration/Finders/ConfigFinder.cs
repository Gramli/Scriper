using System.Collections.Generic;
using System.IO;

namespace Scriper.Configuration.Finders
{
    public abstract class ConfigFinder
    {
        protected readonly string _configFolderName = "Config";

        protected List<string> FindConfigs(string configNameEnd)
        {
            var result = new List<string>();
            var dirName = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), _configFolderName);
            var fileNames = Directory.GetFiles(dirName);

            foreach (var fileName in fileNames)
            {
                if (fileName.EndsWith(configNameEnd))
                {
                    result.Add(fileName);
                }
            }

            return result;
        }

        public abstract string FindConfig();
        public abstract IList<string> FindConfigs();
    }
}
