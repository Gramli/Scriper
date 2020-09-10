using ScriperLib.Configuration;
using ScriperLib.Core;
using ScriperLib.Enums;
using System.Collections;
using System.Collections.Generic;

namespace ScriperLib
{
    public interface IScript
    {
        ScriptType ScriptType {get;}
        IScriptConfiguration Configuration { get; }

        ICollection<IOutput> Outputs { get; }
    }
}
