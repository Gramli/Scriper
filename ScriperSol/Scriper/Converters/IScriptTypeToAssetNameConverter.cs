using ScriperLib.Enums;

namespace Scriper.Converters
{
    public interface IScriptTypeToAssetNameConverter
    {
        string Convert(ScriptType key);
    }
}
