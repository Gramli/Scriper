using ScriperLib.Configuration;

namespace ScriperLib.ScriptScheduler
{
    public interface IScriptSchedulerManager
    {
        void Add(IScriptConfiguration scriptConfiguration);
        void Remove(string scriptName);

        ITimeScheduleConfiguration Get(string scriptName);
    }
}
