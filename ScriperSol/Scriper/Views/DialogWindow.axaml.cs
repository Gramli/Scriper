using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Scriper.Views
{
    public class DialogWindow : Window
    {
        private StackPanel _myPanel;

        public DialogWindow()
        {
            this.InitializeComponent();

            this.Width = 600;
            this.Height = 500;

#if DEBUG
            this.AttachDevTools();
#endif
        }
        public DialogWindow(IControl control)
            : this()
        {

            _myPanel.Children.Add(control);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            _myPanel = this.FindControl<StackPanel>("basePanel");
        }
    }
}
