using System.Collections.Generic;
using System.Linq;

namespace Scriper.Models
{
    public class ArgumentsSplitter : IArgumentsSplitter
    {
        public string JoinArguments(IEnumerable<string> arguments)
        {
           return string.Join(" ", arguments);
        }

        public IEnumerable<string> SplitArguments(string arguments)
        {
            var rawArgs = arguments.Split(" ");
            if(!IsArgumentName(rawArgs.First()))
            {
                return rawArgs;
            }

            return SplitNamedArguments(rawArgs);
        }

        private IEnumerable<string> SplitNamedArguments(string[] rawArguments)
        {
            if(rawArguments.Length == 1)
            {
                return new List<string> { rawArguments.First() };
            }

            var result = new List<string>();
            for (var i = 0; i < rawArguments.Length; i++)
            {
                if (IsArgumentName(rawArguments[i]))
                {
                    var argument = rawArguments[i];
                    var next = i + 1 < rawArguments.Length ? rawArguments[i + 1] : null;
                    if (!string.IsNullOrEmpty(next) && IsArgumentValue(rawArguments[i+1]))
                    {
                        argument = $"{argument} {rawArguments[i + 1]}";
                    }
                    result.Add(argument);
                }
            }

            return result;
        }

        private bool IsArgumentName(string value)
        {
            return value.StartsWith('-');
        }

        private bool IsArgumentValue(string value)
        {
            return !value.StartsWith('-');
        }
    }
}
