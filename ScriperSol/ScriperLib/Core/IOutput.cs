using ScriperLib.Configuration.Base;
using ScriperLib.Enums;

namespace ScriperLib.Core
{
    public interface IOutput
    {
        IConfigurationElement Configuration { get; set; }
        OutputType OutputType { get; set; }
        void WriteOutput(string outputText);
    }
}
