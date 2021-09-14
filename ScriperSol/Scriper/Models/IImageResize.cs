using System.Drawing;

namespace Scriper.Models
{
    public interface IImageResize
    {
        Image ResizeImageTo96px(Image image);
    }
}
