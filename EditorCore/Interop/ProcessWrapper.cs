using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorWinForms.Interop
{
    public class ProcessWrapper
    {
        Process process_;
        public ProcessStartInfo ProcessStart { get { return process_.StartInfo; } }
        StringBuilder output = new StringBuilder();

        public ProcessWrapper(string fileName)
        {
            process_ = new Process();
            process_.StartInfo.FileName = fileName;
            process_.EnableRaisingEvents = true;
            process_.StartInfo.UseShellExecute = false;
            process_.StartInfo.CreateNoWindow = true;
            process_.ErrorDataReceived += pi_ErrorDataReceived;
            process_.OutputDataReceived += pi_OutputDataReceived;
            process_.StartInfo.RedirectStandardError = true;
            process_.StartInfo.RedirectStandardOutput = true;
        }

        void pi_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            output.Append(e.Data);
        }

        void pi_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            output.Append(e.Data);
        }

        public string Run()
        {
            try
            {
                process_.Start();
                process_.BeginOutputReadLine();
                process_.BeginErrorReadLine();
                process_.WaitForExit();

                return output.ToString();
            } 
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
