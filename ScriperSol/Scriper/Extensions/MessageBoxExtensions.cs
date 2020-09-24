using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using MessageBox.Avalonia.Models;

namespace Scriper.Extensions
{
    public static class MessageBoxExtensions
    {
        public static void Show(string message)
        {
            var messageBoxCustomWindow = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxCustomWindow(new MessageBoxCustomParams
            {
                Icon = Icon.Error,
                Style = Style.Windows,
                ContentMessage = message,
                ContentHeader = "Error Message:",
                ContentTitle = "Error",
                ButtonDefinitions = new[] { new ButtonDefinition { Name = "Ok", Type = ButtonType.Default }, }
            });

            messageBoxCustomWindow.ShowDialog(App.Current.GetMainWindow());
        }
    }
}
