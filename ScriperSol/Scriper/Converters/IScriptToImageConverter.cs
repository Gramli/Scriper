using Avalonia.Media.Imaging;
using ScriperLib;

namespace Scriper.Converters
{
    public interface IScriptToImageConverter
    {
        IBitmap Convert(IScript script);

        string GetImagePath(IScript script);
    }
}
