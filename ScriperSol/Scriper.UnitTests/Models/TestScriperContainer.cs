using Avalonia;
using Avalonia.ReactiveUI;

namespace Scriper.UnitTests.Models
{
    public class TestScriperContainer : ScriperContainer
    {
        public TestScriperContainer(string configurationFilePath, string uiConfigfurationFilePath) 
            : base(configurationFilePath, uiConfigfurationFilePath)
        {
            BuildAvaloniaApp();
        }

        private static void BuildAvaloniaApp() => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();

        public void Verify()
        {
            _container.Verify();
        }
    }
}
