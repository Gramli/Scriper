using ScriperLib.Configuration;
using ScriperLib.Enums;
using ScriperLib.Outputs;
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
