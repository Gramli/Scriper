using ScriperLib.Configuration.Base;
using ScriperLib.Configuration.Outputs;
using ScriperLib.Enums;
using System.IO;
using System.Text;

namespace ScriperLib.Core
{
    internal class FileOutput : IOutput
    {
        public IConfigurationElement Configuration => _fileOutputConfiguration;
        public OutputType OutputType => OutputType.File;

        private IFileOutputConfiguration _fileOutputConfiguration;

        public void InitFromConfiguration(IConfigurationElement configuration)
        {
            _fileOutputConfiguration = (IFileOutputConfiguration)configuration;
        }

        public async void WriteOutput(string outputText)
        {
            using var stream = new FileStream(_fileOutputConfiguration.Path, FileMode.Append, FileAccess.Write, FileShare.Write);
            using var streamWriter = new StreamWriter(stream);
            await streamWriter.WriteLineAsync(outputText);
        }
    }
}
