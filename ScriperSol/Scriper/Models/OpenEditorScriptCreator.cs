using Scriper.Configuration;
using ScriperLib;
using ScriperLib.Configuration;

namespace Scriper.Models
{
    internal class OpenEditorScriptCreator : IOpenEditorScriptCreator
    {
        private readonly IScriperUIConfiguration _uiConfig;
        private readonly IScriptFactory _scriptCreator;
        private readonly IScriptConfigurationFactory _scriptConfigurationCreator;
        private readonly string _openScriptEditorScript = "OpenScriptEditorScript";

        public OpenEditorScriptCreator(IScriptConfigurationFactory scriptConfigurationCreator, IScriptFactory scriptCreator, IScriperUIConfiguration uiConfig)
        {
            _scriptCreator = scriptCreator;
            _scriptConfigurationCreator = scriptConfigurationCreator;
            _uiConfig = uiConfig;
        }

        public bool IsTextEditorSet()
        {
            return string.IsNullOrEmpty(_uiConfig.TextEditor.Path);
        }

        public IScript CreateOpenScriptEditorScript(string pathToScript)
        {
            var newScriptConfig = _scriptConfigurationCreator.CreateEmptyScriptConfiguration();
            newScriptConfig.Name = _openScriptEditorScript;
            newScriptConfig.Arguments = pathToScript;
            newScriptConfig.Path = _uiConfig.TextEditor.Path;
            return _scriptCreator.Create(newScriptConfig);
        }
    }
}
