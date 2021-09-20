using Avalonia.Data.Converters;
using Scriper.AssetsAccess;
using ScriperLib;
using System;
using System.Globalization;

namespace Scriper.Converters
{
    public class ScriptToImageConverter : IValueConverter
    {
        private readonly IScriptTypeToAssetNameConverter _scriptTypeToAssetNameConverter = new ScriptTypeToAssetNameConverter();
        private readonly PathToImageConverter _pathToImageConverter = new PathToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var script = (IScript)value;
            if(!string.IsNullOrEmpty(script.Configuration.IconImagePath))
            {
                return _pathToImageConverter.Convert(script.Configuration.IconImagePath, null, null, null);
            }

            var name = _scriptTypeToAssetNameConverter.Convert(script.ScriptType);
            return AvaloniaAssets.Instance.GetAssetsImage(name);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
