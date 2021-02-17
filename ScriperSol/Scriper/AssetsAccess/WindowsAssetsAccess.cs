using System.Drawing;

namespace Scriper.AssetsAccess
{
    public class WindowsAssetsAccess : AssetsAccessBase
    {
        public static WindowsAssetsAccess Instance => _instance ??= _instance = new WindowsAssetsAccess();
        private static WindowsAssetsAccess _instance;

        public Icon GetAssetsIcon(string name)
        {
            using var iconStream = GetAsset(name);
            return new Icon(iconStream);
        }

        public Image GetAssetsImage(string imageName)
        {
            var asset = GetAsset(imageName);
            return new Bitmap(asset);
        }
    }
}
