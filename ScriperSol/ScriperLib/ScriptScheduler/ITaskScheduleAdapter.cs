using ScriperLib.Configuration;

namespace ScriperLib.ScriptScheduler
{
    public interface ITaskScheduleAdapter
    {
        void Register(IScriptConfiguration scriptConfiguration);
        void Delete(string scriptName);

        void Get(string scriptName);
    }
}
