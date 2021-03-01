using ScriperLib.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scriper.Configuration.Finders
{
    public class ScriperUIConfigFinder : ConfigFinder
    {
        private readonly string _configUINameEnd = "ScriperUI.config";

        public override string FindConfig()
        {
            return FindConfigs(_configUINameEnd).SingleOrDefault() ?? throw new ConfigurationException($"I found more {_configUINameEnd} configs.");
        }

        public override IList<string> FindConfigs()
        {
            throw new NotImplementedException("There should be only one UI config.");
        }
    }
}
