using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Scriper.AssetsAccess
{
    internal class UserAssets : IUserAssets
    {
        public string AssetsImageDir => GetImageDir();

        private readonly string _imageDirName = "Images";
        private readonly string _baseDirName;

        public UserAssets(string baseDirName)
        {
            _baseDirName = baseDirName;
            CreateBaseDir();
        }
        public Stream GetAssetStream(string fileName)
        {
            if(!File.Exists(fileName))
            {
                throw new FileNotFoundException(fileName);
            }

            return new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        public string SaveImageInAssets(string imageName, Image image)
        {
            var fileName = Path.Combine(AssetsImageDir, imageName);
            image.Save(fileName, ImageFormat.Png);
            return fileName;
        }

        public string SaveImageInAssets(string imagePath)
        {
            var fileName = Path.Combine(AssetsImageDir, Path.GetFileName(imagePath));
            File.Copy(imagePath, fileName);
            return fileName;
        }

        public string SaveImageInAssetsAsIcon(string imageName, Image image)
        {
            var fileName = Path.Combine(AssetsImageDir, $"{Path.GetFileNameWithoutExtension(imageName)}.ico");
            image.Save(fileName, ImageFormat.Icon);
            return fileName;
        }

        private void CreateBaseDir()
        {
            var baseDir = Path.Combine(Path.GetTempPath(), _baseDirName);
            if (!Directory.Exists(baseDir))
            {
                Directory.CreateDirectory(baseDir);
            }
        }

        private string GetImageDir()
        {
            var imageDir = Path.Combine(Path.GetTempPath(), _baseDirName, _imageDirName);
            if (!Directory.Exists(imageDir))
            {
                Directory.CreateDirectory(imageDir);
            }

            return imageDir;
        }
    }
}
