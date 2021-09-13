using System;
using System.Collections.Generic;
using System.IO;

namespace Scriper.ViewModels.Validation.Validators
{
    public class ImageExtensionValidator : IValidator<string>
    {
        private readonly ICollection<string> _allowedImages;
        public Func<string> GetValueToValidate { get; }
        public Action<string> InvalidCallback { get; }
        public ImageExtensionValidator(Func<string> getValueToValidate, Action<string> invalidCallback)
        {
            GetValueToValidate = getValueToValidate;
            InvalidCallback = invalidCallback;
            _allowedImages = new List<string>(){ "png", "bmp", "gif", "jpeg" };
        }

        public bool Validate()
        {
            var imagePath = GetValueToValidate();

            if(string.IsNullOrEmpty(imagePath))
            {
                return true;
            }

            if (_allowedImages.Contains(Path.GetExtension(imagePath)))
            {
                return true;
            }

            InvalidCallback("Uknown image(file) type.");
            return false;
        }
    }
}
