using ReactiveUI;
using Scriper.Closing;
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

        public ScriptVM(IScriptConfiguration scriptConfiguration)
        {
            ScriptConfiguration = scriptConfiguration;
            CancelCmd = ReactiveCommand.Create(Cancel);
            OkCmd = ReactiveCommand.Create(Ok);
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
    }
}
