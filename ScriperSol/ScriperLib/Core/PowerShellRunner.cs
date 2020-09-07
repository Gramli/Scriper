using ScriperLib.Enums;
using System;

namespace ScriperLib.Core
{
    internal class PowerShellRunner : IScriptRunner
    {
        public ScriptType[] ScriptTypes => new[] { ScriptType.PowerShell1, ScriptType.PowerShell2 };

        public void Run(IScript script)
        {
            throw new NotImplementedException();
        }
    }
}
