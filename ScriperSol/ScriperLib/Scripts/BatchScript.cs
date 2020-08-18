using ScriperLib.Configuration;
using System;

namespace ScriperLib.Scripts
{
    internal class BatchScript : IScript
    {
        public IScriptConfiguration Configuration { get; private set; }

        public BatchScript(IScriptConfiguration configuration)
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
