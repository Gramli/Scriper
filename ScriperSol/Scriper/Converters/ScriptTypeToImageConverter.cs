using Avalonia.Data.Converters;
using Scriper.Extensions;
using ScriperLib.Enums;
using System;
using System.Globalization;
using Scriper.AssetsAccess;

namespace Scriper.Converters
{
    public class ScriptTypeToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch((ScriptType)value)
            {
                case ScriptType.PowerShell1:
                case ScriptType.PowerShell2:
                    return AvaloniaAssets.Instance.GetAssetsImage("icons8_powershell_96px.png");
                case ScriptType.PythonFile:
                    return AvaloniaAssets.Instance.GetAssetsImage("icons8_python_96px.png");
                case ScriptType.WindowsProcess:
                    return AvaloniaAssets.Instance.GetAssetsImage("icons8_windows_xp_96px_1.png");
                case ScriptType.ExeFile:
                    return AvaloniaAssets.Instance.GetAssetsImage("icons8_application_window_96px.png");
                case ScriptType.Javascript:
                    return AvaloniaAssets.Instance.GetAssetsImage("icons8_javascript_96px.png");
                case ScriptType.LinuxShell:
                    return AvaloniaAssets.Instance.GetAssetsImage("icons8_linux_96px.png");
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
