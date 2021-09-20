using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Scriper.Models
{
    public class ImageCopy : IImageCopy
    {
        private readonly IUserAssets _userAssets;
        public ImageCopy(IUserAssets userAssets)
        {
            _userAssets = userAssets;
        }
        public string SaveImageInAssets(string imageName, Image image)
        {
            var fileName = Path.Combine(_userAssets.AssetsImageDir, imageName);
            image.Save(fileName, ImageFormat.Png);
            return fileName;
        }

        public string SaveImageInAssets(string imagePath)
        {
            var fileName = Path.Combine(_userAssets.AssetsImageDir, Path.GetFileName(imagePath));
            File.Copy(imagePath, fileName);
            return fileName;
        }
    }
}
