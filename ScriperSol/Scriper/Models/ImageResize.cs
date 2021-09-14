using System.Drawing;
using System.Drawing.Imaging;

namespace Scriper.Models
{
    class ImageResize : IImageResize
    {
        public Image ResizeImageTo96px(Image image)
        {
            return ResizeImage(image, 96, 96);
        }

        private Image ResizeImage(Image image, int width, int height)
        {
            var destRectangle = new Rectangle(0, 0, width, height);
            var destinationImage = new Bitmap(width, height);

            destinationImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using var graphics = Graphics.FromImage(destinationImage);
            graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            using var wrapMode = new ImageAttributes();
            wrapMode.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);
            graphics.DrawImage(image, destRectangle, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);

            return destinationImage;
        }
    }
}
