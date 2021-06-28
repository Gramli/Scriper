using ScriperLib;

namespace Scriper.Models
{
    public interface IOpenEditorScriptCreator
    {
        bool IsTextEditorSet();
        IScript CreateOpenScriptEditorScript(string pathToScript);
    }
}
