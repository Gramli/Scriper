﻿using ScriperLib.Configuration;
using System.Collections.Generic;

namespace ScriperLib
{
    public interface IScriptManager
    {
        IScriptManagerConfiguration Configuration { get; }
        IReadOnlyCollection<IScript> Scripts { get; }

        void AddScript(IScriptConfiguration scriptConfiguration);

        bool RemoveScript(IScript script);

        void RunScript(IScript script);
    }
}
