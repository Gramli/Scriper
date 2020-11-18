﻿using Avalonia.Controls;
using Avalonia.Media;
using ReactiveUI;
using Scriper.Closing;
using Scriper.Extensions;
using ScriperLib;
using ScriperLib.Configuration;
using ScriperLib.Configuration.Outputs;
using ScriperLib.Extensions;
using System.IO;
using System.Reactive;

namespace Scriper.ViewModels
{
    public class AddEditScriptVM : ViewModelBase, IClose<IScript>
    {
        private string name;
        public string Name
        {
            get => ScriptConfiguration.Name;
            set
            {
                ScriptConfiguration.Name = value;
                this.RaiseAndSetIfChanged(ref name, value);
                ClearInvalid();
            }
        }

        private string errorText;
        public string ErrorText
        {
            get { return errorText; }
            set
            {
                if (errorText != value)
                {
                    this.RaiseAndSetIfChanged(ref errorText, value);
                }
            }
        }

        private IBrush nameBackground = Brushes.White;
        public IBrush NameBackground
        {
            get { return nameBackground; }
            set
            {
                if (nameBackground != value)
                {
                    this.RaiseAndSetIfChanged(ref nameBackground, value);
                }
            }
        }

        private IBrush configBackground = Brushes.White;
        public IBrush ConfigBackground
        {
            get { return configBackground; }
            set
            {
                if (configBackground != value)
                {
                    this.RaiseAndSetIfChanged(ref configBackground, value);
                }
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
                ClearInvalid();
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

        public event CloseEventHandler<IScript> Close;

        public ReactiveCommand<Unit, Unit> CancelCmd { get; }

        public ReactiveCommand<Unit, Unit> OkCmd { get; }

        public ReactiveCommand<string, Unit> OpenFileCmd { get; }

        private readonly IScriperLibContainer _container;

        private readonly IScriptCreator _scriptCreator;

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
        }

        public void Cancel()
        {
            this.Close.Invoke(this, new CloseEventArgs<IScript>());
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
            this.Close.Invoke(this, new CloseEventArgs<IScript>(script));
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

        private void ClearInvalid()
        {
            NameBackground = Brushes.White;
            ConfigBackground = Brushes.White;
            ErrorText = string.Empty;
        }
    }
}