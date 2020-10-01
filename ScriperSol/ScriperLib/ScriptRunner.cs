using ScriperLib.Exceptions;
using ScriperLib.Runners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScriperLib
{
    internal class ScriptRunner : IScriptRunner
    {
        private readonly IEnumerable<IRunner> _runners;
        public ScriptRunner(IEnumerable<IRunner> runners)
        {
            _runners = runners;
        }

        public IScriptResult Run(IScript script)
        {
            var scriptRunner = _runners.SingleOrDefault(runner => runner.ScriptTypes.Contains(script.ScriptType)) 
                ?? throw new ScriptException("Can't run script, script runner does not exists.");

            return scriptRunner.Run(script);
        }

        public Task<IScriptResult> RunAsync(IScript script)
        {
            return Task.Factory.StartNew(() => Run(script));
        }
    }
}
