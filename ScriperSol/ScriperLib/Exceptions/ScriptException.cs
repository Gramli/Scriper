using System;

namespace ScriperLib.Exceptions
{
    public class ScriptException : Exception
    {
        public ScriptException(string message)
            : base(message)
        {
        }
    }
}
