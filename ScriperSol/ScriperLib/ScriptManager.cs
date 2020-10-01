using ScriperLib.Configuration;
using ScriperLib.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace ScriperLib
{
    internal class ScriptManager : IScriptManager
    {
        public IScriptManagerConfiguration Configuration { get; private set; }
        public IReadOnlyCollection<IScript> Scripts => _scripts;

        private HashSet<IScript> _scripts;

        private readonly IScriptCreator _scriptCreator;

        public ScriptManager(IScriptManagerConfiguration configuration, IScriptCreator scriptCreator)
        {
            Configuration = configuration;
            _scriptCreator = scriptCreator;
            LoadScripts();
        }
        private void LoadScripts()
        {
            _scripts = new HashSet<IScript>(Configuration.ScriptsConfigurations.Count);
            foreach (var scriptConfiguration in Configuration.ScriptsConfigurations)
            {
                AddScript(scriptConfiguration);
            }
        }

        public void AddScript(IScriptConfiguration scriptConfiguration)
        {
            var script = _scriptCreator.Create(scriptConfiguration);
            AddScript(script);
        }

        public void AddScript(IScript script)
        {
            if (_scripts.Any(key => key.Configuration.Name == script.Configuration.Name))
            {
                throw new ConfigurationException($"In Configuration are two scripts with same name: {script.Configuration.Name}.");
            }

            _scripts.Add(script);

            if (!Configuration.ScriptsConfigurations.Contains(script.Configuration))
            {
                Configuration.ScriptsConfigurations.Add(script.Configuration);
            }
        }

        public bool RemoveScript(IScript script)
        {
            Configuration.ScriptsConfigurations.Remove(script.Configuration);
            return _scripts.Remove(script);
        }

        public void ReplaceScript(IScript oldScript, IScript newScript)
        {
            if(_scripts.Any(script=> script.Configuration.Name == newScript.Configuration.Name && script != oldScript))
            {
                throw new ConfigurationException($"Can't replace script:{oldScript.Configuration.Name} because scripts with same name already exists: {newScript.Configuration.Name}.");
            }

            RemoveScript(oldScript);
            AddScript(newScript);
        }
    }
}
