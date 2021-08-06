using Scriper.RunModes;

namespace Scriper
{
    class Program
    {
        public static void Main(string[] args) =>
            new RunModeFactory().
            CreateRunMode(args).
            Run();
    }
}
