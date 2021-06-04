using ScriperLib.Configuration;

namespace ScriperLib.ScriptScheduler
{
    public interface ITaskScheduleAdapter
    {
        void Register(string runnerExe, string arguments, IScriptConfiguration scriptConfiguration);
        void Delete(string scriptName);
    }
}
