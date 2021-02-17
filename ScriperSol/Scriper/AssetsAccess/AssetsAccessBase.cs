using Avalonia;
using Avalonia.Platform;
using System;
using System.IO;

namespace Scriper.AssetsAccess
{
    public abstract class AssetsAccessBase
    {
        public Stream GetAsset(string assetName)
        {
            var assetPath = GetAbsolutePath(assetName);
            var uri = new Uri(assetPath, UriKind.Absolute);
            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
            return assets.Open(uri);
        }

        public string GetAbsolutePath(string assetName)
        {
            return $"avares://Scriper/Assets/{assetName}";
        }
    }
}
