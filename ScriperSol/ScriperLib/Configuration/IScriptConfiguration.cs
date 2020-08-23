using ScriperLib.Enums;
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
        ICollection<IOutputConfiguration> OutputTypes { get; }
        ICollection<ITimeScheduleConfiguration> TimeScheduleConfigurations { get; }

    }
}
