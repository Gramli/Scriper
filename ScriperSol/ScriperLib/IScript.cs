using ScriperLib.Configuration;
using ScriperLib.Core;
using ScriperLib.Enums;

namespace ScriperLib
{
    public interface IScript
    {
        ScriptType ScriptType {get;}
        IScriptConfiguration Configuration { get; }

        IOutput[] Outputs { get; }
    }
}
