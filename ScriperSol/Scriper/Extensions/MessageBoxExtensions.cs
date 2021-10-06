
using Scriper.ViewModels.MessageBox;
using Scriper.Views;

namespace Scriper.Extensions
{
    public static class MessageBoxExtensions
    {
        public static void ShowDialog(string message)
        {
            var messageBox = CreateDefaultErrorMessageBox(message);
            messageBox.ShowDialog(App.Current.GetMainWindow());

        }

        public static void Show(string message)
        {
            var messageBoxCustomWindow = CreateDefaultErrorMessageBox(message);
            messageBoxCustomWindow.Show();
        }

        private static MessageBox CreateDefaultErrorMessageBox(string message)
        {
            var messageBox = new MessageBox()
            {
                Width = 540,
                Height = 200,
            };
            var dataContext = new MessageBoxVM("Error", message);
            dataContext.Close += (sender, args) =>
            {
                messageBox.Close();
            };
            messageBox.DataContext = dataContext;
            return messageBox;
        }
    }
}
