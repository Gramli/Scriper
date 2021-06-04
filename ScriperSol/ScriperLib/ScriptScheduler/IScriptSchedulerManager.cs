using ScriperLib.Configuration;

namespace ScriperLib.ScriptScheduler
{
    public interface IScriptSchedulerManager
    {
        void Add(string runnerAppPath, string configPath, IScriptConfiguration scriptConfiguration);
        void Remove(string scriptName);

        ITimeTriggerConfiguration Get(string scriptName);
    }
}
