using ScriperLib.ScriptScheduler;

namespace Scriper
{
    internal class TaskUninstallMode
    {
        public static void TaskRunnerMain()
        {
            var taskScheduler = new TaskScheduleAdapter();
            taskScheduler.DeleteFolder();
        }
    }
}
