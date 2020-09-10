using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using System;

namespace Scriper.Extensions
{
    public static class AppExtensions
    {
        public static Window GetMainWindow(this Application application)
        {
            if (application is App app)
            {
                if (app.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                {
                    return desktop.MainWindow;
                }

                throw new ApplicationException("Application is not IClassicDesktopStyleApplicationLifetime");
            }

            throw new ApplicationException("Application is not App");
        }

        public static void Close(this Application application)
        {
            if (application is App app)
            {
                if (app.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                {
                    desktop.MainWindow.Close();
                    return;
                }

                throw new ApplicationException("Application is not IClassicDesktopStyleApplicationLifetime");
            }

            throw new ApplicationException("Application is not App");
        }
    }
}
