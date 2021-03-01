using Avalonia;
using Avalonia.Platform;

namespace Scriper.OperationSystem
{
    public static class OperationSystemInformation
    {
        public static OperatingSystemType GetOperatingSystemType { get => AvaloniaLocator.Current.GetService<IRuntimePlatform>().GetRuntimeInfo().OperatingSystem; }
    }
}
