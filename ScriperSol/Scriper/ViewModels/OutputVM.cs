using Avalonia.Media;
using ReactiveUI;
using ScriperLib.Configuration.Base;
using ScriperLib.Enums;
using ScriperLib.Outputs;
using System.Text;
using System.Timers;

namespace Scriper.ViewModels
{
    public class OutputVM : ViewModelBase, IOutputVM
    {
        public OutputType OutputType => OutputType.Console;

        private IBrush _foregroundColor = Brushes.White;
        public IBrush ForegroundColor
        {
            get => _foregroundColor;
            set => this.RaiseAndSetIfChanged(ref _foregroundColor, value);
        }

        private IBrush _backgroundColor = Brushes.Black;
        public IBrush BackgroundColor
        {
            get => _backgroundColor;
            set => this.RaiseAndSetIfChanged(ref _backgroundColor, value);
        }

        private string _text;
        public string Text
        {
            get => _text;
            set => this.RaiseAndSetIfChanged(ref _text, value);
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

            Configuration = configuration;
            //TODO init from configuration
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
