using Avalonia.Media;
using ReactiveUI;
using ScriperLib.Configuration.Base;
using ScriperLib.Configuration.Outputs;
using ScriperLib.Core;
using ScriperLib.Enums;
using ScriperLib.Exceptions;

namespace Scriper.ViewModels
{
    public class OutputVM : ViewModelBase, IOutput
    {
        public OutputType OutputType => OutputType.Console;

        private IBrush foregroundColor = Brushes.White;
        public IBrush ForegroundColor
        {
            get { return foregroundColor; }
            set
            {
                this.RaiseAndSetIfChanged(ref foregroundColor, value);
            }
        }


        private IBrush backgroundColor = Brushes.Black;
        public IBrush BackgroundColor 
        {
            get { return backgroundColor; }
            set
            {
                this.RaiseAndSetIfChanged(ref backgroundColor, value);
            }
        }

        private string text;
        public string Text
        {
            get { return text; }
            set
            {
                this.RaiseAndSetIfChanged(ref text, value);
            }
        }
        public IConfigurationElement Configuration { get; private set; }

        public void InitFromConfiguration(IConfigurationElement configuration)
        {
            if(configuration is null)
            {
                return;
            }

            if (configuration is IConsoleOutputConfiguration consoleOutputConfiguration)
            {

                Configuration = consoleOutputConfiguration;


                if(!string.IsNullOrEmpty(consoleOutputConfiguration.Foreground))
                {
                    ForegroundColor = Brush.Parse(consoleOutputConfiguration.Foreground);
                }

                if (!string.IsNullOrEmpty(consoleOutputConfiguration.Background))
                {
                    BackgroundColor = Brush.Parse(consoleOutputConfiguration.Background);
                }

                return;
            }

            throw new ConfigurationException("Expects IConsoleOutputConfiguration.");
        }

        public void WriteOutput(string outputText)
        {
            Text += $"\n{outputText}";
        }
    }
}
