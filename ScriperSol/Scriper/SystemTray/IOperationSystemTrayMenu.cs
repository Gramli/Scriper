using Avalonia.Platform;

namespace Scriper.SystemTray
{
    interface IOperationSystemTrayMenu : ISystemTrayMenu
    {
        OperatingSystemType OperatingSystemType { get; }
        void Init(string name);
    }
}
