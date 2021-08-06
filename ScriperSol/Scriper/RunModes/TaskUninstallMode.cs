using ScriperLib.ScriptScheduler;

namespace Scriper.RunModes
{
    internal class TaskUninstallMode : IRunMode
    {
        public void Run()
        {
            var taskScheduler = new TaskScheduleAdapter();
            taskScheduler.DeleteFolder();
        }
    }
}
