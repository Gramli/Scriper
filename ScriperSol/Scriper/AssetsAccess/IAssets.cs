namespace Scriper.AssetsAccess
{
    public interface IAssets
    {
        T GetAssetsIcon<T>(string fileName);
        T GetAssetsImage<T>(string fileName);
    }
}
