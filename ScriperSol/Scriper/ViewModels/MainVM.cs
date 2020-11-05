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

        public ReactiveCommand<Unit, Unit> EditScriptInDefaultEditorCmd { get; }

        private static readonly Logger logger = NLogExtensions.LogFactory.GetCurrentClassLogger();
        public MainVM(IScriperLibContainer container)
        {
            ScriptManagerVM = new ScriptManagerVM(container);
            CreateScriptCmd = ReactiveCommand.Create<string>(CreateScript);
            ExitCmd = ReactiveCommand.Create(Exit);
            EditScriptInDefaultEditorCmd = ReactiveCommand.Create(EditScriptInDefaultEditor);
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

        public void EditScriptInDefaultEditor()
        {
            try
            {

            }
            catch(Exception ex)
            {

            }
        }
    }
}
