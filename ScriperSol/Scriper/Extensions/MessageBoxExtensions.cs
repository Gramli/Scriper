using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using MessageBox.Avalonia.Models;

namespace Scriper.Extensions
{
    public static class MessageBoxExtensions
    {
        private static MessageBox.Avalonia.BaseWindows.Base.IMsBoxWindow<string> CreateDefaultErrorMessageBox(string message)
        {
            return MessageBox.Avalonia.MessageBoxManager.GetMessageBoxCustomWindow(new MessageBoxCustomParams
            {
                Icon = Icon.Error,
                Style = Style.Windows,
                ContentMessage = message,
                ContentHeader = "Error Message:",
                ContentTitle = "Error",
                ButtonDefinitions = new[] { new ButtonDefinition { Name = "Ok", Type = ButtonType.Default }, }
            }); ;
        }

        public static void ShowDialog(string message)
        {
            var messageBoxCustomWindow = CreateDefaultErrorMessageBox(message);
            messageBoxCustomWindow.ShowDialog(App.Current.GetMainWindow());
        }

        public static void Show(string message)
        {
            var messageBoxCustomWindow = CreateDefaultErrorMessageBox(message);
            messageBoxCustomWindow.Show();
        }
    }
}
