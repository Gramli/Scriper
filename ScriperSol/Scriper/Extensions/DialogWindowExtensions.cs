using Avalonia.Controls;
using Scriper.AssetsAccess;
using Scriper.Views;

namespace Scriper.Extensions
{
    public static class DialogWindowExtensions
    {
        private const int _scriptDialogHeight = 530;
        private const int _scriptDialogWidth = 600;

        public static DialogWindow CreateAddScriptDialogWindow(IControl control)
        {
            return new DialogWindow(_scriptDialogWidth, _scriptDialogHeight, "Add Script", control, AvaloniaAssets.Instance.GetAssetsIcon("icons8_file_1.ico"));
        }

        public static DialogWindow CreateRunScriptDialogWindow(string name, IControl control)
        {
            return new DialogWindow(500, 500, name, control, AvaloniaAssets.Instance.GetAssetsIcon("icons8_console.ico"));
        }

        public static DialogWindow CreateEditScriptDialogWindow(IControl control)
        {
            return new DialogWindow(_scriptDialogWidth, _scriptDialogHeight, "Edit Script", control, AvaloniaAssets.Instance.GetAssetsIcon("icons8_edit_property.ico"));
        }
    }
}
