using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using NLog;
using Scriper.Extensions;
using Scriper.ViewModels;
using Scriper.Views;
using System;
using Application = Avalonia.Application;

namespace Scriper
{
    public class App : Application
    {
        private static readonly Logger _logger = NLogFactoryProxy.Instance.GetLogger();

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowVM(),
                };

                desktop.Exit += Desktop_Exit;
            }

            base.OnFrameworkInitializationCompleted();

            _logger.Info($"Application Start: {DateTime.Now}");
        }

        private void Desktop_Exit(object sender, ControlledApplicationLifetimeExitEventArgs e)
        {
            try
            {
                var mainWindow = (MainWindow)((IClassicDesktopStyleApplicationLifetime)sender).MainWindow;
                var mainWindowVM = (MainWindowVM)mainWindow.DataContext;
                mainWindowVM.SaveConfigs();
                mainWindowVM.Dispose();
            }
            catch(Exception ex)
            {
                MessageBoxExtensions.Show(ex.Message);
                _logger.Error(ex);
            }

            App.Current.CloseWindows();
            NLogFactoryProxy.Instance.Shutdown();
        }
    }
}
