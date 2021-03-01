using ScriperLib.Exceptions;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Scriper.Configuration.Finders
{
    public class ScriperConfigFinder : ConfigFinder
    {
        private readonly string _configNamePostfix = "Scriper.config";
        private readonly string _defaultConfigName = "defaultScriper.config";

        public string GetDefaultConfigPath()
        {
            return Path.Combine(_configFolderName, _defaultConfigName);
        }

        public override string FindConfig()
        {
            return FindConfigs().SingleOrDefault() ?? throw new ConfigurationException($"I found more {_configNamePostfix} configs.");
        }

        public override IList<string> FindConfigs()
        {
            return FindConfigs(_configNamePostfix);
        }
    }
}
