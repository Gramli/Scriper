using System.Collections.Generic;

namespace ScriperLib.Configuration
{
    public interface IScriptManagerConfiguration : IConfigurationElement
    {
        ICollection<IScriptConfiguration> ScriptsConfigurations { get; }
    }
}
