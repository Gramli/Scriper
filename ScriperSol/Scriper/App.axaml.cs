using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using NLog;
using Scriper.Extensions;
using Scriper.SystemTray.Windows;
using Scriper.ViewModels;
using Scriper.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Application = Avalonia.Application;

namespace Scriper
{
    public class App : Application
    {
        private readonly string _configNameEnd = "Scriper.config";
        private readonly string _configUINameEnd = "ScriperUI.config";

        private static readonly Logger logger = NLogExtensions.LogFactory.GetCurrentClassLogger();

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var uiConfigPath = FindConfig(_configUINameEnd).SingleOrDefault();
            var configPaths = FindConfig(_configNameEnd);

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowVM(configPaths, uiConfigPath),
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
                mainWindowVM.SaveConfigs();
            }
            catch(Exception ex)
            {
                MessageBoxExtensions.Show(ex.Message);
                logger.Error(ex);
            }

            App.Current.CloseWindows();
            NLogExtensions.LogFactory.Shutdown();
        }

        private List<string> FindConfig(string configNameEnd)
        {
            var result = new List<string>();
            var dirName = @$"{Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\Config";
            var fileNames = Directory.GetFiles(dirName);

            foreach(var fileName in fileNames)
            {
                if(fileName.EndsWith(configNameEnd))
                {
                    result.Add(fileName);
                }
            }

            return result;
        }
    }
}
