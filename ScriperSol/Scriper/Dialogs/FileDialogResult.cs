namespace Scriper.Dialogs
{
    public class FileDialogResult
    {
        public bool Ok { get; private set; }

        public string[] Files { get; private set; }

        public FileDialogResult(bool ok, string[] files)
        {
            Ok = ok;
            Files = files;
        }
    }
}
