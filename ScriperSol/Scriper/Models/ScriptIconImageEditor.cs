using System.Drawing;
using System.IO;

namespace Scriper.Models
{
    public class ScriptIconImageEditor : IScriptIconImageEditor
    {
        private readonly IImageCopy _imageCopy;
        private readonly IImageResize _imageResize;
        public ScriptIconImageEditor(IImageCopy imageCopy, IImageResize imageResize)
        {
            _imageCopy = imageCopy;
            _imageResize = imageResize;
        }
        public string CreateImageInAssets(string fileName)
        {
            using var image = Image.FromFile(fileName);
            using var resizedImage = _imageResize.ResizeImageTo96px(image);
            return _imageCopy.SaveImageInAssets(Path.GetFileName(fileName), resizedImage);
        }
    }
}
