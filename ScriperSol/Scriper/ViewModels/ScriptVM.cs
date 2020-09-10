using Avalonia.Controls;
using ReactiveUI;
using Scriper.Closing;
using Scriper.Extensions;
using ScriperLib.Configuration;
using System.Reactive;

namespace Scriper.ViewModels
{
    public class ScriptVM : ViewModelBase, IClose<IScriptConfiguration>
    {
        public IScriptConfiguration ScriptConfiguration { get; private set; }

        public event CloseEventHandler<IScriptConfiguration> Close;

        public ReactiveCommand<Unit, Unit> CancelCmd { get; }

        public ReactiveCommand<Unit, Unit> OkCmd { get; }

        public ReactiveCommand<string, Unit> OpenFileCmd { get; }

        public ScriptVM(IScriptConfiguration scriptConfiguration)
        {
            ScriptConfiguration = scriptConfiguration;
            CancelCmd = ReactiveCommand.Create(Cancel);
            OkCmd = ReactiveCommand.Create(Ok);
            OpenFileCmd = ReactiveCommand.Create<string>(OpenFile);
        }

        public void Cancel()
        {
            this.Close.Invoke(this, new CloseEventArgs<IScriptConfiguration>()) ;
        }

        public void Ok()
        {
            //TODO MAKE CHECK
            this.Close.Invoke(this, new CloseEventArgs<IScriptConfiguration>(ScriptConfiguration));
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
                switch(parameter)
                {
                    case "ScriptPath":
                        ScriptConfiguration.Path = result[0];
                        break;
                    case "FileOutputPath":
                        ScriptConfiguration.FileOutputConfiguration.Path = result[0];
                        break;
                }
            }
        }
    }
}
