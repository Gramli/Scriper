using ScriperLib.Enums;
using System;

namespace ScriperLib.Scripts
{
    [Serializable]
    internal class LinuxShell : ScriptBase
    {
        public override ScriptType ScriptType => ScriptType.LinuxShell;
    }
}
