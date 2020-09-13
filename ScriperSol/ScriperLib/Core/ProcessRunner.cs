using ScriperLib.Enums;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace ScriperLib.Core
{
    internal class ProcessRunner : IScriptRunner
    {
        public ScriptType[] ScriptTypes => new[] { ScriptType.ExeFile, ScriptType.WindowsProcess };

        public ProcessRunner()
        {
        }

        public void Run(IScript script)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = script.Configuration.Path,
                    CreateNoWindow = script.Configuration.RunInNewWindow,
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true
                }
            };

            process.OutputDataReceived += (object sender, DataReceivedEventArgs e) => WriteOutputs(script.Outputs, $"output:: {e.Data}");
            process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) => WriteOutputs(script.Outputs, $"error:: {e.Data}");

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();

            WriteOutputs(script.Outputs, $"exitcode:: {process.ExitCode}");
            process.Close();
        }

        private void WriteOutputs(ICollection<IOutput> outputs, string message)
        {
            foreach(var output in outputs)
            {
                output.WriteOutput(message);
            }
        }
    }
}
