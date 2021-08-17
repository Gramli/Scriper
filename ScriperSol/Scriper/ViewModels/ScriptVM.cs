using ReactiveUI;
using ScriperLib;
using ScriperLib.Configuration;
using ScriperLib.Enums;

namespace Scriper.ViewModels
{
    public class ScriptVM : ViewModelBase, IScriptVM
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
