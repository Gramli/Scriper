using ScriperLib.Configuration;
using System;

namespace ScriperLib.Scripts
{
    internal class PowerShellScript : IScript
    {
        public IScriptConfiguration Configuration { get; private set; }

        public PowerShellScript(IScriptConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Run()
        {
            throw new NotImplementedException();
        }
        public void Load(IScriptConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        public IScriptConfiguration Save()
        {
            throw new NotImplementedException();
        }
    }
}
