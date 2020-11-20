using Jint;
using Jint.Parser.Ast;
using ScriperLib.Enums;
using ScriperLib.Outputs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ScriperLib.Runners
{
    internal class JavascriptRunner : IRunner
    {
        public ScriptType[] ScriptTypes => new[] { ScriptType.Javascript };

        public IScriptResult Run(IScript script)
        {
            Action<object> writeAction = (object obj) => { WriteOutputs(script.Outputs, obj); };
            Action<object,string> writeActionFormated = (object obj, string format) => 
            {
                var formated = string.Format(format, obj);
                WriteOutputs(script.Outputs, formated); 
            };
            var engine = new Engine()
                .SetValue("logf", writeActionFormated)
                .SetValue("log", writeAction);

            var scriptContent = File.ReadAllText(script.Configuration.Path);

            engine.Execute(scriptContent);

            return null;
        }

        public Task<IScriptResult> RunAsync(IScript script)
        {
            return Task.Factory.StartNew(() =>
            {
                return Run(script);
            });
        }

        private void WriteOutputs(ICollection<IOutput> outputs, object message)
        {
            foreach (var output in outputs)
            {
                output.WriteOutput(message.ToString());
            }
        }
    }
}
