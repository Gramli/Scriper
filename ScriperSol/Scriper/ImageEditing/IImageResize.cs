using System.Drawing;

namespace Scriper.ImageEditing
{
    public interface IImageResize
    {
        Image ResizeImageTo96px(Image image);
    }
}
