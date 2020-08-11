using ScriperLib.Configuration;
using System.Collections.Generic;

namespace ScriperLib
{
    public interface IScriptManager
    {
        IScriptManagerConfiguration Configuration { get; }
        List<IScript> Scripts { get; }
    }
}
