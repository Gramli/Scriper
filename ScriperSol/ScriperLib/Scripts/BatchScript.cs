using ScriperLib.Configuration;
using ScriperLib.Core;
using ScriperLib.Enums;
using System;
using System.Collections.Generic;

namespace ScriperLib.Scripts
{
    internal class BatchScript : IScript
    {
        public IScriptConfiguration Configuration { get; private set; }

        public ScriptType ScriptType => ScriptType.WindowsProcess;

        public ICollection<IOutput> Outputs => throw new NotImplementedException();

        public BatchScript(IScriptConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
