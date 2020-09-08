using Scriper.Closing;
using ScriperLib;
using ScriperLib.Configuration;

namespace Scriper.ViewModels
{
    public class ScriptVM : ViewModelBase, IClose<IScriptConfiguration>
    {
        public IScriptConfiguration ScriptConfiguration { get; private set; }

        public event CloseEventHandler<IScriptConfiguration> Close;

        public ScriptVM(IScriptConfiguration scriptConfiguration)
        {
            ScriptConfiguration = scriptConfiguration;
        }
    }
}
