using ScriperLib.Enums;

namespace ScriperLib
{
    public interface IOutputManager
    {
        void WriteOutput(string outputText, OutputType[] outputTypes, params object[] args);
    }
}
