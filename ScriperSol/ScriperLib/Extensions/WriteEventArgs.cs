using System;

namespace ScriperLib.Extensions
{
    public class WriteEventArgs : EventArgs
    {
        public string Text { get; private set; }

        public WriteEventArgs(string text)
        {
            Text = text;
        }
    }
}
