using System.IO;
using System.Text;

namespace ScriperLib.Extensions
{
    public class NotifyStreamWriter : StreamWriter
    {
        public override Encoding Encoding => Encoding.UTF8;

        public event WriteEventHandler OnWrite;

        public NotifyStreamWriter(Stream stream)
            : base(stream)
        {
        }
        public override void Write(string value)
        {
            Invoke(value);
            base.Write(value);
        }

        public override void Write(bool value)
        {
            Invoke(value);
            base.Write(value);
        }

        public override void Write(char value)
        {
            Invoke(value);
            base.Write(value);
        }

        public override void Write(StringBuilder value)
        {
            Invoke(value);
            base.Write(value);
        }

        public override void Write(object value)
        {
            Invoke(value);
            base.Write(value);
        }

        private void Invoke(object value)
        {
            OnWrite.Invoke(this, new WriteEventArgs(value.ToString()));
        }
    }
}
