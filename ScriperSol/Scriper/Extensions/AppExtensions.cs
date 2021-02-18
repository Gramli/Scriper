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

            throw new ApplicationException("Application is not App type");
        }

        public static void Hide(this Application application)
        {
            application.HandleDesktop((desktop) =>
            {
                desktop.MainWindow.Hide();
            });
        }

        public static void Show(this Application application)
        {
            application.HandleDesktop((desktop) =>
            {
                desktop.MainWindow.Show();
            });
        }

        public static void CloseWindows(this Application application)
        {
            application.HandleDesktop((desktop) =>
            {
                desktop.CloseWindows();
            });
        }

        public static void Close(this Application application)
        {
            application.HandleDesktop((desktop) =>
            {
                desktop.CloseWindows();
                desktop.MainWindow.Close();
            });
        }

        private static void CloseWindows(this IClassicDesktopStyleApplicationLifetime desktop)
        {
            foreach (var window in desktop.Windows)
            {
                window.Close();
            }
        }

        private static void HandleDesktop(this Application application, Action<IClassicDesktopStyleApplicationLifetime> action)
        {
            if (application is App app)
            {
                if (app.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                {
                    action.Invoke(desktop);
                    return;
                }

                throw new ApplicationException("Application is not IClassicDesktopStyleApplicationLifetime");
            }

            throw new ApplicationException("Application is not type");
        }
    }
}
