using ScriperLib.Configuration.Attributes;
using System.Collections.Generic;
using System.Xml.Linq;

namespace ScriperLib.Configuration
{
    internal class ScriptManagerConfiguration : ConfigurationElement, IScriptManagerConfiguration
    {
        [XmlCollection("ScriptManager", "Script")]
        public IReadOnlyCollection<IScriptConfiguration> ScriptsConfigurations { get; private set; }

        public ScriptManagerConfiguration(XElement element) 
            : base(element)
        {
        }
    }
}
