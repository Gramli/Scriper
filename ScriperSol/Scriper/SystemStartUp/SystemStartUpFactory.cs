using Avalonia.Platform;
using NLog;
using Scriper.OperationSystem;
using Scriper.SystemStartUp.Windows;

namespace Scriper.SystemStartUp
{
    internal class SystemStartUpFactory : ISystemStartUpFactory
    {
        private static readonly Logger _logger = NLogFactoryProxy.Instance.GetLogger();

        public ISystemStartUp CreateSystemStartUp()
        {
            ISystemStartUp osSpecificSystemStartUp = null;

            var operationSystemType = OperationSystemInformation.GetOperatingSystemType;

            switch (operationSystemType)
            {
                case OperatingSystemType.WinNT:
                    osSpecificSystemStartUp = new WindowsSystemStartUp();
                    break;
                default:
                    _logger.Log(LogLevel.Info, $"TrayMenu is not supported for actual OS type:{operationSystemType}");
                    break;
            }

            return osSpecificSystemStartUp;
        }
    }
}
