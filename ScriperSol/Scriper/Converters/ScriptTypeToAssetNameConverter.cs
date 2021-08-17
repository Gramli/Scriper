using ScriperLib.Enums;
using System;
using System.Collections.Generic;

namespace Scriper.Converters
{
    public class ScriptTypeToAssetNameConverter : IScriptTypeToAssetNameConverter
    {
        private readonly Dictionary<ScriptType, string> _scriptTypeAssetNameDict = new Dictionary<ScriptType, string>()
        {
            { ScriptType.ExeFile, "icons8_application_window_96px.png" },
            { ScriptType.PowerShell1, "icons8_powershell_96px.png" },
            { ScriptType.PowerShell2, "icons8_powershell_96px.png" },
            { ScriptType.PythonFile, "icons8_python_96px.png" },
            { ScriptType.WindowsProcess, "icons8_windows_xp_96px_1.png" },
            { ScriptType.Javascript, "icons8_javascript_96px.png" },
            { ScriptType.LinuxShell, "icons8_linux_96px.png" },
        };

        public string Convert(ScriptType key)
        {
            if(!_scriptTypeAssetNameDict.TryGetValue(key, out var value))
            {
                throw new ArgumentException($"Can't convert {key} to asset name");
            }

            return value;
        }
    }
}
