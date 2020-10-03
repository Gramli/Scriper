namespace Scriper.ViewModels
{
    public class AboutVM : ViewModelBase
    {
        public string Author => "Author: Gramli";
        public string Icons => "Icons: made by Icons8";
        public string UI => "UI: framework AvaloniaUI";
        public string Msg => $"MessageBox: MessageBox.Avalonia";
        public string Version => $"Version: {GetType().Assembly.GetName().Version}";
    }
}
