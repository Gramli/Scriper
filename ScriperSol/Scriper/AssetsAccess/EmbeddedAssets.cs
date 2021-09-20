using Avalonia;
using Avalonia.Platform;
using System;
using System.IO;

namespace Scriper.AssetsAccess
{
    internal class EmbeddedAssets : IEmbeddedAssets
    {
        private const string assetsPath = "avares://Scriper/Assets/";
        public Stream GetAssetStream(string assetName)
        {
            var uri = GetAssetUri(assetName);
            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
            return assets.Open(uri);
        }
        public Uri GetAssetUri(string assetName)
        {
            var assetPath = GetAbsolutePath(assetName);
            return new Uri(assetPath, UriKind.Absolute);
        }

        private string GetAbsolutePath(string assetName)
        {
            return $"{assetsPath}{assetName}";
        }
    }
}
