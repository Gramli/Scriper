using ScriperLib.Enums;
using ScriperLib.Outputs;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace ScriperLib.Runners
{
    internal class ProcessRunner : IScriptRunner
    {
        public ScriptType[] ScriptTypes => new[] { ScriptType.ExeFile, ScriptType.WindowsProcess };

        public ProcessRunner()
        {
        }

        public void Run(IScript script)
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
