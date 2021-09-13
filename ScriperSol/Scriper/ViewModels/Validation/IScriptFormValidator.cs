using Scriper.ViewModels.Validation.Validators;
using System;

namespace Scriper.ViewModels.Validation
{
    public interface IScriptFormValidator : IValidate
    {
        IScriptFormValidator AddNameValidator(Func<string> getName, Action<string> invalidCallback);
        IScriptFormValidator AddConfigValidators(Func<string> getConfigPath, Action<string> invalidCallback);
        IScriptFormValidator AddImageValidator(Func<string> getImagePath, Action<string> invalidCallback);
    }
}
