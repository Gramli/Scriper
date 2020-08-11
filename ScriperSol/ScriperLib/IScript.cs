using ScriperLib.Configuration;

namespace ScriperLib
{
    public interface IScript
    {
        IScriptConfiguration Configuration { get; }
        public void Run();
        IScriptConfiguration Save();
    }
}
