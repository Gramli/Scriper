using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Scriper.ViewModels;

namespace Scriper.Views
{
    public class ScriptManagerVC : UserControl
    {
        public ScriptManagerVC()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void TappedRow(object sender, RoutedEventArgs routedEventArgs)
        {
            var scriptManagerVM = (ScriptManagerVM)this.DataContext;
            var image = (Image)sender;
            scriptManagerVM.MoveScriptUp((string)image.Tag);
        }
    }
}
