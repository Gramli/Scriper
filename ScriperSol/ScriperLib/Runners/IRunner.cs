using ScriperLib.Enums;
using System.Threading.Tasks;

namespace ScriperLib.Runners
{
    internal interface IRunner
    {
        public ScriptType[] ScriptTypes { get; }
        IScriptResult Run(IScript script);
        Task<IScriptResult> RunAsync(IScript script);
    }
}
