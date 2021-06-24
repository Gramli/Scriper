using ScriperLib.Configuration;
using ScriperLib.ScriptScheduler;

namespace Scriper.Models
{
    internal interface IScriptSchedulerManagerAdapter
    {
        void Add(IScriptConfiguration scriptConfiguration);
        void Remove(string scriptName);
    }
}
