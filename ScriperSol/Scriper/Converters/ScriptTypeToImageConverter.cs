using Avalonia.Data.Converters;
using Scriper.AssetsAccess;
using ScriperLib.Enums;
using System;
using System.Globalization;

namespace Scriper.Converters
{
    public class ScriptTypeToImageConverter : IValueConverter
    {
        private readonly ScriptTypeToAssetNameConverter scriptTypeToAssetNameConverter = new ScriptTypeToAssetNameConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var name = scriptTypeToAssetNameConverter.Convert((ScriptType)value);
            return AvaloniaAssets.Instance.GetAssetsImage(name);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
