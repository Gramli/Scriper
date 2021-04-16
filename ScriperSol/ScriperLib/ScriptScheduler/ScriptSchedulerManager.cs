using ScriperLib.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScriperLib.ScriptScheduler
{
    public class ScriptSchedulerManager : IScriptSchedulerManager
    {
        public ScriptSchedulerManager()
        {

        }

        public void Add(IScriptConfiguration scriptConfiguration)
        {
            throw new NotImplementedException();
        }

        public ITimeScheduleConfiguration Get(string scriptName)
        {
            throw new NotImplementedException();
        }

        public void Remove(string scriptName)
        {
            throw new NotImplementedException();
        }
    }
}
