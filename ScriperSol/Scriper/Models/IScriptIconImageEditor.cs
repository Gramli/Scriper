namespace Scriper.Models
{
    public interface IScriptIconImageEditor
    {
        string ImageFileFilter { get; }
        string CreateImageInAssets(string fileName);
    }
}
