﻿using Scriper.AssetsAccess;
using System.Drawing;
using System.IO;

namespace Scriper.ImageEditing
{
    public class ScriptIconImageEditor : IScriptIconImageEditor
    {
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
            return _userAssets.SaveImageInAssets(fileName, resizedImage);
        }
    }
}
