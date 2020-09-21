using ScriperLib.Enums;
using System;

namespace ScriperLib.Scripts
{
    [Serializable]
    public class PythonScript : ScriptBase
    {
        public override ScriptType ScriptType => ScriptType.PythonFile;
    }
}
