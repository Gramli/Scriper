using ScriperLib.Enums;
using ScriperLib.Outputs;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ScriperLib.Extensions;
using System.Management.Automation;
using ScriperLib.Arguments;

namespace ScriperLib.Runners
{
    internal class PowerShellRunner : IRunner
    {
        public ScriptType[] ScriptTypes => new[] { ScriptType.PowerShell1, ScriptType.PowerShell2 };

        private readonly IPowerShellArgumentsSplitter _powerShellArgumentsSplitter;

        public PowerShellRunner(IPowerShellArgumentsSplitter powerShellArgumentsSplitter)
        {
            _powerShellArgumentsSplitter = powerShellArgumentsSplitter;
        }

        public IScriptResult Run(IScript script)
        { 
            using var powerShell = PowerShell.Create();
            var content = File.ReadAllText(script.Configuration.Path);
            powerShell.AddScript(content);

            if (!string.IsNullOrEmpty(script.Configuration.Arguments))
            {
                var argsAndParams = _powerShellArgumentsSplitter.Get(script.Configuration.Arguments);

                foreach(var argument in argsAndParams.Arguments)
                {
                    powerShell.AddArgument(argument);
                }

                foreach(var parameter in argsAndParams.Parameters)
                {
                    powerShell.AddParameter(parameter.Key, parameter.Value);
                }
            }

            var pipelineObjects = powerShell.Invoke();

            // print the resulting pipeline objects to the console.
            foreach (var item in pipelineObjects)
            {
                WriteOutputs(script.Outputs, item.ToString());
            }

            if (powerShell.HadErrors)
            {
                foreach (var item in powerShell.Streams.Error)
                {
                    var formated = $"Exception Message: {item.Exception.Message}; Stack Trace{item.Exception.StackTrace}; \n + CategoryInfo: {item.CategoryInfo} \n + FullyQualifiedErrorId: {item.FullyQualifiedErrorId}".FormatError();
                    WriteOutputs(script.Outputs, formated);
                }
            }

            return null;
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
