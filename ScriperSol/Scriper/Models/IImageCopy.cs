using System.Drawing;

namespace Scriper.Models
{
    public interface IImageCopy
    {
        string SaveImageInAssets(string imageName, Image image);
    }
}
