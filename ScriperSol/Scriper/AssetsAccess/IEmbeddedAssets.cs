using System;

namespace Scriper.AssetsAccess
{
    public interface IEmbeddedAssets : IAssetsStream
    {
        Uri GetAssetUri(string assetName);
    }
}
