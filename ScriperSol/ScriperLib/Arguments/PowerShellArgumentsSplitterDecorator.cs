using System;
using System.Collections.Generic;

namespace ScriperLib.Arguments
{
    internal class PowerShellArgumentsSplitterDecorator : IPowerShellArgumentsSplitter
    {
        private readonly IArgumentsSplitter _argumentsSplitter;
        internal PowerShellArgumentsSplitterDecorator(IArgumentsSplitter argumentsSplitter)
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
                    var nameAndValue = arg.Split(" ");

                    if(nameAndValue.Length != 2)
                    {
                        throw new ArgumentException("PowerShell Parameter does not contains name and value.");
                    }

                    parameters.Add(nameAndValue[0], nameAndValue[1]);
                    continue;
                }

                arguments.Add(arg);
            }

            return new PowerShellScriptInputs() { Arguments = arguments, Parameters = parameters };
        }
    }
}
