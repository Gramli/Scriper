using System.Collections.Generic;
using System.IO;

namespace Scriper.Configuration.Finders
{
    public class NLogConfigFinder : ConfigFinder
    {
        private readonly string _nlofConfigName = "nlog.config";

        public override string FindConfig()
        {
            return Path.Combine(_configFolderName, _nlofConfigName);
        }

        public override IList<string> FindConfigs()
        {
            throw new System.NotImplementedException("Do not support more nlog configs.");
        }
    }
}
