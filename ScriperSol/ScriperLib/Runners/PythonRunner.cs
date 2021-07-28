using IronPython.Hosting;
using ScriperLib.Enums;
using ScriperLib.Extensions;
using ScriperLib.Outputs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.Scripting.Hosting;

namespace ScriperLib.Runners
{
    public class PythonRunner : IRunner
    {
        public ScriptType[] ScriptTypes => new[] { ScriptType.PythonFile };

        public IScriptResult Run(IScript script)
        {
            //inicialize stream and writers
            using var memoryStream = new MemoryStream();
            using var writer = new NotifyStreamWriter(memoryStream);
            using var writerError = new NotifyStreamWriter(memoryStream);
            writer.OnWrite += (sender, args) =>{ WriteOutputs(script.Outputs, args.Text); };
            writerError.OnWrite += (sender, args) =>{ WriteOutputs(script.Outputs, args.Text.FormatError()); };

            //create engine and set what I need
            var engine = Python.CreateEngine();
            AddPaths(engine, script);
            engine.Runtime.IO.SetOutput(memoryStream, writer);
            engine.Runtime.IO.SetErrorOutput(memoryStream, writerError);
            var scope = engine.CreateScope();
            var pythonScript = engine.CreateScriptSourceFromFile(script.Configuration.Path);
            var compiled = pythonScript.Compile();

            //execute and catch exceptions
            try
            {
                if (compiled.Execute(scope) is object result)
                {
                    writer.Write(result);
                }
            }
            catch(Exception ex)
            {
                writerError.Write(ex);
            }

            return null;
        }

        private void AddPaths(ScriptEngine engine, IScript script)
        {
            var paths = engine.GetSearchPaths();
            paths.Add(Path.GetDirectoryName(script.Configuration.Path));

            const string importStart = "#${";

            using var reader = new StreamReader(script.Configuration.Path);
            while (true)
            {
                var line = reader.ReadLine();

                if (line is null)
                {
                    break;
                }
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                if (line.Contains(importStart))
                {
                    var start = line.IndexOf(importStart);
                    var end = line.IndexOf("}");

                    if (end < start)
                    {
                        //throw
                    }

                    var path = line.Substring(start + importStart.Length, end - (start+ importStart.Length));
                    paths.Add(path);
                }
            }

            engine.SetSearchPaths(paths);
        }

        public Task<IScriptResult> RunAsync(IScript script)
        {
            return Task.Factory.StartNew(() => Run(script));
        }

        private void WriteOutputs(ICollection<IOutput> outputs, string message)
        {
            foreach (var output in outputs)
            {
                output.WriteOutput(message);
            }
        }
    }
}
