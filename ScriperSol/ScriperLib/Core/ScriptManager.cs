using ScriperLib.Configuration;
using ScriperLib.Exceptions;
using ScriperLib.Scripts;
using System;
using System.Collections.Generic;
using System.IO;

namespace ScriperLib.Core
{
    public class ScriptManager : IScriptManager
    {
        private readonly Dictionary<string, Func<IScriptConfiguration, IScript>> scriptInicializer = new Dictionary<string, Func<IScriptConfiguration, IScript>>
        {
            {"bat", new Func<IScriptConfiguration, IScript>((scriptConfiguration) => new BatchScript(scriptConfiguration)) },
            {"ps1", new Func<IScriptConfiguration, IScript>((scriptConfiguration) => new PowerShellScript(scriptConfiguration)) },
            {"ps2", new Func<IScriptConfiguration, IScript>((scriptConfiguration) => new PowerShellScript(scriptConfiguration)) },
        };
        public IScriptManagerConfiguration Configuration { get; private set; }

        public List<IScript> Scripts { get; private set; }

        public ScriptManager(IScriptManagerConfiguration configuration)
        {
            Configuration = configuration;
            LoadScripts();
        }
        private void LoadScripts()
        {
            Scripts = new List<IScript>(Configuration.ScriptsConfigurations.Count);
            foreach(var scriptConfiguration in Configuration.ScriptsConfigurations)
            {
                var extension = Path.GetExtension(scriptConfiguration.Path);
                if(scriptInicializer.TryGetValue(extension, out var inicializationFunc))
                {
                    var script = inicializationFunc.Invoke(scriptConfiguration);
                    Scripts.Add(script);
                    continue;
                }

                throw new ScriptException($"Do not support or can't recognize script extension {Path.GetFileName(scriptConfiguration.Path)}");
            }
        }
    }
}
