using ScriperLib.Configuration;

namespace ScriperLib.ScriptScheduler
{
    public interface IScriptSchedulerManager
    {
        void Add(string command, string runnerAppPath, string configPath, IScriptConfiguration scriptConfiguration);
        void Remove(string scriptName);
    }
}
