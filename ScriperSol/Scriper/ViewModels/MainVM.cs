using NLog;
using ReactiveUI;
using Scriper.Extensions;
using ScriperLib;
using System;
using System.Reactive;

namespace Scriper.ViewModels
{
    public class MainVM : ViewModelBase
    {
        public ScriptManagerVM ScriptManagerVM { get; private set; }
        public ReactiveCommand<string, Unit> CreateScriptCmd { get; }
        public ReactiveCommand<Unit, Unit> ExitCmd { get; }

        private static readonly Logger logger = NLogExtensions.LogFactory.GetCurrentClassLogger();
        public MainVM(IScriperLibContainer container)
        {
            ScriptManagerVM = new ScriptManagerVM(container);
            CreateScriptCmd = ReactiveCommand.Create<string>(CreateScript);
            ExitCmd = ReactiveCommand.Create(Exit);
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
    }
}
