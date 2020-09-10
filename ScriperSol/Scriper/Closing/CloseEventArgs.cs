using System;

namespace Scriper.Closing
{
    public class CloseEventArgs<T> : EventArgs
    {
        public bool Cancel { get; private set; }

        public T Result { get; private set; }

        public CloseEventArgs()
        {
        }
        public CloseEventArgs(T result)
        {
            Result = result;
        }

    }
}
