using ScriperLib;
using ScriperLib.ScriptScheduler;

namespace Scriper
{
    internal class TaskRunnerMode
    {
        public static void TaskRunnerMain(string[] args)
        {
            if (args.Length <= 0)
            {
                return;
            }

            var configPath = args[0];
            var container = new ScriperLibContainer(configPath);
            var runner = container.GetInstance<IScriptTaskSchedulerRunner>();
            var scriptName = string.Join(" ", args[1..]);
            runner.Run(scriptName);
        }
    }
}
