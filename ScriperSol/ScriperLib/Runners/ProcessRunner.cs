using ScriperLib.Enums;
using ScriperLib.Extensions;
using ScriperLib.Outputs;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ScriperLib.Runners
{
    internal class ProcessRunner : IRunner
    {
        public ScriptType[] ScriptTypes => new[] { ScriptType.ExeFile, ScriptType.WindowsProcess, ScriptType.LinuxShell };

        public ProcessRunner()
        {
        }

        public IScriptResult Run(IScript script)
        {
            switch(script.ScriptType)
            {
                case ScriptType.ExeFile:
                    RunExe(script);
                    break;
                case ScriptType.WindowsProcess:
                    RunBat(script);
                    break;
            }

            return null;
        }

        public Task<IScriptResult> RunAsync(IScript script)
        {
            return Task.Factory.StartNew(() =>
            {
                return Run(script);
            });
        }

        private void RunExe(IScript script)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = script.Configuration.Path,
                    UseShellExecute = true,
                    RedirectStandardError = false,
                    RedirectStandardOutput = false,
                    Arguments = script.Configuration.Arguments,
                }
            };

            process.Start();
        }
        private void RunBat(IScript script)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = script.Configuration.Path,
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    Arguments = script.Configuration.Arguments,
                }
            };

            process.OutputDataReceived += (object sender, DataReceivedEventArgs e) => WriteOutputs(script.Outputs, e.Data);
            process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) => WriteOutputs(script.Outputs, e.Data.FormatError());

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();

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
