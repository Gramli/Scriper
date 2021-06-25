using ScriperLib.Configuration;

namespace Scriper.TimeSchedule
{
    public interface IScriptSchedulerManagerAdapter
    {
        void Add(IScriptConfiguration scriptConfiguration);
        void Remove(string scriptName);
        void Replace(IScriptConfiguration scriptConfiguration);
    }
}
