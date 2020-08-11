namespace ScriperLib.Configuration
{
    public interface IScriptConfiguration : IConfigurationElement
    {
        string Name { get; }
        string Description { get; }
        string Path { get; }
        bool InSystemTray { get; }
        ITimeScheduleConfiguration TimeScheduleConfiguration { get; }

    }
}
