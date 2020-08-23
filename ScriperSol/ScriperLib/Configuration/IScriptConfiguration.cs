using ScriperLib.Configuration.Base;
using ScriperLib.Configuration.Outputs;
using System.Collections.Generic;

namespace ScriperLib.Configuration
{
    public interface IScriptConfiguration : IConfigurationElement
    {
        string Name { get; }
        string Description { get; }
        string Path { get; }
        bool InSystemTray { get; }
        bool RunInNewWindow { get; }
        IConsoleOutputConfiguration ConsoleOutputConfiguration { get; }
        IFileOutputConfiguration IFileOutputConfiguration { get; }
        ICollection<ITimeScheduleConfiguration> TimeScheduleConfigurations { get; }

    }
}
