﻿using Avalonia;
using Avalonia.ReactiveUI;

namespace Scriper.UnitTests.Models
{
    public class AvaloniaScriperContainer : TestScriperContainer
    {
        public AvaloniaScriperContainer(string configurationFilePath, string uiConfigfurationFilePath)
            : base(configurationFilePath, uiConfigfurationFilePath)
        {
            BuildAvaloniaApp()
                .SetupWithoutStarting();
        }

        private static AppBuilder BuildAvaloniaApp() =>
            AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();

        public void Verify()
        {
            _container.Verify();
        }
    }
}
