using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ScriperLib.Arguments
{
    internal class PowerShellArgumentsSplitterDecorator : IPowerShellArgumentsSplitter
    {
        private readonly IArgumentsSplitter _argumentsSplitter;
        public PowerShellArgumentsSplitterDecorator(IArgumentsSplitter argumentsSplitter)
        {
            _argumentsSplitter = argumentsSplitter;
        }

        public PowerShellScriptInputs Get(string rawData)
        {
            var parameters = new Dictionary<string, string>();
            var arguments = new List<string>();

            var splittedArguments = _argumentsSplitter.SplitArguments(rawData);
            foreach(var arg in splittedArguments)
            {
                if(_argumentsSplitter.IsArgumentName(arg))
                {
                    var nameAndValue = _argumentsSplitter.SplitBySpace(arg);

                    if(nameAndValue.Count == 1)
                    {
                        parameters.Add(nameAndValue[0], string.Empty);
                        continue;
                    }

                    var value = RemoveQuotationMarks(nameAndValue[1]);
                    parameters.Add(nameAndValue[0], value);
                    continue;
                }

                arguments.Add(arg);
            }

            return new PowerShellScriptInputs() { Arguments = arguments, Parameters = parameters };
        }

        private string RemoveQuotationMarks(string value)
        {
            if (Regex.IsMatch(value, "^((\\\".+\\\")|(\\\'.+\\\'))$"))
            {
                return value[1..^1];
            }

            return value;
        }
    }
}
