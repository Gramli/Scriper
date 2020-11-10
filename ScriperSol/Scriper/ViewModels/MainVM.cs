using NLog;
using ReactiveUI;
using Scriper.Configuration;
using Scriper.Extensions;
using Scriper.Views;
using ScriperLib;
using ScriperLib.Extensions;
using System;
using System.Reactive;

namespace Scriper.ViewModels
{
    public class MainVM : ViewModelBase
    {
        public ScriptManagerVM ScriptManagerVM { get; private set; }
        public ReactiveCommand<string, Unit> CreateScriptCmd { get; }
        public ReactiveCommand<Unit, Unit> ExitCmd { get; }

        public ReactiveCommand<Unit, Unit> OpenSettingsCmd { get; }

        private static readonly Logger logger = NLogExtensions.LogFactory.GetCurrentClassLogger();

        private IScriperUIConfiguration _uiConfig;
        public MainVM(IScriperLibContainer container, IScriperUIConfiguration uiConfig)
        {
            _uiConfig = uiConfig;
            ScriptManagerVM = new ScriptManagerVM(container, _uiConfig);
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
                var settingsVM = new SettingsVM(_uiConfig.DeepClone());
                var settingsWindow = new SettingsWindow(settingsVM);

                settingsVM.Close += (sender, args) =>
                {
                    if(!args.Cancel)
                    {
                        _uiConfig = args.Result;
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
