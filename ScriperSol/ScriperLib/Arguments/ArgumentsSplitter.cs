using System.Collections.Generic;
using System.Linq;

namespace ScriperLib.Arguments
{
    public class ArgumentsSplitter : IArgumentsSplitter
    {
        public string JoinArguments(IEnumerable<string> arguments)
        {
           return string.Join(" ", arguments);
        }

        public IEnumerable<string> SplitArguments(string arguments)
        {
            if(arguments is null || !arguments.Any())
            {
                return new List<string>();
            }

            var rawArgs = SplitBySpace(arguments);
            return SplitByName(rawArgs);
        }

        private IEnumerable<string> SplitByName(IList<string> rawArguments)
        {
            if(rawArguments.Count == 1)
            {
                return new List<string> { rawArguments.First() };
            }

            var result = new List<string>();
            for (var i = 0; i < rawArguments.Count; i++)
            {
                var rawArgument = rawArguments[i];
                if (IsArgumentName(rawArgument))
                {
                    var next = i + 1 < rawArguments.Count ? rawArguments[i + 1] : null;
                    if (!string.IsNullOrEmpty(next) && !IsArgumentName(rawArguments[i+1]))
                    {
                        rawArgument = $"{rawArgument} {rawArguments[i + 1]}";
                        result.Add(rawArgument);
                        i++;
                        continue;
                    }
                    result.Add(rawArgument);
                    continue;
                }
                result.Add(rawArgument);
            }

            return result;
        }

        public bool IsArgumentName(string value)
        {
            return value.StartsWith('-');
        }

        public IList<string> SplitBySpace(string arguments)
        {
            arguments = arguments.Trim();
            var result = new List<string>();
            var isEscape = false;
            var lastSubstringIndex = 0;
            for(var i=0; i<arguments.Length; i++)
            {
                var character = arguments[i];

                if(character == '"' || character == '\'')
                {
                    isEscape = !isEscape;
                    continue;
                }

                if(!isEscape && character == ' ')
                {
                    result.Add(arguments.Substring(lastSubstringIndex, i - lastSubstringIndex));
                    lastSubstringIndex = i + 1;
                }
            }
            result.Add(arguments.Substring(lastSubstringIndex));
            return result;
        }
    }
}
