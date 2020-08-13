using System.Collections.Generic;

namespace ScriperLib.Configuration
{
    public interface IScriptManagerConfiguration : IConfigurationElement
    {
        IReadOnlyCollection<IScriptConfiguration> ScriptsConfigurations { get; }
    }
}
