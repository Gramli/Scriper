using System;

namespace Scriper.ViewModels.Arguments
{
    interface IArgumentVM
    {
        string Value { get; set; }
        bool IsEmpty { get; set; }

        event EventHandler OnDelete;
        event EventHandler OnValueChanged;
    }
}
