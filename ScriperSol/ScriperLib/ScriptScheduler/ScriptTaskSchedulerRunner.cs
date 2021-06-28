using System;
using System.Threading.Tasks;

namespace ScriperLib.ScriptScheduler
{
    internal class ScriptTaskSchedulerRunner : IScriptTaskSchedulerRunner
    {
        private readonly IScriptManager _scriptManager;
        private readonly IScriptRunner _scriptRunner;

        public ScriptTaskSchedulerRunner(IScriptManager scriptManager, IScriptRunner scriptRunner)
        {
            _scriptManager = scriptManager;
            _scriptRunner = scriptRunner;
        }

        public IScriptResult Run(string scriptName)
        {
            var script = _scriptManager.GetScript(scriptName);
            return _scriptRunner.Run(script);
        }

        public Task<IScriptResult> RunAsync(string scriptName)
        {
            throw new NotImplementedException();
        }
    }
}
