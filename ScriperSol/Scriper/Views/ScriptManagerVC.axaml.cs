using Avalonia.Controls;
using Avalonia.Input;
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

        public void DoubleTappedRow(object sender, RoutedEventArgs routedEventArgs)
        {
            var context = (ScriptManagerVM)this.DataContext;
            var image = (Image)sender;
            context.ChangeScriptOrder((string)image.Tag, true);

        }

        public void TappedRow(object sender, RoutedEventArgs routedEventArgs)
        {
            var context = (ScriptManagerVM)this.DataContext;
            var image = (Image)sender;
            context.ChangeScriptOrder((string)image.Tag, true);
        }
    }
}
