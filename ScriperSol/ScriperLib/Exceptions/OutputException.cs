using System;

namespace ScriperLib.Exceptions
{
    public class OutputException : Exception
    {
        public OutputException(string message)
            : base(message)
        {
        }
    }
}
