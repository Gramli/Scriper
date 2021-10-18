using System.Collections.Generic;

namespace ScriperLib.Arguments
{
    public class PowerShellScriptInputs
    {
        public IList<string> Arguments { get; init; }
        public IDictionary<string,string> Parameters { get; init; }
    }
}
