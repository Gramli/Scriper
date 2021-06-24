using ScriperLib.Configuration;
using ScriperLib.ScriptScheduler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Scriper.Models
{
    internal class ScriptSchedulerManagerAdapter : IScriptSchedulerManagerAdapter
    {
        private readonly IScriptSchedulerManager _scriptSchedulerManager;
        private readonly string _configPath;
        private readonly string _runnerExe;

        public ScriptSchedulerManagerAdapter(string configPath, IScriptSchedulerManager scriptSchedulerManager)
        {
            _scriptSchedulerManager = scriptSchedulerManager;
            _configPath = configPath;
            _runnerExe = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, System.AppDomain.CurrentDomain.FriendlyName);
        }

        public void Add(IScriptConfiguration scriptConfiguration)
        {
            throw new NotImplementedException();
        }

        public void Remove(string scriptName)
        {
            throw new NotImplementedException();
        }
    }
}
