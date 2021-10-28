using ScriperLib.Arguments;
using System.Collections;
using System.Collections.Generic;

namespace Scriper.UnitTests.Models
{
    public class PowerShellArgumentsData : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new object[] 
            { 
                "", 
                new PowerShellScriptInputs() 
                { 
                    Arguments = new List<string>(), 
                    Parameters = new Dictionary<string,string>() 
                }
            };
            yield return new object[] 
            { 
                "data", 
                new PowerShellScriptInputs() 
                { 
                    Arguments = new List<string>() { "data" }, 
                    Parameters = new Dictionary<string, string>() 
                } 
            };
            yield return new object[] 
            { 
                "data sata", 
                new PowerShellScriptInputs() 
                { 
                    Arguments = new List<string>() { "data", "sata" }, 
                    Parameters = new Dictionary<string, string>() 
                } 
            };
            yield return new object[] 
            { 
                "\"data sata\" \"C:\\\\Propgrasda x64\\sdasdasd\"",
                new PowerShellScriptInputs()
                {
                    Arguments = new List<string>() { "\"data sata\"", "\"C:\\\\Propgrasda x64\\sdasdasd\"" },
                    Parameters = new Dictionary<string, string>()
                }
            };
            yield return new object[] 
            { 
                "-data a -sata s",
                new PowerShellScriptInputs()
                {
                    Arguments = new List<string>(),
                    Parameters = new Dictionary<string, string>() 
                    {
                        {"-data", "a" },
                        {"-sata", "s" },
                    }
                }
            };
            yield return new object[] 
            { 
                "-arg1 data -arg2 sata",
                new PowerShellScriptInputs()
                {
                    Arguments = new List<string>(),
                    Parameters = new Dictionary<string, string>()
                    {
                        {"-arg1", "data" },
                        {"-arg2", "sata" },
                    }
                }
            };
            yield return new object[] 
            { 
                "-arg1 data -arg2 sata -set",
                new PowerShellScriptInputs()
                {
                    Arguments = new List<string>(),
                    Parameters = new Dictionary<string, string>()
                    {
                        {"-arg1", "data" },
                        {"-arg2", "sata" },
                        {"-set", "" },
                    }
                }
            };
            yield return new object[] 
            { 
                "-arg1 \"data asddas\" -arg2 sata 1 2 3",
                new PowerShellScriptInputs()
                {
                    Arguments = new List<string>(){ "1","2","3" },
                    Parameters = new Dictionary<string, string>()
                    {
                        {"-arg1", "data asddas" },
                        {"-arg2", "sata" },
                    }
                }
            };
            yield return new object[]
            {
                "-arg1 \"data asddas\" -arg2 sata 1 2 3 sadsa -set",
                new PowerShellScriptInputs()
                {
                    Arguments = new List<string>(){ "1","2","3","sadsa" },
                    Parameters = new Dictionary<string, string>()
                    {
                        {"-arg1", "data asddas" },
                        {"-arg2", "sata" },
                        {"-set", "" },
                    }
                }
            };
            yield return new object[]
            {
                "-arg1 \'data asddas\' -arg2 sata 1 2 3 sadsa -set",
                new PowerShellScriptInputs()
                {
                    Arguments = new List<string>(){ "1","2","3","sadsa" },
                    Parameters = new Dictionary<string, string>()
                    {
                        {"-arg1", "data asddas" },
                        {"-arg2", "sata" },
                        {"-set", "" },
                    }
                }
            };
            yield return new object[]
            {
                "-arg1 \'data asddas\' -arg2 \'sata fasa\' 1 2 3 sadsa -set",
                new PowerShellScriptInputs()
                {
                    Arguments = new List<string>(){ "1","2","3","sadsa" },
                    Parameters = new Dictionary<string, string>()
                    {
                        {"-arg1", "data asddas" },
                        {"-arg2", "sata fasa" },
                        {"-set", "" },
                    }
                }
            };

        }
    }
}
