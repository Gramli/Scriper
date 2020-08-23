using ScriperLib.Configuration.Outputs;
using ScriperLib.Enums;
using ScriperLib.Exceptions;
using System.IO;
using System.Linq;
using System.Text;

namespace ScriperLib.Core
{
    internal class FileOutput : IOutput
    {
        public IFileOutputConfiguration Configuration { get; private set; }
        public OutputType OutputType => OutputType.File;
        public void WriteOutput(string outputText)
        {
            using var stream = new FileStream(Configuration.Path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
            using var streamWriter = new StreamWriter(stream, Encoding.UTF8);
            streamWriter.WriteLine(outputText);
        }
    }
}
