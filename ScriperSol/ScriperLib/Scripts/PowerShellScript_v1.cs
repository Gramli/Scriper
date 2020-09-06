using ScriperLib.Configuration;
using ScriperLib.Core;
using ScriperLib.Enums;
using System;

namespace ScriperLib.Scripts
{
    internal class PowerShellScript_v1 : IScript
    {
        public ScriptType ScriptType => ScriptType.PowerShell1;
        public IScriptConfiguration Configuration { get; private set; }

        public IOutput[] Outputs => throw new NotImplementedException();

        public PowerShellScript_v1(IScriptConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
