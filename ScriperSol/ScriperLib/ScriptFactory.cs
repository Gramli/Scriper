using ScriperLib.Configuration;
using ScriperLib.Exceptions;
using ScriperLib.Extensions;
using ScriperLib.Outputs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ScriperLib
{
    internal class ScriptFactory : IScriptFactory
    {
        private Func<IEnumerable<IOutput>> _scriptOutputs;

        private Func<IEnumerable<IScript>> _scrips;

        public ScriptFactory(Func<IEnumerable<IScript>> scrips, Func<IEnumerable<IOutput>> scriptOutputs)
        {
            _scriptOutputs = scriptOutputs;
            _scrips = scrips;
        }

        public IScript Create(IScriptConfiguration scriptConfiguration)
        {
            if (string.IsNullOrEmpty(scriptConfiguration.Name) || string.IsNullOrEmpty(scriptConfiguration.Path))
            {
                throw new ConfigurationException("Name or path is empty.");
            }

            var extension = Path.GetExtension(scriptConfiguration.Path);
            var scriptType = extension.GetScriptType();
            var script = _scrips().Single(item => item.ScriptType == scriptType);
            script.InitFromConfiguration(scriptConfiguration, _scriptOutputs);
            return script;
        }
    }
}
