using ScriperLib.Enums;
using System;

namespace ScriperLib.Core
{
    internal class PowerShellRunner : IScriptRunner
    {
        public ScriptType[] ScriptTypes => new[] { ScriptType.ExeFile, ScriptType.WindowsProcess };

        public void Run(IScript script)
        {
            throw new NotImplementedException();
        }
    }
}
