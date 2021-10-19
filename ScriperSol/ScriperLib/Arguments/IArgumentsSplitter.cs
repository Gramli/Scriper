using System.Collections.Generic;

namespace ScriperLib.Arguments
{
    public interface IArgumentsSplitter
    {
        IEnumerable<string> SplitArguments(string arguments);

        string JoinArguments(IEnumerable<string> arguments);

        bool IsArgumentName(string value);

        IList<string> SplitBySpace(string arguments);
    }
}
