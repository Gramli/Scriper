using ScriperLib.Configuration;
using System.Collections.Generic;

namespace ScriperLib
{
    public interface IScriptManager
    {
        IScriptManagerConfiguration Configuration { get; }
        IReadOnlyCollection<IScript> Scripts { get; }

        void AddScript(IScriptConfiguration scriptConfiguration);

        void ReplaceScript(IScript oldScript, IScriptConfiguration newScriptConfiguration);

        bool RemoveScript(IScript script);

        void RunScript(IScript script);
    }
}
