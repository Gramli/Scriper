using System.IO;

namespace Scriper.AssetsAccess
{
    public interface IAssetsStream
    {
        Stream GetAssetStream(string fileName);
    }
}
