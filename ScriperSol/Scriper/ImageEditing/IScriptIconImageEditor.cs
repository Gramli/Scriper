namespace Scriper.ImageEditing
{
    public interface IScriptIconImageEditor
    {
        string ImageFileFilter { get; }
        string CreateImageInAssets(string fileName);
    }
}
