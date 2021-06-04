using System.Threading.Tasks;

namespace ScriperLib
{
    public interface IScriptRunner
    {
        IScriptResult Run(IScript script);
        Task<IScriptResult> RunAsync(IScript script);
    }
}
