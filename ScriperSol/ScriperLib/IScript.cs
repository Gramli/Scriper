using ScriperLib.Configuration;
using ScriperLib.Core;
using ScriperLib.Enums;
using System;
using System.Collections.Generic;

namespace ScriperLib
{
    public interface IScript
    {
        ScriptType ScriptType {get;}
        IScriptConfiguration Configuration { get; }
        ICollection<IOutput> Outputs { get; }
        void InitFromConfiguration(IScriptConfiguration configuration, Func<IEnumerable<IOutput>> _outputs);
    }
}
