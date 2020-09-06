﻿using ScriperLib.Configuration;
using ScriperLib.Core;
using ScriperLib.Enums;
using System;

namespace ScriperLib.Scripts
{
    internal class BatchScript : IScript
    {
        public IScriptConfiguration Configuration { get; private set; }

        public ScriptType ScriptType => ScriptType.WindowsProcess;

        public IOutput[] Outputs => throw new NotImplementedException();

        public BatchScript(IScriptConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
