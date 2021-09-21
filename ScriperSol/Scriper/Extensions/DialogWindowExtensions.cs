using Avalonia.Controls;
using Scriper.AssetsAccess;
using Scriper.Views;

namespace Scriper.Extensions
{
    public static class DialogWindowExtensions
    {
        private const int _scriptDialogHeight = 585;
        private const int _scriptDialogWidth = 600;

        public static DialogWindow CreateAddScriptDialogWindow(IControl control, IAssets assets)
        {
            return new DialogWindow(_scriptDialogWidth, _scriptDialogHeight, "Add Script", control, assets.GetAssetsImage<WindowIcon>("icons8_file_1.ico"));
        }

        public static DialogWindow CreateRunScriptDialogWindow(string name, IControl control, IAssets assets)
        {
            return new DialogWindow(500, 500, name, control, assets.GetAssetsImage<WindowIcon>("icons8_console.ico"));
        }

        public static DialogWindow CreateEditScriptDialogWindow(IControl control, IAssets assets)
        {
            return new DialogWindow(_scriptDialogWidth, _scriptDialogHeight, "Edit Script", control, assets.GetAssetsImage<WindowIcon>("icons8_edit_property.ico"));
        }
    }
}
