namespace ScriperLib.Configuration
{
    public class ScriptConfigurationFactory : IScriptConfigurationFactory
    {
        public IScriptConfiguration CreateEmptyScriptConfiguration()
        {
            return new ScriptConfiguration();
        }
    }
}
