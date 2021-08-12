using Avalonia.Controls;
using Avalonia.Media;
using NLog;
using ReactiveUI;
using Scriper.AssetsAccess;
using Scriper.Closing;
using Scriper.Extensions;
using Scriper.ViewModels.Validation;
using Scriper.Views;
using ScriperLib;
using ScriperLib.Configuration;
using ScriperLib.Configuration.Outputs;
using System.Reactive;

namespace Scriper.ViewModels
{
    public class AddEditScriptVM : ViewModelBase, IClose<IScript>
    {
        private string _name;
        public string Name
        {
            get => ScriptConfiguration.Name;
            set
            {
                ScriptConfiguration.Name = value;
                this.RaiseAndSetIfChanged(ref _name, value);
                ClearInvalid();
            }
        }

        private string _errorText;
        public string ErrorText
        {
            get => _errorText;
            set
            {
                if (_errorText != value)
                {
                    this.RaiseAndSetIfChanged(ref _errorText, value);
                }
            }
        }

        private IBrush _nameBackground = Brushes.White;
        public IBrush NameBackground
        {
            get => _nameBackground;
            set
            {
                if (_nameBackground != value)
                {
                    this.RaiseAndSetIfChanged(ref _nameBackground, value);
                }
            }
        }

        private IBrush _configBackground = Brushes.White;
        public IBrush ConfigBackground
        {
            get => _configBackground;
            set
            {
                if (_configBackground != value)
                {
                    this.RaiseAndSetIfChanged(ref _configBackground, value);
                }
            }
        }

        private string _description;
        public string Description
        {
            get => ScriptConfiguration.Description;
            set
            {
                ScriptConfiguration.Description = value;
                this.RaiseAndSetIfChanged(ref _description, value);
            }
        }

        private string _arguments;
        public string Arguments
        {
            get => ScriptConfiguration.Arguments;
            set
            {
                ScriptConfiguration.Arguments = value;
                this.RaiseAndSetIfChanged(ref _arguments, value);
            }
        }

        private string _configPath;
        public string ConfigPath
        {
            get => ScriptConfiguration.Path;
            set
            {
                ScriptConfiguration.Path = value;
                this.RaiseAndSetIfChanged(ref _configPath, value);
                ClearInvalid();
            }
        }

        private bool _fileOutput;
        public bool FileOutput
        {
            get => _fileOutput;
            set
            {
                if (_fileOutput == value)
                {
                    return;
                }

                ScriptConfiguration.FileOutputConfiguration = value ? _container.GetInstance<IFileOutputConfiguration>() : null;
                _fileOutput = value;
                this.RaiseAndSetIfChanged(ref _fileOutput, value);
            }
        }

        private string _fileOutputPath;
        public string FileOutputPath
        {
            get => ScriptConfiguration.FileOutputConfiguration?.Path;
            set
            {
                ScriptConfiguration.FileOutputConfiguration.Path = value;
                this.RaiseAndSetIfChanged(ref _fileOutputPath, value);
            }
        }

        private bool _outputWindow;
        public bool OutputWindow
        {
            get => ScriptConfiguration.OutputWindow;
            set
            {
                ScriptConfiguration.OutputWindow = value;
                this.RaiseAndSetIfChanged(ref _outputWindow, value);
            }
        }

        public IScriptConfiguration ScriptConfiguration { get; private set; }
        public event CloseEventHandler<IScript> Close;
        public ReactiveCommand<Unit, Unit> CancelCmd { get; }
        public ReactiveCommand<Unit, Unit> OkCmd { get; }
        public ReactiveCommand<string, Unit> OpenFileCmd { get; }
        public ReactiveCommand<Unit, Unit> EditTimeScheduleCmd { get; }

        private readonly IScriperLibContainer _container;
        private readonly IScriptCreator _scriptCreator;
        private readonly IScriptFormValidator _scriptFormValidator;

        private AvaloniaAssets AvaloniaAssets => AvaloniaAssets.Instance;

        private static readonly Logger _logger = NLogFactoryProxy.Instance.GetLogger();

        public AddEditScriptVM(IScriperLibContainer container, IScriptConfiguration scriptConfiguration, IScriptFormValidator scriptFormValidator)
           : this(container, scriptFormValidator)
        {
            ScriptConfiguration = scriptConfiguration;
            _scriptFormValidator.AddNameValidator(() => Name, InvalidName);
            _scriptFormValidator.AddConfigValidators(() => ConfigPath, (message) =>
            {
                ConfigBackground = Brushes.Salmon;
                ErrorText = message;
            });
        }

        private AddEditScriptVM(IScriperLibContainer container, IScriptFormValidator scriptFormValidator)
        {
            _container = container;
            _scriptCreator = container.GetInstance<IScriptCreator>();
            CancelCmd = ReactiveCommand.Create(Cancel).CatchError(_logger);
            OkCmd = ReactiveCommand.Create(Ok).CatchError(_logger);
            OpenFileCmd = ReactiveCommand.Create<string>(OpenFile).CatchError(_logger);
            EditTimeScheduleCmd = ReactiveCommand.Create(EditTimeSchedule).CatchError(_logger);
            _scriptFormValidator = scriptFormValidator;
        }

        public void Cancel()
        {
            Close?.Invoke(this, new CloseEventArgs<IScript>());
        }

        public void Ok()
        {
            if(!_scriptFormValidator.Validate())
            {
                return;
            }
            var script = _scriptCreator.Create(ScriptConfiguration);
            Close?.Invoke(this, new CloseEventArgs<IScript>(script));
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

        public void InvalidName(string message)
        {
            NameBackground = Brushes.Salmon;
            ErrorText = message;
        }

        private void EditTimeSchedule()
        {
            var timeScheduleVM = new TimeScheduleVM(_container, ScriptConfiguration.TimeScheduleConfigurations);
            var timeScheduleControl = new TimeScheduleVC(timeScheduleVM);
            var dialogWindow = new DialogWindow(500, 630, "Edit Time Schedule Configuration", timeScheduleControl, AvaloniaAssets.GetAssetsIcon("icons8_schedule.ico"));
            dialogWindow.Closed += (sender, eventArgs) =>
                {
                    ScriptConfiguration.TimeScheduleConfigurations = timeScheduleVM.TimeTriggerConfigurations;
                };
            
            dialogWindow.ShowDialog(App.Current.GetMainWindow());
        }

        private void ClearInvalid()
        {
            NameBackground = Brushes.White;
            ConfigBackground = Brushes.White;
            ErrorText = string.Empty;
        }
    }
}
