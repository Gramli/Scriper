using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using ScriperLib.Enums;
using ScriperLib.Extensions;
using ScriperLib.Outputs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

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

            var scriptPaths = ModulePathExtractor.ExtractPaths(script.Configuration.Path);
            foreach (var path in scriptPaths)
            {
                paths.Add(path);   
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
