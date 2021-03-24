using Avalonia.Platform;
using NLog;
using Scriper.OperationSystem;
using Scriper.SystemTray.Windows;

namespace Scriper.SystemTray
{
    public class OperatingSystemTrayMenuFactory : IOperatingSystemTrayMenuFactory
    {
        private static readonly Logger _logger = NLogFactoryProxy.Instance.GetLogger();

        public IOperationSystemTrayMenu CreateOperationSystemTrayMenu()
        {
            IOperationSystemTrayMenu osSpecificSystemTrayMenu = null;

            var operationSystemType = OperationSystemInformation.GetOperatingSystemType;

            switch (operationSystemType)
            {
                case OperatingSystemType.WinNT:
                    osSpecificSystemTrayMenu = new WindowsSystemTrayMenu();
                    break;
                default:
                    _logger.Log(LogLevel.Info, $"TrayMenu is not supported for actual OS type:{operationSystemType}");
                    break;
            }

            if (osSpecificSystemTrayMenu != null)
            {
                var appName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                osSpecificSystemTrayMenu.Init(appName);
            }

            return osSpecificSystemTrayMenu;
        }
    }
}
