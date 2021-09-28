using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using NLog;
using ReactiveUI;
using Scriper.AssetsAccess;
using Scriper.Closing;
using Scriper.Dialogs;
using Scriper.Extensions;
using Scriper.ImageEditing;
using Scriper.ViewModels.Validation;
using Scriper.Views;
using ScriperLib;
using ScriperLib.Configuration;
using ScriperLib.Configuration.Outputs;
using ScriperLib.Configuration.TimeTrigger;
using System;
using System.Collections.Generic;
using System.Reactive;

namespace Scriper.ViewModels
{
    public class AddEditScriptVM : ViewModelBase, IAddEditScriptVM
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

        private IBrush _scriptPathBackground = Brushes.White;
        public IBrush ScriptPathBackground
        {
            get => _scriptPathBackground;
            set
            {
                if (_scriptPathBackground != value)
                {
                    this.RaiseAndSetIfChanged(ref _scriptPathBackground, value);
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

        private string _scriptPath;
        public string ScriptPath
        {
            get => ScriptConfiguration.Path;
            set
            {
                ScriptConfiguration.Path = value;
                this.RaiseAndSetIfChanged(ref _scriptPath, value);
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

                ScriptConfiguration.FileOutputConfiguration = value ? _fileOutputConfigurationFactory.Create() : null;
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

        private string _iconImagePath;
        public string IconImagePath
        {
            get => ScriptConfiguration.IconImagePath;
            set
            {
                ScriptConfiguration.IconImagePath = value;
                this.RaisePropertyChanged("IconImage");
                this.RaiseAndSetIfChanged(ref _iconImagePath, value);
            }
        }

        public IBitmap IconImage
        {
            get => !string.IsNullOrEmpty(IconImagePath) ? new Bitmap(IconImagePath) : null;
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
        public ReactiveCommand<Unit, Unit> UseDefaultIconCmd { get; }

        private readonly IScriptFactory _scriptCreator;
        private readonly IScriptFormValidator _scriptFormValidator;
        private readonly IFileOutputConfigurationFactory _fileOutputConfigurationFactory;
        private readonly Func<ICollection<ITimeTriggerConfiguration>, ITimeScheduleVM> _createTimeScheduleVM;
        private readonly IScriptIconImageEditor _scriptIconImageEditor;
        private readonly IAssets _assets;
        private readonly IScriperFileDialogOpener _scriperFileDialogOpener;

        public const string OpenFileCmdScriptPath = "ScriptPath";
        public const string OpenFileCmdFileOutputPath = "FileOutputPath";
        public const string OpenFileCmdIcon = "Icon";

        private static readonly Logger _logger = NLogFactoryProxy.Instance.GetLogger();

        public AddEditScriptVM(IScriptFactory scriptCreator,
            IFileOutputConfigurationFactory fileOutputConfigurationFactory,
            IScriptConfiguration scriptConfiguration,
            IScriptFormValidator scriptFormValidator,
            Func<ICollection<ITimeTriggerConfiguration>, ITimeScheduleVM> createTimeScheduleVM,
            IScriptIconImageEditor scriptIconImageEditor,
            IAssets assets,
            IScriperFileDialogOpener scriperFileDialogOpener)
        {
            ScriptConfiguration = scriptConfiguration;
            _scriptCreator = scriptCreator;
            CancelCmd = ReactiveCommand.Create(Cancel).CatchError(_logger);
            OkCmd = ReactiveCommand.Create(Ok).CatchError(_logger);
            OpenFileCmd = ReactiveCommand.Create<string>(OpenFile).CatchError(_logger);
            EditTimeScheduleCmd = ReactiveCommand.Create(EditTimeSchedule).CatchError(_logger);
            UseDefaultIconCmd = ReactiveCommand.Create(SetDefaultIcon).CatchError(_logger);
            _scriptIconImageEditor = scriptIconImageEditor;
            _scriptFormValidator = scriptFormValidator;

            _scriptFormValidator.AddNameValidator(() => Name, InvalidName);
            _scriptFormValidator.AddConfigValidators(() => ScriptPath, (message) =>
            {
                ScriptPathBackground = Brushes.Salmon;
                ErrorText = message;
            });

            _fileOutputConfigurationFactory = fileOutputConfigurationFactory;
            _createTimeScheduleVM = createTimeScheduleVM;
            _assets = assets;
            _scriperFileDialogOpener = scriperFileDialogOpener;
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
            switch (parameter)
            {
                case OpenFileCmdScriptPath:
                    var scriptResult = await _scriperFileDialogOpener.OpenScriptFileDialogAsync();
                    if (scriptResult.ok)
                    {
                        CreateImageInAssets(scriptResult.file);
                    }
                    break;
                case OpenFileCmdFileOutputPath:
                    var fileOutputResult = await _scriperFileDialogOpener.OpenOutputFileDialogAsync();
                    if (fileOutputResult.ok)
                    {
                        CreateImageInAssets(fileOutputResult.file);
                    }
                    break;
                case OpenFileCmdIcon:
                    var imageResult = await _scriperFileDialogOpener.OpenImageFileDialogAsync();
                    if(imageResult.ok)
                    {
                        CreateImageInAssets(imageResult.file);
                    }
                    break;
            }
        }

        private void CreateImageInAssets(string file)
        {
            try
            {
                IconImagePath = _scriptIconImageEditor.CreateImageInAssets(file);
            }
            catch(Exception ex)
            {
                MessageBoxExtensions.ShowDialog(ex.Message);
                _logger.Error(ex);
            }
        }

        public void InvalidName(string message)
        {
            NameBackground = Brushes.Salmon;
            ErrorText = message;
        }

        private void EditTimeSchedule()
        {
            var timeScheduleVM = _createTimeScheduleVM(ScriptConfiguration.TimeScheduleConfigurations);
            var timeScheduleControl = new TimeScheduleVC(timeScheduleVM);
            var dialogWindow = new DialogWindow(500, 630, "Edit Time Schedule Configuration", timeScheduleControl, _assets.GetAssetsImage<WindowIcon>("icons8_schedule.ico"));
            dialogWindow.Closed += (sender, eventArgs) =>
                {
                    ScriptConfiguration.TimeScheduleConfigurations = timeScheduleVM.TimeTriggerConfigurations;
                };
            
            dialogWindow.ShowDialog(App.Current.GetMainWindow());
        }

        private void SetDefaultIcon()
        {
            IconImagePath = string.Empty;
        }

        private void ClearInvalid()
        {
            NameBackground = Brushes.White;
            ScriptPathBackground = Brushes.White;
            ErrorText = string.Empty;
        }
    }
}
