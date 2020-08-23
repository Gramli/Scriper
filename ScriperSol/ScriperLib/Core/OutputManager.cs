using ScriperLib.Enums;
using ScriperLib.Exceptions;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace ScriperLib.Core
{
    internal class OutputManager : IOutputManager
    {
        public void WriteOutput(string outputText, OutputType[] outputTypes, params object[] args)
        {
            foreach(var outputType in outputTypes)
            {
                switch(outputType)
                {
                    case OutputType.Console:
                        WriteToConsole(outputText, args);
                        break;
                    case OutputType.File:
                        WriteToFile(outputText, args);
                        break;
                }
            }
        }

        private void WriteToFile(string outputText, object[] args)
        {
            if(args is null || !args.Any())
            {
                throw new OutputException("Can't write output text to file, arguments are null or empty.");
            }

            var filePath = (string)args[0];
            using var stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
            using var streamWriter = new StreamWriter(stream, Encoding.UTF8);
            streamWriter.WriteLine(outputText);
        }

        private void WriteToConsole(string outputText, object[] args)
        {
            var temp = Console.ForegroundColor;
            if (args != null && args.Any())
            {
                var color = (ConsoleColor)args[0];
                Console.ForegroundColor = color;
            }

            Console.WriteLine(outputText);
            Console.ForegroundColor = temp;
        }
    }
}
