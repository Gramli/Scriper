using ReactiveUI;
using System;
using System.Reactive;

namespace Scriper.ViewModels.Arguments
{
    public class ArgumentVM : ViewModelBase, IArgumentVM
    {
        private string _value;
        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                OnValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public ReactiveCommand<Unit, Unit> DeleteCmd { get; }

        public event EventHandler OnDelete;
        public event EventHandler OnValueChanged;
        private bool _isEmpty;
        public bool IsEmpty
        {
            get => _isEmpty;
            set
            {
                _isEmpty = value;
                this.RaisePropertyChanged("IsEmpty");
            }
        }

        public ArgumentVM(string value)
        {
            Value = value;
            DeleteCmd = ReactiveCommand.Create(() => OnDelete?.Invoke(this, EventArgs.Empty));
        }
    }
}
