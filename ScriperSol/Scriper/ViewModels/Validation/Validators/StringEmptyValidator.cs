using System;

namespace Scriper.ViewModels.Validation.Validators
{
    class StringEmptyValidator : IValidator<string>
    {
        public Func<string> GetValueToValidate { get; }
        public Action<string> InvalidCallback { get; }

        private readonly string _exceptionMsg;

        public StringEmptyValidator(Func<string> getValueToValidate, string exceptionMsg, Action<string> invalidCallback)
        {
            GetValueToValidate = getValueToValidate;
            _exceptionMsg = exceptionMsg;
            InvalidCallback = invalidCallback;
        }

        public bool Validate()
        {
            var name = GetValueToValidate();
            if (!string.IsNullOrEmpty(name))
            {
                return true;
            }

            InvalidCallback(_exceptionMsg);
            return false;
        }
    }
}
