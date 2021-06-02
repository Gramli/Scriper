namespace Scriper.SystemStartUp
{
    public class SystemStartUpAdapter : ISystemStartUp
    {
        public bool IsStartUp => _systemStartUp.IsStartUp;

        private readonly ISystemStartUp _systemStartUp;

        public SystemStartUpAdapter(ISystemStartUpFactory systemStartUpFactory)
        {
            _systemStartUp = systemStartUpFactory.CreateSystemStartUp();
        }

        public void AddToStartUp()
        {
            _systemStartUp.AddToStartUp();
        }

        public void RemoveFromStartUp()
        {
            _systemStartUp.RemoveFromStartUp();
        }
    }
}
