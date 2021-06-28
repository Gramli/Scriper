using ScriperLib.Configuration;
using System.Collections.Generic;

namespace ScriperLib
{
    public interface IScriptManager
    {
        IScriptManagerConfiguration Configuration { get; }
        IReadOnlyCollection<IScript> Scripts { get; }

        IScript GetScript(string scriptName);

        void AddScript(IScriptConfiguration scriptConfiguration);

        void AddScript(IScript script);

        void ReplaceScript(IScript oldScript, IScript newScript);

        bool RemoveScript(IScript script);
    }
}
