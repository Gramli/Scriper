using ScriperLib.Extensions;
using System;
using System.IO;

namespace Scriper.ViewModels.Validation.Validators
{
    class ScriptExtensionValidator : IValidator<string>
    {
        public Func<string> GetValueToValidate { get; }
        public Action<string> InvalidCallback { get; }
        public ScriptExtensionValidator(Func<string> getValueToValidate, Action<string> invalidCallback)
        {
            GetValueToValidate = getValueToValidate;
            InvalidCallback = invalidCallback;
        }

        public bool Validate()
        {
            var scriptPath = GetValueToValidate();
            if (Path.GetExtension(scriptPath).TryGetScriptType(out var scriptType))
            {
                return true;
            }

            InvalidCallback("Uknown script(file) type.");
            return false;
        }
    }
}
