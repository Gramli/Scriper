using NLog;
using Scriper.Configuration;
using ScriperLib;
using ScriperLib.Configuration;

namespace Scriper.Models
{
    internal class OpenEditorScriptCreator : IOpenEditorScriptCreator
    {
        private readonly IScriperUIConfiguration _uiConfig;
        private readonly IScriperLibContainer _container;
        private readonly string _openScriptEditorScript = "OpenScriptEditorScript";

        public OpenEditorScriptCreator(IScriperLibContainer container, IScriperUIConfiguration uiConfig)
        {
            _container = container;
            _uiConfig = uiConfig;
        }

        public bool IsTextEditorSet()
        {
            return string.IsNullOrEmpty(_uiConfig.TextEditor.Path);
        }

        public IScript CreateOpenScriptEditorScript(string pathToScript)
        {
            var newScriptConfig = _container.GetInstance<IScriptConfiguration>();
            newScriptConfig.Name = _openScriptEditorScript;
            newScriptConfig.Arguments = pathToScript;
            newScriptConfig.Path = _uiConfig.TextEditor.Path;
            return _container.GetInstance<IScriptCreator>().Create(newScriptConfig);
        }
    }
}
