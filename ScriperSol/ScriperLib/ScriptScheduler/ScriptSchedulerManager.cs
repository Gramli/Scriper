using ScriperLib.Configuration;

namespace ScriperLib.ScriptScheduler
{
    public class ScriptSchedulerManager : IScriptSchedulerManager
    {
        private readonly ITaskScheduleAdapter _taskScheduleAdapter;

        public ScriptSchedulerManager(ITaskScheduleAdapter taskScheduleAdapter)
        {
            _taskScheduleAdapter = taskScheduleAdapter;
        }

        public void Add(string command, string runnerAppPath, string configPath, IScriptConfiguration scriptConfiguration)
        {
            var arguments = $"{command} {configPath} {scriptConfiguration.Name}";
            _taskScheduleAdapter.Register(runnerAppPath, arguments, scriptConfiguration);
        }

        public void Remove(string scriptName)
        {
            _taskScheduleAdapter.Delete(scriptName);
        }
    }
}
