using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using EditorCore;
using Sce.Atf;

namespace EditorCore.Panels
{
    [PanelHint(WeifenLuo.WinFormsUI.Docking.DockState.DockBottomAutoHide, true)]
    public partial class LogPanel : Controls.PanelDockContent
    {
        public LogPanel()
        {
            InitializeComponent();
            Text = "Console".Localize();

            DocumentManager.GetInst().DocumentChanged += LogPanel_DocumentChanged;
        }

        void LogPanel_DocumentChanged(object sender, DocumentChangedEventArgs args)
        {
            Documents.ILoggingDocument logDoc = args.New as Documents.ILoggingDocument;
            if (logDoc != null)
            {
                richTextBox1.Text = "";
                logDoc.LogAppended += logDoc_LogAppended;
                foreach (string str in logDoc.Log)
                    richTextBox1.Text += str + "\r\n";
            }
            else
                richTextBox1.Text = "";

        }

        void logDoc_LogAppended(object msg, EventArgs args)
        {
            richTextBox1.Text += ((Documents.ILoggingDocument)msg).Log.Last() + "\r\n";
        }
    }
}
