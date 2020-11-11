﻿using Avalonia.Controls;
using ReactiveUI;
using Scriper.Closing;
using Scriper.Configuration;
using Scriper.Extensions;
using System.IO;
using System.Reactive;

namespace Scriper.ViewModels
{
    public class SettingsVM : ViewModelBase, IClose<IScriperUIConfiguration>
    {
        public event CloseEventHandler<IScriperUIConfiguration> Close;

        private string textEditorPath;
        public string TextEditorPath
        {
            get => UIConfig.TextEditor.Path;
            set
            {
                UIConfig.TextEditor.Path = value;
                this.RaiseAndSetIfChanged(ref textEditorPath, value);
            }
        }
        public ReactiveCommand<Unit, Unit> CancelCmd { get; }

        public ReactiveCommand<Unit, Unit> OkCmd { get; }

        public ReactiveCommand<string, Unit> OpenFileCmd { get; }

        public IScriperUIConfiguration UIConfig { get; private set; }
        public SettingsVM(IScriperUIConfiguration uiConfig)
        {
            UIConfig = uiConfig;
            CancelCmd = ReactiveCommand.Create(Cancel);
            OkCmd = ReactiveCommand.Create(Ok);
            OpenFileCmd = ReactiveCommand.Create<string>(OpenFile);
        }

        public async void OpenFile(string parameter)
        {
            var openFileDialog = new OpenFileDialog()
            {
                Directory = Path.GetDirectoryName(UIConfig.TextEditor.Path),
                AllowMultiple = false,
            };
            var result = await openFileDialog.ShowAsync(App.Current.GetMainWindow());
            if (result != null && result.Length == 1)
            {
                TextEditorPath = result[0];
            }
        }

        public void Cancel()
        {
            this.Close.Invoke(this, new CloseEventArgs<IScriperUIConfiguration>());
        }

        public void Ok()
        {

            this.Close.Invoke(this, new CloseEventArgs<IScriperUIConfiguration>(UIConfig));
        }
    }
}
