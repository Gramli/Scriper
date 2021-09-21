using Avalonia.Media.Imaging;
using Scriper.AssetsAccess;
using ScriperLib;

namespace Scriper.Converters
{
    public class ScriptToImageConverter : IScriptToImageConverter
    {
        private readonly IScriptTypeToAssetNameConverter _scriptTypeToAssetNameConverter;
        private readonly IAssets _assets;

        public ScriptToImageConverter(IScriptTypeToAssetNameConverter scriptTypeToAssetNameConverter, IAssets assets)
        {
            _scriptTypeToAssetNameConverter = scriptTypeToAssetNameConverter;
            _assets = assets;
        }

        public IBitmap Convert(IScript script)
        {
            var path = GetImagePath(script);
            return _assets.GetAssetsImage<Bitmap>(path);
        }

        public string GetImagePath(IScript script)
        {
            if (!string.IsNullOrEmpty(script.Configuration.IconImagePath))
            {
                return script.Configuration.IconImagePath;
            }

            return _scriptTypeToAssetNameConverter.Convert(script.ScriptType);
        }
    }
}
