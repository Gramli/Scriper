using IronPython.Hosting;
using ScriperLib.Enums;
using ScriperLib.Extensions;
using ScriperLib.Outputs;
using System.Collections.Generic;
using System.IO;

namespace ScriperLib.Runners
{
    public class PythonRunner : IScriptRunner
    {
        public ScriptType[] ScriptTypes => new[] { ScriptType.PythonFile };

        public void Run(IScript script)
        {
            using var memoryStream = new MemoryStream();
            using var writer = new NotifyStreamWriter(memoryStream);
            writer.OnWrite += (sender, args) =>
            {
                WriteOutputs(script.Outputs, args.Text);
            };
            var engine = Python.CreateEngine();
            engine.Runtime.IO.SetOutput(memoryStream, writer);
            var scope = engine.CreateScope();
            var pythonScript = engine.CreateScriptSourceFromFile(script.Configuration.Path);
            var compiled = pythonScript.Compile();
            var result = compiled.Execute(scope);
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
