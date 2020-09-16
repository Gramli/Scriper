namespace ScriperLib.Configuration
{
    public interface IScriperConfiguration
    {
        IScriptManagerConfiguration ScriptManagerConfiguration { get; }

        void Save();
    }
}
