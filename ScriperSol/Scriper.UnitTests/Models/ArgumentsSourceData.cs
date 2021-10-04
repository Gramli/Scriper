using System.Collections;
using System.Collections.Generic;

namespace Scriper.UnitTests.Models
{
    public class ArgumentsSourceData : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new object[] { "", new List<string>() };
            yield return new object[] {  "data", new List<string>() { "data" } };
            yield return new object[] { "data sata", new List<string>() { "data", "sata" } };
            yield return new object[] { "\"data sata\" \"C:\\\\Propgrasda x64\\sdasdasd\"", new List<string>() { "\"data sata\"", "\"C:\\\\Propgrasda x64\\sdasdasd\"" } };
            yield return new object[] { "-data -sata", new List<string>() { "-data", "-sata" } };
            yield return new object[] { "-arg1 data -arg2 sata", new List<string>() { "-arg1 data", "-arg2 sata" } };
            yield return new object[] { "-arg1 data -arg2 sata -set", new List<string>() { "-arg1 data", "-arg2 sata", "-set" } };
            yield return new object[] { "-arg1 \"data asddas\" -arg2 sata -set", new List<string>() { "-arg1 \"data asddas\"", "-arg2 sata", "-set" } };
        }
    }
}
