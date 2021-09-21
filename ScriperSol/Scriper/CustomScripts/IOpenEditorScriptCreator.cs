using ScriperLib;

namespace Scriper.CustomScripts
{
    public interface IOpenEditorScriptCreator
    {
        bool IsTextEditorSet();
        IScript CreateOpenScriptEditorScript(string pathToScript);
    }
}
