using ScriperLib.Configuration.Base;
using System;

namespace ScriperLib.Configuration.Outputs
{
    public interface IConsoleOutputConfiguration : IConfigurationElement
    {
        string Background { get; set; }

        string Foreground { get; set; }
    }
}
