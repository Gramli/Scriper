using Avalonia.Platform;

namespace Scriper.SystemTray
{
    public interface IOperationSystemTrayMenu : ISystemTrayMenu
    {
        OperatingSystemType OperatingSystemType { get; }
        void Init(string name);
    }
}
