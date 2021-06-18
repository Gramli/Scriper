using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Media;
using ReactiveUI;
using Scriper.Closing;
using Scriper.Extensions;
using ScriperLib;
using ScriperLib.Configuration;
using ScriperLib.Configuration.Outputs;
using ScriperLib.Extensions;
using System.IO;
using System.Linq;
using System.Reactive;
using Scriper.AssetsAccess;
using Scriper.Views;

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
                ScriptConfiguration.Path = value;
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

        private AvaloniaAssets AvaloniaAssets => AvaloniaAssets.Instance;

        public AddEditScriptVM(IScriperLibContainer container, IScriptConfiguration scriptConfiguration)
           : this(container)
        {
            ScriptConfiguration = scriptConfiguration;
        }

        private AddEditScriptVM(IScriperLibContainer container)
        {
            _container = container;
            _scriptCreator = container.GetInstance<IScriptCreator>();
            CancelCmd = ReactiveCommand.Create(Cancel);
            OkCmd = ReactiveCommand.Create(Ok);
            OpenFileCmd = ReactiveCommand.Create<string>(OpenFile);
            EditTimeScheduleCmd = ReactiveCommand.Create(EditTimeSchedule);
        }

        public void Cancel()
        {
            Close.Invoke(this, new CloseEventArgs<IScript>());
        }

        public void Ok()
        {
            if (string.IsNullOrEmpty(Name))
            {
                InvalidName("Invalid script name, name is empty.");
                return;
            }

            if (string.IsNullOrEmpty(ConfigPath))
            {
                InvalidateConfigPath("Invalid script path, path is empty.");
                return;
            }

            if (!Path.GetExtension(ConfigPath).TryGetScriptType(out var scriptType))
            {
                InvalidateConfigPath("Uknown script(file) type.");
                return;
            }

            var script = _scriptCreator.Create(ScriptConfiguration);
            Close.Invoke(this, new CloseEventArgs<IScript>(script));
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

        private void InvalidateConfigPath(string message)
        {
            ConfigBackground = Brushes.Salmon;
            ErrorText = message;
        }

        private void EditTimeSchedule()
        {
            var timeScheduleVM = new TimeScheduleVM(_container, ScriptConfiguration.TimeScheduleConfigurations);
            var timeScheduleControl = new TimeScheduleVC(timeScheduleVM);
            var dialogWindow = new DialogWindow(500, 500, "Edit Time Schedule Configuration", timeScheduleControl, AvaloniaAssets.GetAssetsIcon("icons8_file_1.ico"));
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
