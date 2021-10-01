using System.Collections.Generic;

namespace Scriper.Models
{
    public interface IArgumentsSplitter
    {
        public IEnumerable<string> SplitArguments(string arguments);

        public string JoinArguments(IEnumerable<string> arguments);
    }
}
