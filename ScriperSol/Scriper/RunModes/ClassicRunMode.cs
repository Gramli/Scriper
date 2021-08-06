using Avalonia;
using Avalonia.ReactiveUI;

namespace Scriper.RunModes
{
    class ClassicRunMode : IRunMode
    {
        private readonly string[] _args;
        public ClassicRunMode(string[] args)
        {
            _args = args;
        }

        public void Run() =>
            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(_args);

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();
    }
}
