using System.IO;

namespace Scriper.AssetsAccess
{
    public interface IAssets
    {
        T GetAssetsImage<T>(string fileName);
        MemoryStream GetAssetsImageMemoryStream(string fileName);
    }
}
