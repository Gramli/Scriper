using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using NLog;
using Scriper.Extensions;
using Scriper.ViewModels;
using Scriper.Views;
using System;
using System.Collections.Generic;
using System.IO;

namespace Scriper
{
    public class App : Application
    {
        private readonly string _configNameEnd = "Scriper.config";

        private static readonly Logger logger = NLogExtensions.LogFactory.GetCurrentClassLogger();

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

                desktop.Exit += Desktop_Exit;
            }

            base.OnFrameworkInitializationCompleted();

            logger.Info($"Application Start: {DateTime.Now}");
        }

        private void Desktop_Exit(object sender, ControlledApplicationLifetimeExitEventArgs e)
        {
            try
            {
                var mainWindow = (MainWindow)((IClassicDesktopStyleApplicationLifetime)sender).MainWindow;
                var mainWindowVM = (MainWindowVM)mainWindow.DataContext;
                mainWindowVM.SaveConfig();
            }
            catch(Exception ex)
            {
                MessageBoxExtensions.Show(ex.Message);
                logger.Error(ex);
            }

            App.Current.CloseWindows();
            NLogExtensions.LogFactory.Shutdown();
        }

        private List<string> FindConfig()
        {
            var result = new List<string>();
            var dirName = @$"{Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\Config";
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
