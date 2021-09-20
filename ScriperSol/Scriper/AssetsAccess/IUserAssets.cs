using System.Drawing;

namespace Scriper.AssetsAccess
{
    public interface IUserAssets : IAssetsStream
    {
        string AssetsImageDir { get; }
        string SaveImageInAssets(string imageName, Image image);
        string SaveImageInAssets(string imagePath);
        string SaveImageInAssetsAsIcon(string imageName, Image image);
    }
}
