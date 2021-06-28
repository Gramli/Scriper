using NLog;
using ReactiveUI;
using System;
using System.Reactive;

namespace Scriper.Extensions
{
    public static class ReactiveCommandExtensions
    {
        public static ReactiveCommand<Unit, Unit> CatchError(this ReactiveCommand<Unit, Unit> command, Logger logger)
        {
            command.ThrownExceptions.Subscribe(ex => { LogErrorAndShowDialog(ex, logger); });
            return command;
        }

        public static ReactiveCommand<string, Unit> CatchError(this ReactiveCommand<string, Unit> command, Logger logger)
        {
            command.ThrownExceptions.Subscribe(ex => { LogErrorAndShowDialog(ex, logger); });
            return command;
        }

        private static void LogErrorAndShowDialog(Exception ex, Logger logger)
        {
            logger.Error(ex);

            var message = ex.Message;

            switch (ex)
            {
                case UnauthorizedAccessException unauthorizedAccessException:
                    message = $"{unauthorizedAccessException.Message}\n\nPlease run Scriper as an Administrator!";
                    break;
            }
            MessageBoxExtensions.ShowDialog(message);
        }
    }
}
