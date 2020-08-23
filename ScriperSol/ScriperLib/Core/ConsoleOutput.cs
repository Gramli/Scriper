using ScriperLib.Configuration.Outputs;
using ScriperLib.Enums;
using System;

namespace ScriperLib.Core
{
    internal class ConsoleOutput : IOutput
    {
        public IConsoleOutputConfiguration Configuration { get; private set; }
        public OutputType OutputType => OutputType.Console;

        public void WriteOutput(string outputText)
        {
            var temp = Console.ForegroundColor;
            if (Configuration.Color.HasValue)
            {
                Console.ForegroundColor = Configuration.Color.Value;
            }

            Console.WriteLine(outputText);
            Console.ForegroundColor = temp;
        }
    }
}
