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

        public ScriptVM(IScript script)
        {
            Script = script;
        }
    }
}
