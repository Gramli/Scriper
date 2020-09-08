using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Scriper.ViewModels;

namespace Scriper.Views
{
    public class ScriptVC : UserControl
    {
        public ScriptVC()
        {
            this.InitializeComponent();
        }
        public ScriptVC(ViewModelBase viewModelBase)
            : this()
        {
            this.DataContext = viewModelBase;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
