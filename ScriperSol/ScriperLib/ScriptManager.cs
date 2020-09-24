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
    internal class ScriptManager : IScriptManager
    {
        public IScriptManagerConfiguration Configuration { get; private set; }
        public IReadOnlyCollection<IScript> Scripts => _scripts.Keys;

        private Dictionary<IScript, IScriptRunner> _scripts;

        private IEnumerable<IScriptRunner> _scriptRunners;

        private Func<IEnumerable<IOutput>> _scriptOutputs;

        private Func<IEnumerable<IScript>> _scrips;

        public ScriptManager(IScriptManagerConfiguration configuration,
            IEnumerable<IScriptRunner> scriptRunners,
            Func<IEnumerable<IScript>> scrips,
            Func<IEnumerable<IOutput>> scriptOutputs)
        {
            Configuration = configuration;
            _scriptRunners = scriptRunners;
            _scriptOutputs = scriptOutputs;
            _scrips = scrips;
            LoadScripts();
        }
        private void LoadScripts()
        {
            _scripts = new Dictionary<IScript, IScriptRunner>(Configuration.ScriptsConfigurations.Count);
            foreach (var scriptConfiguration in Configuration.ScriptsConfigurations)
            {
                AddScript(scriptConfiguration);
            }
        }

        public void AddScript(IScriptConfiguration scriptConfiguration)
        {
            var script = CreateScript(scriptConfiguration);
            AddScript(script);
        }

        public void AddScript(IScript script)
        {
            if (_scripts.Keys.Any(key => key.Configuration.Name == script.Configuration.Name))
            {
                throw new ConfigurationException($"In Configuration are two scripts with same name: {script.Configuration.Name}.");
            }

            var scriptRunner = _scriptRunners.Single(runner => runner.ScriptTypes.Contains(script.ScriptType));
            _scripts.Add(script, scriptRunner);

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

        public void RunScript(IScript script)
        {
            _scripts[script].Run(script);
        }

        public void ReplaceScript(IScript oldScript, IScript newScript)
        {
            if(_scripts.Keys.Any(script=> script.Configuration.Name == newScript.Configuration.Name && script != oldScript))
            {
                throw new ConfigurationException($"Can't replace script:{oldScript.Configuration.Name} because scripts with same name already exists: {newScript.Configuration.Name}.");
            }

            RemoveScript(oldScript);
            AddScript(newScript);
        }

        public IScript CreateScript(IScriptConfiguration scriptConfiguration)
        {
            var extension = Path.GetExtension(scriptConfiguration.Path);
            var scriptType = extension.GetScriptType();
            var script = _scrips().Single(item => item.ScriptType == scriptType);
            script.InitFromConfiguration(scriptConfiguration, _scriptOutputs);
            return script;
        }
    }
}
