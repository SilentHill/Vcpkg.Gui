using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcpkg.Core
{
    public abstract class CmdletSession
    {
        public async Task<String> RunCmdlet(String args, bool lineByLine = false)
        {
            String outputString = String.Empty;
            var process = new Process();
            process.StartInfo.Arguments = args;
            process.StartInfo.FileName = CmdletPath;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;

            if (lineByLine)
            {
                var sb = new StringBuilder();
                process.OutputDataReceived += (sender, args) =>
                {
                    sb.AppendLine(args.Data);
                };
                process.Start();
                process.BeginOutputReadLine();
                await process.WaitForExitAsync();
                outputString = sb.ToString();
            }
            else
            {
                process.Start();
                outputString = await process.StandardOutput.ReadToEndAsync();
            }
            return outputString;
        }

        public String CmdletPath { get; set; }
    }
}
