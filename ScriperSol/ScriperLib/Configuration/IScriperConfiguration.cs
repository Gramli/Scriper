namespace ScriperLib.Configuration
{
    public interface IScriperConfiguration
    {
        public string ConfigPath { get; }
        IScriptManagerConfiguration ScriptManagerConfiguration { get; }

        void Save(string path);
    }
}
