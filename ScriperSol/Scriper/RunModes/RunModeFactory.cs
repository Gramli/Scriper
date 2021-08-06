using System.Linq;

namespace Scriper.RunModes
{
    public class RunModeFactory : IRunModeFactory
    {
        public IRunMode CreateRunMode(string[] args)
        {
            if (args.Any())
            {
                var command = args.First();
                switch (command)
                {
                    case "-run":
                        return new TaskRunnerMode(args[1..]);
                    case "-un":
                        return new TaskUninstallMode();
                }
            }

            return new ClassicRunMode(args);
        }
    }
}
