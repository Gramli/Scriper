using ScriperLib.Configuration.Base;
using System;

namespace ScriperLib.Configuration.Outputs
{
    public interface IConsoleOutputConfiguration : IConfigurationElement
    {
        ConsoleColor? Color { get; }
    }
}
