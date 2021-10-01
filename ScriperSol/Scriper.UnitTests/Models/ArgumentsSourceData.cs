using System.Collections;

namespace Scriper.UnitTests.Models
{
    public class ArgumentsSourceData : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new object[] {  "data", new[] { "data" } };
            yield return new object[] { "data sata", new[] { "data", "sata" } };
            yield return new object[] { "-data -sata", new[] { "-data", "-sata" } };
            yield return new object[] { "-arg1 data -arg2 sata", new[] { "-arg1 data", "-arg2 sata" } };
            yield return new object[] { "-arg1 data -arg2 sata -set", new[] { "-arg1 data", "-arg2 sata", "-set" } };
        }
    }
}
