using System.Threading.Tasks;

namespace ScriperLib.ScriptScheduler
{
    public interface IScriptTaskSchedulerRunner
    {
        IScriptResult Run(string scriptName);
        Task<IScriptResult> RunAsync(string scriptName);
    }
}
