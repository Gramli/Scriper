using ScriperLib.Configuration;
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

        private Func<IEnumerable<IOutput>> _scriptOutputs;

        private readonly Dictionary<ScriptType, Func<IScriptConfiguration, IScript>> scriptInicializer = new Dictionary<ScriptType, Func<IScriptConfiguration, IScript>>
        {
            {ScriptType.WindowsProcess, new Func<IScriptConfiguration, IScript>((scriptConfiguration) => new BatchScript(scriptConfiguration)) },
            {ScriptType.ExeFile, new Func<IScriptConfiguration, IScript>((scriptConfiguration) => new ExeFile(scriptConfiguration)) },
            {ScriptType.PowerShell1, new Func<IScriptConfiguration, IScript>((scriptConfiguration) => new PowerShellScript_v1(scriptConfiguration)) },
            {ScriptType.PowerShell2, new Func<IScriptConfiguration, IScript>((scriptConfiguration) => new PowerShellScript_v2(scriptConfiguration)) },
        };

        public ScriptManager(IScriptManagerConfiguration configuration,
            IEnumerable<IScriptRunner> scriptRunners,
            Func<IEnumerable<IOutput>> scriptOutputs)
        {
            Configuration = configuration;
            _scriptRunners = scriptRunners;
            _scriptOutputs = scriptOutputs;
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
            var script = CreateScript(scriptConfiguration);
            var scriptRunner = _scriptRunners.Single(runner => runner.ScriptTypes.Contains(script.ScriptType));
            _scripts.Add(script, scriptRunner);
        }

        public bool RemoveScript(IScript script)
        {
            return _scripts.Remove(script);
        }

        public void RunScript(IScript script)
        {
            _scripts[script].Run(script);
        }

        public void ReplaceScript(IScript oldScript, IScriptConfiguration newScriptConfiguration)
        {
            AddScript(newScriptConfiguration);
            RemoveScript(oldScript);
        }

        private IScript CreateScript(IScriptConfiguration scriptConfiguration)
        {
            var extension = Path.GetExtension(scriptConfiguration.Path);
            var scriptType = extension.GetScriptType();
            if (scriptInicializer.TryGetValue(scriptType, out var inicializationFunc))
            {
                var script = inicializationFunc.Invoke(scriptConfiguration);
                foreach(var output in _scriptOutputs())
                {
                    switch(output.OutputType)
                    {
                        case OutputType.Console:
                            if(script.Configuration.FileOutputConfiguration is null)
                            {
                                continue;
                            }
                            output.Configuration = script.Configuration.FileOutputConfiguration;
                            break;
                        case OutputType.File:
                            if (script.Configuration.ConsoleOutputConfiguration is null)
                            {
                                continue;
                            }
                            output.Configuration = script.Configuration.ConsoleOutputConfiguration;
                            break;
                    }

                    script.Outputs.Add(output);
                }
            }

            throw new ScriptException($"Do not support or can't recognize script extension {Path.GetFileName(scriptConfiguration.Path)}");
        }
    }
}
