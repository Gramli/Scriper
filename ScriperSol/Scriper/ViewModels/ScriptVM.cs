using Avalonia.Controls;
using ReactiveUI;
using Scriper.Closing;
using Scriper.Extensions;
using ScriperLib;
using ScriperLib.Configuration;
using ScriperLib.Configuration.Outputs;
using ScriperLib.Enums;
using System;
using System.Reactive;

namespace Scriper.ViewModels
{
    public class ScriptVM : ViewModelBase, IClose<IScript>
    {
        private string name;
        public string Name
        {
            get => ScriptConfiguration.Name;
            set
            {
                ScriptConfiguration.Name = value;
                this.RaiseAndSetIfChanged(ref name, value);
            }
        }

        private string description;
        public string Description
        {
            get => ScriptConfiguration.Description;
            set
            {
                ScriptConfiguration.Description = value;
                this.RaiseAndSetIfChanged(ref description, value);
            }
        }

        private string arguments;
        public string Arguments
        {
            get => ScriptConfiguration.Arguments;
            set
            {
                ScriptConfiguration.Arguments = value;
                this.RaiseAndSetIfChanged(ref arguments, value);
            }
        }

        private string configPath;
        public string ConfigPath
        {
            get => ScriptConfiguration.Path;
            set
            {
                ScriptConfiguration.Path = value;
                this.RaiseAndSetIfChanged(ref configPath, value);
            }
        }

        private bool fileOutput;
        public bool FileOutput
        {
            get => fileOutput;
            set
            {
                if (fileOutput != value)
                {
                    ScriptConfiguration.FileOutputConfiguration = value ? _container.GetInstance<IFileOutputConfiguration>() : null;
                    fileOutput = value;
                    this.RaiseAndSetIfChanged(ref fileOutput, value);
                }
            }
        }

        private string fileOutputPath;
        public string FileOutputPath
        {
            get => ScriptConfiguration.FileOutputConfiguration?.Path;
            set
            {
                ScriptConfiguration.Path = value;
                this.RaiseAndSetIfChanged(ref fileOutputPath, value);
            }
        }

        private bool outputWindow;
        public bool OutputWindow
        {
            get => ScriptConfiguration.OutputWindow;
            set
            {
                ScriptConfiguration.OutputWindow = value;
                this.RaiseAndSetIfChanged(ref outputWindow, value);
            }
        }

        public IScriptConfiguration ScriptConfiguration { get; private set; }

        public ScriptType ScriptType => _script.ScriptType;

        public event CloseEventHandler<IScript> Close;

        public ReactiveCommand<Unit, Unit> CancelCmd { get; }

        public ReactiveCommand<Unit, Unit> OkCmd { get; }

        public ReactiveCommand<string, Unit> OpenFileCmd { get; }

        private IScriperLibContainer _container;

        private IScript _script;

        private Func<IScriptConfiguration, IScript> _createScript;

        public ScriptVM(IScriperLibContainer container, IScript script)
        {
            _container = container;
            _script = script;
            ScriptConfiguration = script.Configuration;
            CancelCmd = ReactiveCommand.Create(Cancel);
            OkCmd = ReactiveCommand.Create(Ok);
            OpenFileCmd = ReactiveCommand.Create<string>(OpenFile);
        }

        public ScriptVM(IScriperLibContainer container, Func<IScriptConfiguration, IScript> createScript, IScriptConfiguration scriptConfiguration)
        {
            _container = container;
            _createScript = createScript;
            ScriptConfiguration = scriptConfiguration;
            CancelCmd = ReactiveCommand.Create(Cancel);
            OkCmd = ReactiveCommand.Create(Ok);
            OpenFileCmd = ReactiveCommand.Create<string>(OpenFile);
        }

        public void Cancel()
        {
            this.Close.Invoke(this, new CloseEventArgs<IScript>());
        }

        public void Ok()
        {
            //TODO MAKE CHECK
            this.Close.Invoke(this, new CloseEventArgs<IScript>(GetScript()));
        }

        public async void OpenFile(string parameter)
        {
            var openFileDialog = new OpenFileDialog()
            {
                AllowMultiple = false,
            };
            var result = await openFileDialog.ShowAsync(App.Current.GetMainWindow());
            if (result != null && result.Length == 1)
            {
                switch (parameter)
                {
                    case "ScriptPath":
                        ConfigPath = result[0];
                        break;
                    case "FileOutputPath":
                        FileOutputPath = result[0];
                        break;
                }
            }
        }

        public IScript GetScript()
        {
            if (_script is null)
            {
                _script = _createScript(ScriptConfiguration);
            }

            return _script;
            
        }
    }
}
