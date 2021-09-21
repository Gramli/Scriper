namespace Scriper.AssetsAccess
{
    public interface IAssets
    {
        T GetAssetsImage<T>(string fileName);
    }
}
