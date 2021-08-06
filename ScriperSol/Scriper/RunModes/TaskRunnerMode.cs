using ScriperLib;
using ScriperLib.ScriptScheduler;
using System.Linq;

namespace Scriper.RunModes
{
    internal class TaskRunnerMode : IRunMode
    {
        private readonly string[] _args;
        public TaskRunnerMode(string[] args)
        {
            _args = args;
        }

        public void Run()
        {
            if (!_args.Any())
            {
                return;
            }

            var configPath = _args[0];
            var container = new ScriperLibContainer(configPath);
            var runner = container.GetInstance<IScriptTaskSchedulerRunner>();
            var scriptName = _args[1];
            runner.Run(scriptName);
        }
    }
}
