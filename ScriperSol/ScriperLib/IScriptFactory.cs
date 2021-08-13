using ScriperLib.Configuration;

namespace ScriperLib
{
    public interface IScriptFactory
    {
        IScript Create(IScriptConfiguration scriptConfiguration);
    }
}
