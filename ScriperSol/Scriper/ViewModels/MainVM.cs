using NLog;
using ReactiveUI;
using Scriper.Configuration;
using Scriper.Extensions;
using Scriper.Views;
using ScriperLib;
using ScriperLib.Extensions;
using System;
using System.Reactive;
using Scriper.SystemTray;

namespace Scriper.ViewModels
{
    public class MainVM : ViewModelBase
    {
        public ScriptManagerVM ScriptManagerVM { get; }
        public ReactiveCommand<string, Unit> CreateScriptCmd { get; }
        public ReactiveCommand<Unit, Unit> ExitCmd { get; }

        public ReactiveCommand<Unit, Unit> OpenSettingsCmd { get; }

        private static readonly Logger logger = NLogExtensions.LogFactory.GetCurrentClassLogger();

        public IScriperUIConfiguration ActualUiConfiguration { get; private set; }
        public MainVM(IScriperLibContainer container, IScriperUIConfiguration uiConfig, ISystemTrayMenu systemTrayMenu)
        {
            ActualUiConfiguration = uiConfig;
            ScriptManagerVM = new ScriptManagerVM(container, uiConfig, systemTrayMenu);
            CreateScriptCmd = ReactiveCommand.Create<string>(CreateScript);
            ExitCmd = ReactiveCommand.Create(Exit);
            OpenSettingsCmd = ReactiveCommand.Create(OpenSettings);
        }

        public void Exit()
        {
            App.Current.Close();
        }

        public void CreateScript(string argument)
        {
            try
            {
                ScriptManagerVM.CreateScript();
            }
            catch (Exception ex)
            {
                MessageBoxExtensions.Show(ex.Message);
                logger.Error(ex);
            }
        }

        public void OpenSettings()
        {
            try
            {
                var settingsVM = new SettingsVM(ActualUiConfiguration.DeepClone());
                var settingsWindow = new SettingsWindow(settingsVM);

                settingsVM.Close += (sender, args) =>
                {
                    if(!args.Cancel)
                    {
                        ActualUiConfiguration = args.Result;
                    }
                    settingsWindow.Close();
                };

                settingsWindow.ShowDialog(App.Current.GetMainWindow());
            }
            catch(Exception ex)
            {
                MessageBoxExtensions.Show(ex.Message);
                logger.Error(ex);
            }
        }
    }
}
