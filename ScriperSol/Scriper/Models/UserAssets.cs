using System.IO;

namespace Scriper.Models
{
    class UserAssets : IUserAssets
    {
        public string AssetsImageDir => GetImageDir();

        private readonly string _imageDirName = "Images";
        private readonly string _baseDirName;

        public UserAssets(string baseDirName)
        {
            _baseDirName = baseDirName;
            CreateBaseDir();
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
