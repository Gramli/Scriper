using Avalonia.Media;
using ReactiveUI;
using ScriperLib.Configuration.Base;
using ScriperLib.Enums;
using ScriperLib.Exceptions;
using ScriperLib.Outputs;
using System.Text;
using System.Timers;

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

        private readonly StringBuilder _output;
        private readonly Timer _wait;
        public OutputVM()
        {
            _output = new StringBuilder();
            _wait = new Timer(100);
            _wait.Elapsed += _wait_Elapsed;
        }

        private void _wait_Elapsed(object sender, ElapsedEventArgs e)
        {
            _wait.Stop();
            Text = _output.ToString();
        }

        public void InitFromConfiguration(IConfigurationElement configuration)
        {
            if (configuration is null)
            {
                return;
            }

            throw new ConfigurationException("Expects IConsoleOutputConfiguration.");
        }

        public void WriteOutput(string outputText)
        {
            _output.AppendLine(outputText);
            if(!_wait.Enabled)
            {
                _wait.Start();
            }
        }
    }
}
