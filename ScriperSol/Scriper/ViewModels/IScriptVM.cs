using ScriperLib;
using ScriperLib.Configuration;
using ScriperLib.Enums;

namespace Scriper.ViewModels
{
    public interface IScriptVM
    {
        IScriptConfiguration ScriptConfiguration { get; }
        ScriptType ScriptType { get; }
        IScript Script { get; }

        string LastRun { get; set; }
    }
}
