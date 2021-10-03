using Avalonia.Media.Imaging;
using ReactiveUI;
using ScriperLib;
using ScriperLib.Configuration;

namespace Scriper.ViewModels.Script
{
    public class ScriptVM : ViewModelBase, IScriptVM
    {
        public IScriptConfiguration ScriptConfiguration => Script.Configuration;
        public IScript Script { get; }
        public IBitmap ScriptImage { get; }

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

        public ScriptVM(IScript script, IBitmap scriptImage)
        {
            Script = script;
            ScriptImage = scriptImage;
        }
    }
}
