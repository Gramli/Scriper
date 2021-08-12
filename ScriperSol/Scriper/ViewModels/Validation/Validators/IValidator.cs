using System;

namespace Scriper.ViewModels.Validation.Validators
{
    interface IValidator<T> : IValidate
    {
        Action<string> InvalidCallback { get; }
        Func<T> GetValueToValidate { get; }
    }
}
