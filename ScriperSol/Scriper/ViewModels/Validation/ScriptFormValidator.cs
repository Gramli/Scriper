using Scriper.ViewModels.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scriper.ViewModels.Validation
{
    internal class ScriptFormValidator : IScriptFormValidator
    {
        private readonly IList<IValidate> _validators;

        public ScriptFormValidator()
        {
            _validators = new List<IValidate>();
        }

        public IScriptFormValidator AddNameValidator(Func<string> getName, Action<string> invalidCallback)
        {
            _validators.Add(new StringEmptyValidator(getName, "Script name is empty.",invalidCallback));
            return this;
        }

        public IScriptFormValidator AddConfigValidators(Func<string> getConfigPath, Action<string> invalidCallback)
        {
            _validators.Add(new StringEmptyValidator(getConfigPath, "Script path is empty.", invalidCallback));
            _validators.Add(new ScriptExtensionValidator(getConfigPath, invalidCallback));
            return this;
        }

        public bool Validate()
        {
            return !_validators.Any(item => !item.Validate());
        }
    }
}
