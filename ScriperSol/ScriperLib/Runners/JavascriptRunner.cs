using ScriperLib.Enums;
using System;
using System.Threading.Tasks;

namespace ScriperLib.Runners
{
    internal class JavascriptRunner : IRunner
    {
        public ScriptType[] ScriptTypes => new[] { ScriptType.Javascript };

        public IScriptResult Run(IScript script)
        {
            throw new NotImplementedException();
        }

        public Task<IScriptResult> RunAsync(IScript script)
        {
            throw new NotImplementedException();
        }
    }
}
