using System.IO;
using System.Text;

namespace ScriperLib.Extensions
{
    public class NotifyTextWriter : TextWriter
    {
        public override Encoding Encoding => Encoding.UTF8;

        public event WriteEventHandler OnWrite;
    }
}
