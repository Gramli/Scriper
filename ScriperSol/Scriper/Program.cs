using System;
using System.IO;
using System.Linq;
using Avalonia;
using Avalonia.ReactiveUI;
using ScriperLib;
using ScriperLib.ScriptScheduler;

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
                var configPath = args[0];
                var container = new ScriperLibContainer(configPath);
                var runner = container.GetInstance<IScriptTaskSchedulerRunner>();
                var scriptName = string.Join(" ", args[1..]);
                runner.Run(scriptName);

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
