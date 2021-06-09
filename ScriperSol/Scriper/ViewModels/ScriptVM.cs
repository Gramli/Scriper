using System.Linq;
using System.Reactive;
using ReactiveUI;
using Scriper.Views;
using ScriperLib;
using ScriperLib.Configuration;
using ScriperLib.Enums;

namespace Scriper.ViewModels
{
    public class ScriptVM : ViewModelBase
    {
        public IScriptConfiguration ScriptConfiguration => Script.Configuration;

        public ScriptType ScriptType => Script.ScriptType; 

        public IScript Script { get; private set; }

        private string _lastRun;
        public string LastRun
        {
            get => ScriptConfiguration.LastRun;
            set
            {
                ScriptConfiguration.LastRun = value;
                this.RaiseAndSetIfChanged(ref _lastRun, value);
            }
        }

        public ScriptVM(IScript script)
        {
            Script = script;
        }
    }
}
