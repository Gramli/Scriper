namespace Scriper.SystemStartUp
{
    public interface ISystemStartUp
    {
        bool IsStartUp { get; }
        void AddToStartUp();
        void RemoveFromStartUp();
    }
}
