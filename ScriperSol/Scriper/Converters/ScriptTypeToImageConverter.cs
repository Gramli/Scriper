using Avalonia.Data.Converters;
using Scriper.Extensions;
using ScriperLib.Enums;
using System;
using System.Globalization;

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
                    return AssetsExtensions.GetAssetsImage("icons8_powershell_96px.png");
                case ScriptType.PythonFile:
                    return AssetsExtensions.GetAssetsImage("icons8_python_96px.png");
                case ScriptType.WindowsProcess:
                    return AssetsExtensions.GetAssetsImage("icons8_windows_xp_96px_1.png");
                case ScriptType.ExeFile:
                    return AssetsExtensions.GetAssetsImage("icons8_program_96px_1.png");
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
