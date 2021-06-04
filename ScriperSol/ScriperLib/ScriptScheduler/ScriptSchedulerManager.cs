using ScriperLib.Configuration;
using System;

namespace ScriperLib.ScriptScheduler
{
    public class ScriptSchedulerManager : IScriptSchedulerManager
    {
        private readonly ITaskScheduleAdapter _taskScheduleAdapter;

        public ScriptSchedulerManager(ITaskScheduleAdapter taskScheduleAdapter)
        {
            _taskScheduleAdapter = taskScheduleAdapter;
        }

        public void Add(string runnerAppPath, string configPath, IScriptConfiguration scriptConfiguration)
        {
            var arguments = $"{configPath} {scriptConfiguration.Name}";
            _taskScheduleAdapter.Register(runnerAppPath, arguments, scriptConfiguration);
        }

        public ITimeTriggerConfiguration Get(string scriptName)
        {
            throw new NotImplementedException();
        }

        public void Remove(string scriptName)
        {
            _taskScheduleAdapter.Delete(scriptName);
        }
    }
}
