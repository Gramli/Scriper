using System.Threading.Tasks;

namespace Scriper.Dialogs
{
    public interface IScriperFileDialogOpener
    {
        Task<(bool ok, string file)> OpenScriptFileDialogAsync();
        Task<(bool ok, string file)> OpenImageFileDialogAsync();
        Task<(bool ok, string file)> OpenOutputFileDialogAsync();
    }
}
