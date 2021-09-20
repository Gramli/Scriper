using Avalonia.Controls;
using Avalonia.Media.Imaging;

namespace Scriper.AssetsAccess
{
    public class AvaloniaAssets : AssetsAccessBase
    {
        //TODO REMOVE ALL DEPENDENCIES
        public static AvaloniaAssets Instance => _instance ??= _instance = new AvaloniaAssets();
        private static AvaloniaAssets _instance;

        public WindowIcon GetAssetsIcon(string iconName)
        {
            var asset = GetAsset(iconName);
            return new WindowIcon(asset);
        }

        public IBitmap GetAssetsImage(string imageName)
        {
            var asset = GetAsset(imageName);
            return new Bitmap(asset);
        }
    }
}
