using ScriperLib.Configuration.Base;
using ScriperLib.Enums;

namespace ScriperLib.Core
{
    public interface IOutput
    {
        IConfigurationElement Configuration { get; }
        OutputType OutputType { get; }
        void WriteOutput(string outputText);
        void InitFromConfiguration(IConfigurationElement configuration);
    }
}
