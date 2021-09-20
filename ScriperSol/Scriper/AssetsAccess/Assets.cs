using System;
using System.IO;

namespace Scriper.AssetsAccess
{
    internal class Assets : IAssets
    {
        private readonly IEmbeddedAssets _embeddedAssets;
        private readonly IUserAssets _userAssets;
        public Assets(IEmbeddedAssets embeddedAssets, IUserAssets userAssets)
        {
            _embeddedAssets = embeddedAssets;
            _userAssets = userAssets;
        }
        public T GetAssetsIcon<T>(string fileName)
        {
            using var icon = GetAsset(fileName);
            return CreateInstance<T>(icon);
        }

        public T GetAssetsImage<T>(string fileName)
        {
            using var image = GetAsset(fileName);
            return CreateInstance<T>(image);
        }

        private T CreateInstance<T>(Stream stream)
        {
            return (T)Activator.CreateInstance(typeof(T), stream);
        }

        public Stream GetAsset(string fileName)
        {
            if(fileName.Contains(_userAssets.AssetsImageDir))
            {
                return _userAssets.GetAssetStream(fileName);
            }

            return _embeddedAssets.GetAssetStream(fileName);
        }
    }
}
