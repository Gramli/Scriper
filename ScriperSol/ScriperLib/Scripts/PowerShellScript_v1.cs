using ScriperLib.Enums;
using System;

namespace ScriperLib.Scripts
{
    [Serializable]
    internal class PowerShellScript_v1 : ScriptBase
    {
        public override ScriptType ScriptType => ScriptType.PowerShell1;
    }
}
