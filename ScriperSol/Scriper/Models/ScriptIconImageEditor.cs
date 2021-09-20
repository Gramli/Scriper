using Scriper.AssetsAccess;
using System.Drawing;
using System.IO;

namespace Scriper.Models
{
    public class ScriptIconImageEditor : IScriptIconImageEditor
    {
        public string ImageFileFilter => "png | jpg | jpeg | bmp | ico";

        private readonly IUserAssets _userAssets;
        private readonly IImageResize _imageResize;
        public ScriptIconImageEditor(IUserAssets userAssets, IImageResize imageResize)
        {
            _userAssets = userAssets;
            _imageResize = imageResize;
        }

        public string CreateImageInAssets(string filePath)
        {
            if(Path.GetExtension(filePath) == "ico")
            {
                return _userAssets.SaveImageInAssets(filePath);
            }

            using var image = Image.FromFile(filePath);
            using var resizedImage = _imageResize.ResizeImageTo96px(image);
            var fileName = Path.GetFileName(filePath);
            _userAssets.SaveImageInAssetsAsIcon(fileName, resizedImage);
            return _userAssets.SaveImageInAssets(fileName, resizedImage);
        }
    }
}
