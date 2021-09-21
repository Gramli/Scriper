using Avalonia.Media.Imaging;
using ScriperLib;
using ScriperLib.Configuration;

namespace Scriper.ViewModels
{
    public interface IScriptVM
    {
        IScriptConfiguration ScriptConfiguration { get; }
        IScript Script { get; }

        public IBitmap ScriptImage { get; }

        string LastRun { get; set; }
    }
}
