using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Scriper.Models;
using Scriper.ViewModels;

namespace Scriper.Views
{
    public partial class ArgumentsVC : UserControl
    {
        public ArgumentsVC()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
