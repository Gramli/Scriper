using System.Collections.Generic;

namespace ScriperLib
{
    public interface IScriptResult
    {
        bool HasErrors { get; }

        IReadOnlyCollection<string> ErrorCollection { get; }

        long ElapsedMilliseconds { get; }

        object Result { get; }
    }
}
