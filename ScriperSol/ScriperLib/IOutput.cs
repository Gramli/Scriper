using ScriperLib.Enums;

namespace ScriperLib
{
    public interface IOutput
    {
        OutputType OutputType { get; }
        void WriteOutput(string outputText);
    }
}
