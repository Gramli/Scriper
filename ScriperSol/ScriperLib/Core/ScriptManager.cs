﻿using ScriperLib.Configuration;
using ScriperLib.Enums;
using ScriperLib.Exceptions;
using ScriperLib.Extensions;
using ScriperLib.Scripts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ScriperLib.Core
{
    internal class ScriptManager : IScriptManager
    {
        public IScriptManagerConfiguration Configuration { get; private set; }
        public IReadOnlyCollection<IScript> Scripts => _scripts.Keys;

        private Dictionary<IScript, IScriptRunner> _scripts;

        private IEnumerable<IScriptRunner> _scriptRunners;

        private readonly Dictionary<ScriptType, Func<IScriptConfiguration, IScript>> scriptInicializer = new Dictionary<ScriptType, Func<IScriptConfiguration, IScript>>
        {
            {ScriptType.WindowsProcess, new Func<IScriptConfiguration, IScript>((scriptConfiguration) => new BatchScript(scriptConfiguration)) },
            {ScriptType.ExeFile, new Func<IScriptConfiguration, IScript>((scriptConfiguration) => new ExeFile(scriptConfiguration)) },
            {ScriptType.PowerShell1, new Func<IScriptConfiguration, IScript>((scriptConfiguration) => new PowerShellScript_v1(scriptConfiguration)) },
            {ScriptType.PowerShell2, new Func<IScriptConfiguration, IScript>((scriptConfiguration) => new PowerShellScript_v2(scriptConfiguration)) },
        };

        public ScriptManager(IScriptManagerConfiguration configuration, IEnumerable<IScriptRunner> scriptRunners)
        {
            Configuration = configuration;
            _scriptRunners = scriptRunners;
            LoadScripts();
        }
        private void LoadScripts()
        {
            _scripts = new Dictionary<IScript, IScriptRunner>(Configuration.ScriptsConfigurations.Count);
            foreach(var scriptConfiguration in Configuration.ScriptsConfigurations)
            {
                AddScript(scriptConfiguration);
            }
        }

        public void AddScript(IScriptConfiguration scriptConfiguration)
        {
            var extension = Path.GetExtension(scriptConfiguration.Path);
            var scriptType = extension.GetScriptType();
            if (scriptInicializer.TryGetValue(scriptType, out var inicializationFunc))
            {
                var scriptRunner = _scriptRunners.Single(runner => runner.ScriptTypes.Contains(scriptType));
                var script = inicializationFunc.Invoke(scriptConfiguration);
                _scripts.Add(script, scriptRunner);
                return;
            }

            throw new ScriptException($"Do not support or can't recognize script extension {Path.GetFileName(scriptConfiguration.Path)}");
        }

        public bool RemoveScript(IScript script)
        {
            return _scripts.Remove(script);
        }

        public void RunScript(IScript script)
        {
            _scripts[script].Run(script);
        }
    }
}
