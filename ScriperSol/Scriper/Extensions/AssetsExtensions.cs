using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;

namespace Scriper.Extensions
{
    public static class AssetsExtensions
    {
        public static WindowIcon GetAssetsIcon(string iconName)
        {
            var assetPath = GetAbsolutePath(iconName);
            var uri = new Uri(assetPath, UriKind.Absolute);
            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
            var asset = assets.Open(uri);
            return new WindowIcon(asset);
        }

        public static IBitmap GetAssetsImage(string iconName)
        {
            var assetPath = GetAbsolutePath(iconName);
            var uri = new Uri(assetPath, UriKind.Absolute);
            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
            var asset = assets.Open(uri);
            return new Bitmap(asset);
        }

        public static string GetAbsolutePath(string assetName)
        {
            return $"avares://Scriper/Assets/{assetName}";
        }

    }
}
