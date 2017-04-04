using Sce.Atf.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditorCore
{
    public sealed class ErrorInfo
    {
        public string ErrorMessage {get;set;}
        public string Source { get; set; }
        public string ErrorDetails {get;set;}
    }

    [EditorCore.Interfaces.IProgramTerminated]
    [EditorCore.Interfaces.IProgramInitializer("Error Handler")]
    public sealed class ErrorHandler
    {
        static ErrorHandler inst_;
        public static ErrorHandler GetInst() { return inst_; }

        List<ErrorInfo> messages_ = new List<ErrorInfo>();
        Required<Settings.CoreSettings> CoreSettings = new Required<Settings.CoreSettings>();

        ErrorHandler()
        {

        }

        public void Error(Exception ex)
        {
            messages_.Add(new ErrorInfo { ErrorMessage = ex.Message, ErrorDetails = ex.StackTrace, Source = ex.Source });
            
            // Does the user want to see a message?
            if (!CoreSettings.Value.DontShowErrors)
            {
                DialogResult res = DialogResult.None;
                try
                {
                    var dlg = new UnhandledExceptionDialog();

                    // Call ToString() to get the call stack. The Message property may not include that.
                    dlg.ExceptionTextBox.Text = ex.ToString();
                    res = dlg.ShowDialog();
                }
                finally
                {
                    if (res == DialogResult.No)
                        MainWindow.inst().Terminate(); // Terminating from the main window gives termination a chance to do its work
                }
            }
        }

        static void ProgramInitialized()
        {
            inst_ = new ErrorHandler();
        }

        static void Terminate()
        {
            StringBuilder errorInfo = new StringBuilder();
            foreach (ErrorInfo info in inst_.messages_)
            {
                errorInfo.AppendLine("=======================================");
                errorInfo.AppendLine("---------------------------------------");
                errorInfo.AppendLine("::MESSAGE::");
                errorInfo.AppendLine("---------------------------------------");
                errorInfo.AppendLine(info.ErrorMessage);
                errorInfo.AppendLine("---------------------------------------");
                errorInfo.AppendLine("::DETAILS::");
                errorInfo.AppendLine("---------------------------------------");
                errorInfo.AppendLine(info.ErrorDetails);
                errorInfo.AppendLine("---------------------------------------");
                errorInfo.AppendLine("::SOURCE::");
                errorInfo.AppendLine("---------------------------------------");
                errorInfo.AppendLine(info.Source);
            }
            System.IO.File.WriteAllText(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), "ErrorLog.txt"), errorInfo.ToString());
        }
    }
}
