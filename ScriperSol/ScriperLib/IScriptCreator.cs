using ScriperLib.Configuration;

namespace ScriperLib
{
    public interface IScriptCreator
    {
        IScript Create(IScriptConfiguration scriptConfiguration);
    }
}
