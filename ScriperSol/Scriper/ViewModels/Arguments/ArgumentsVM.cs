using Avalonia.Collections;
using Scriper.Models;
using System;
using System.Linq;

namespace Scriper.ViewModels.Arguments
{
    internal class ArgumentsVM : ViewModelBase, IArgumentsVM
    {
        public AvaloniaList<IArgumentVM> Arguments { get; private set; }

        private readonly Func<string, IArgumentVM> _createIArgumentVM;
        private readonly IArgumentsSplitter _argumentsSplitter;
        public ArgumentsVM(IArgumentsSplitter argumentsSplitter,
            Func<string, IArgumentVM> createIArgumentVM)
        {
            _argumentsSplitter = argumentsSplitter;
            _createIArgumentVM = createIArgumentVM;
        }

        public void Init(string arguments)
        {
            var splittedArguments = _argumentsSplitter.SplitArguments(arguments);
            Arguments = new AvaloniaList<IArgumentVM>();
            foreach (var arg in splittedArguments)
            {
                AddNewArgument(arg);
            }
            AddNewEmptyArgument();
        }

        private void EmptyArg_OnValueChanged(object sender, EventArgs e)
        {
            var senderArg = (IArgumentVM)sender;
            if(!string.IsNullOrEmpty(senderArg.Value))
            {
                senderArg.OnValueChanged -= EmptyArg_OnValueChanged;
                senderArg.IsEmpty = false;
                AddNewEmptyArgument();
            }
        }

        private void AddNewEmptyArgument()
        {
            var emptyArg = AddNewArgument("", true);
            emptyArg.OnValueChanged += EmptyArg_OnValueChanged;
        }

        private IArgumentVM AddNewArgument(string value, bool isEmpty = false)
        {
            var argumentVM = _createIArgumentVM(value);
            argumentVM.IsEmpty = isEmpty;
            argumentVM.OnDelete += ArgumentVM_OnDelete;
            Arguments.Add(argumentVM);
            return argumentVM;
        }

        private void ArgumentVM_OnDelete(object sender, EventArgs e)
        {
            var senderArg = (IArgumentVM)sender;
            if (senderArg.IsEmpty)
            {
                return;
            }
            senderArg.OnDelete -= ArgumentVM_OnDelete;
            Arguments.Remove(senderArg);
        }

        public string GetArguments()
        {
            var arguments = Arguments.Select(i => i.Value).SkipLast(1);
            return _argumentsSplitter.JoinArguments(arguments);
        }
    }
}
