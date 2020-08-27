using ScriperLib.Configuration.Base;
using ScriperLib.Configuration.Outputs;
using System.Collections.Generic;

namespace ScriperLib.Configuration
{
    public interface IScriptConfiguration : IConfigurationElement
    {
        string Name { get; set; }
        string Description { get; set; }
        string Path { get; set; }
        bool InSystemTray { get; set; }
        bool RunInNewWindow { get; set; }
        IConsoleOutputConfiguration ConsoleOutputConfiguration { get; set; }
        IFileOutputConfiguration IFileOutputConfiguration { get; set; }
        ICollection<ITimeScheduleConfiguration> TimeScheduleConfigurations { get; }

    }
}
