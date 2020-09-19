using ScriperLib.Enums;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Threading.Tasks;

namespace ScriperLib.Core
{
    internal class PowerShellRunner : IScriptRunner
    {
        public ScriptType[] ScriptTypes => new[] { ScriptType.PowerShell1, ScriptType.PowerShell2 };

        public async void Run(IScript script)
        {
            await RunScript(script);
        }

        private Task RunScript(IScript script)
        {
            return Task.Factory.StartNew(() =>
            {
                using var powerShell = PowerShell.Create();
                var content = File.ReadAllText(script.Configuration.Path);
                powerShell.AddScript(content);

                if (!string.IsNullOrEmpty(script.Configuration.Arguments))
                {
                    powerShell.AddArgument(script.Configuration.Arguments);
                }

                var pipelineObjects = powerShell.Invoke();

                // print the resulting pipeline objects to the console.
                foreach (var item in pipelineObjects)
                {
                    WriteOutputs(script.Outputs, item.ToString());
                }
            });
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
