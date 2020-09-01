using ScriperLib.Enums;

namespace ScriperLib.Core
{
    public interface IOutput
    {
        OutputType OutputType { get; }
        void WriteOutput(string outputText);
    }
}
