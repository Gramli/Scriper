using System;

namespace ScriperLib.Enums
{
    public class FileExtensionAttribute : Attribute
    {
        public string[] FileExtensionts { get; private set; }
        public FileExtensionAttribute(params string[] fileExtensions)
        {
            FileExtensionts = fileExtensions;
        }
    }
}
