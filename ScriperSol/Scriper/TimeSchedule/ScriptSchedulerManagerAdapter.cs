using System;
using System.IO;
using ScriperLib.Configuration;
using ScriperLib.ScriptScheduler;

namespace Scriper.TimeSchedule
{
    internal class ScriptSchedulerManagerAdapter : IScriptSchedulerManagerAdapter
    {
        private readonly IScriptSchedulerManager _scriptSchedulerManager;
        private readonly string _configPath;
        private readonly string _runnerExe;
        private readonly string _command = "-run";

        public ScriptSchedulerManagerAdapter(IScriperConfiguration scriperConfiguration, IScriptSchedulerManager scriptSchedulerManager)
        {
            _scriptSchedulerManager = scriptSchedulerManager;
            _configPath = scriperConfiguration.ConfigPath;
            _runnerExe = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{AppDomain.CurrentDomain.FriendlyName}.exe");
        }

        public void Add(IScriptConfiguration scriptConfiguration)
        {
            _scriptSchedulerManager.Add(_command,_runnerExe, _configPath, scriptConfiguration);
        }

        public void Remove(string scriptName)
        {
            _scriptSchedulerManager.Remove(scriptName);
        }

        public void Replace(IScriptConfiguration scriptConfiguration)
        {
            Remove(scriptConfiguration.Name);
            Add(scriptConfiguration);
        }
    }
}
