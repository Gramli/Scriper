using ScriperLib.Configuration.Base;
using ScriperLib.Configuration.Outputs;
using ScriperLib.Enums;
using System;

namespace ScriperLib.Core
{
    internal class ConsoleOutput : IOutput
    {
        public OutputType OutputType => OutputType.Console;

        public IConfigurationElement Configuration => _consoleOutputConfiguration;

        private IConsoleOutputConfiguration _consoleOutputConfiguration;

        public void InitFromConfiguration(IConfigurationElement configuration)
        {
            _consoleOutputConfiguration = (IConsoleOutputConfiguration)configuration;
        }

        public void WriteOutput(string outputText)
        {
            var temp = Console.ForegroundColor;
            if (_consoleOutputConfiguration.Color != Console.ForegroundColor)
            {
                Console.ForegroundColor = _consoleOutputConfiguration.Color;
            }

            Console.WriteLine(outputText);
            Console.ForegroundColor = temp;
        }
    }
}
