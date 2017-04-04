using Sce.Atf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeifenLuo.WinFormsUI.Docking;

namespace EditorCore.Controls
{
    /// <summary>
    /// Baseclass for dock content that contains a document
    /// </summary>
    public class DocumentDockContent : DockContent
    {
        public IDocument Document { get; protected set; }

        public DocumentDockContent(IDocument document)
        {
            Document = document;
            document.UriChanged += document_UriChanged;
            document.DirtyChanged += document_DirtyChanged;
            SetName();

            DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            FormClosing += DocumentDockContent_FormClosing;
        }

        void document_DirtyChanged(object sender, EventArgs e)
        {
            SetName();
        }

        void SetName()
        {
            if (Document.Uri != null && Document.Uri.IsFile)
            {
                if (Document.Dirty)
                    Text = string.Format("*{0}", System.IO.Path.GetFileName(Document.Uri.LocalPath));
                else
                    Text = System.IO.Path.GetFileName(Document.Uri.LocalPath);
            }
            else //no URI means it must be new
                Text = string.Format("*New {0}".Localize(), Document.Type);
        }

        void document_UriChanged(object sender, UriChangedEventArgs e)
        {
            SetName();
        }

        void DocumentDockContent_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (Document.Dirty)
            {
                //Property for save
                Sce.Atf.Controls.ConfirmationDialog dlg = new Sce.Atf.Controls.ConfirmationDialog("Close without saving?".Localize(), "Close altered document without saving?".Localize());
                System.Windows.Forms.DialogResult result = dlg.ShowDialog();
                
                if (result == System.Windows.Forms.DialogResult.No)
                {
                    //\todo send a save command
                }
                else if (result == System.Windows.Forms.DialogResult.Cancel)
                {
                    // Cancel the close operation
                    e.Cancel = true;
                    return;
                }
            }
            DocumentManager.GetInst().Remove(Document);
        }
    }
}
