using ScriperLib.Enums;
using System;

namespace ScriperLib.Scripts
{
    [Serializable]
    internal class BatchScript : ScriptBase
    {
        public override ScriptType ScriptType => ScriptType.WindowsProcess;
    }
}
