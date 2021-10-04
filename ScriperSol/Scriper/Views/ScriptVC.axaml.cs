using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Scriper.ViewModels;
using Scriper.ViewModels.Script;

namespace Scriper.Views
{
    public class ScriptVC : UserControl
    {
        public ScriptVC()
        {
            this.InitializeComponent();
        }
        public ScriptVC(IAddEditScriptVM viewModelBase)
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
