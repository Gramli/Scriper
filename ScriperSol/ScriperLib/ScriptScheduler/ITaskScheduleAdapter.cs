using ScriperLib.Configuration;

namespace ScriperLib.ScriptScheduler
{
    public interface ITaskScheduleAdapter
    {
        void Register(IScriptConfiguration scriptConfiguration);
        void Delete(string scriptName);
        IScriptConfiguration Get(string scriptName);
    }
}
