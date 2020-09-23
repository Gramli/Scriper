using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Scriper.ViewModels;
using Scriper.Views;
using System.Collections.Generic;
using System.IO;

namespace Scriper
{
    public class App : Application
    {
        private readonly string _configNameEnd = "Scriper.config"; 
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var configPaths = FindConfig();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowVM(configPaths),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }

        private List<string> FindConfig()
        {
            var result = new List<string>();
            var dirName = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var fileNames = Directory.GetFiles(dirName);

            foreach(var fileName in fileNames)
            {
                if(fileName.EndsWith(_configNameEnd))
                {
                    result.Add(fileName);
                }
            }

            return result;
        }
    }
}
