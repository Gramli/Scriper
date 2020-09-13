using ScriperLib.Configuration.Base;
using ScriperLib.Core;
using ScriperLib.Enums;
using System;

namespace Scriper.ViewModels
{
    public class OutputVM : ViewModelBase, IOutput
    {
        public OutputType OutputType => OutputType.Console;

        public string Text { get; private set; }
        public IConfigurationElement Configuration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        OutputType IOutput.OutputType { get => throw new NotImplementedException(); }

        public void InitFromConfiguration(IConfigurationElement configuration)
        {
            throw new NotImplementedException();
        }

        public void WriteOutput(string outputText)
        {
        }
    }
}
