using ScriperLib.Configuration;
using ScriperLib.Exceptions;
using ScriperLib.Scripts;
using System.Collections.Generic;
using System.IO;

namespace ScriperLib
{
    public class ScriptManager : IScriptManager
    {
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
                IScript script;
                switch(extension)
                {
                    case "bat":
                        script = new BatchScript(scriptConfiguration);
                        break;
                    case "ps2":
                    case "ps1":
                        script = new PowerShellScript(scriptConfiguration);
                        break;
                    default: 
                        throw new ScriptException($"Do not support or can't recognize script extension {Path.GetFileName(scriptConfiguration.Path)}");
                }
            }
        }
    }
}
