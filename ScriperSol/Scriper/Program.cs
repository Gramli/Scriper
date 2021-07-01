using Avalonia;
using Avalonia.ReactiveUI;

namespace Scriper
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        public static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                var command = args[0];
                switch (command)
                {
                    case "-run":
                        TaskRunnerMode.TaskRunnerMain(args[1..]);
                        break;
                    case "-un":
                        TaskUninstallMode.TaskRunnerMain();
                        break;
                }
                return;
            }

            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToDebug()
                .UseReactiveUI();
    }
}
