using ScriperLib.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScriperLib.Scripts
{
    internal class BatchScript : IScript
    {
        public IScriptConfiguration Configuration => throw new NotImplementedException();

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
