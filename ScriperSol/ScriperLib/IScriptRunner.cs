using ScriperLib.Enums;
using System.Threading.Tasks;

namespace ScriperLib
{
    internal interface IScriptRunner
    {
        public ScriptType[] ScriptTypes { get; }
        IScriptResult Run(IScript script);

        Task<IScriptResult> RunAsync(IScript script);
    }
}
