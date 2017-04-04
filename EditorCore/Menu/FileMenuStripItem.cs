using Sce.Atf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditorCore.Menu
{
    /// <summary>
    /// Handles single or multiple document type "File" menus
    /// Single document types are traditional
    /// Multiple document types use child menus to select the document type for New/Open
    /// </summary>
    public class FileMenuStripItem : ToolStripMenuItem
    {
        public FileMenuStripItem(Type[] documentTypes) :
            base("&File".Localize())
        {
            Name = "MenuFile";
            // Construct Menu Items
            ToolStripMenuItem mNew = new ToolStripMenuItem("&New".Localize());
            ToolStripMenuItem mOpen = new ToolStripMenuItem("&Open".Localize());
            ToolStripMenuItem mClose = new ToolStripMenuItem("Close".Localize());

            ToolStripMenuItem mSave = new ToolStripMenuItem("&Save".Localize());
            ToolStripMenuItem mSaveAs = new ToolStripMenuItem("Save As".Localize());
            ToolStripMenuItem mExit = new ToolStripMenuItem("E&xit".Localize());

            // Add to Menu
            DropDownItems.Add(mNew);
            DropDownItems.Add(mOpen);
            DropDownItems.Add(mClose);
            DropDownItems.Add("-");
            DropDownItems.Add(mSave);
            DropDownItems.Add(mSaveAs);
            DropDownItems.Add("-");
            DropDownItems.Add(mExit);

            // Connect Event Handlers
            mClose.Click += mClose_Click;
            mSave.Click += mSave_Click;
            mSaveAs.Click += mSaveAs_Click;
            mExit.Click += mExit_Click;

            // Fill the New/Open menus based on documents
            if (documentTypes.Length == 1)
            {
                Documents.DocumentMeta meta = DocumentManager.GetInst().Meta.Get(documentTypes[0]);
                if (meta.NewFactoryData != null)
                {
                    mNew.Tag = documentTypes[0];
                    mNew.Click += (sender, args) =>
                    {
                        IDocument doc = meta.NewFactory.Invoke(null, null) as IDocument;
                        if (doc != null)
                            DocumentManager.GetInst().Activate(doc);
                    };
                }
                else
                    DropDownItems.Remove(mNew); // No NewDocumentFactory, application can only open files
                
                mOpen.Tag = documentTypes[0];
                mOpen.Click += (sender, args) =>
                {
                    OpenFileDialog dlg = new OpenFileDialog();
                    dlg.Filter = meta.Filter.Filter;
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        IDocument doc = meta.OpenFactory.Invoke(null, new object[] { dlg.FileName }) as IDocument;
                        if (doc != null)
                            DocumentManager.GetInst().Activate(doc);
                    }
                };
            }
            else
            {
                foreach (Type docType in documentTypes)
                {
                    Documents.DocumentMeta meta = DocumentManager.GetInst().Meta.Get(docType);

                    if (meta.NewFactory != null)
                    {
                        ToolStripMenuItem newSubItem = new ToolStripMenuItem(meta.Filter.Name);
                        newSubItem.Click += (sender, args) =>
                        {
                            IDocument doc = meta.NewFactory.Invoke(null, null) as IDocument;
                            if (doc != null)
                                DocumentManager.GetInst().Activate(doc);
                        };
                        mNew.DropDownItems.Add(newSubItem);
                    }

                    if (meta.OpenFactory != null)
                    {
                        ToolStripMenuItem openSubItem = new ToolStripMenuItem(meta.Filter.Name);
                        openSubItem.Click += (sender, args) =>
                        {
                            OpenFileDialog dlg = new OpenFileDialog();
                            dlg.Filter = meta.Filter.Filter;
                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                IDocument doc = meta.OpenFactory.Invoke(null, new object[] { dlg.FileName }) as IDocument;
                                if (doc != null)
                                    DocumentManager.GetInst().Activate(doc);
                            }
                        };
                        mOpen.DropDownItems.Add(openSubItem);
                    }
                }
            }
        }

        void mExit_Click(object sender, EventArgs e)
        {
            MainWindow.inst().Terminate();
        }

        void mSaveAs_Click(object sender, EventArgs e)
        {
            MainWindow.inst().SendCommand(ApplicationCmd.SaveAsCmd);
        }

        void mSave_Click(object sender, EventArgs e)
        {
            MainWindow.inst().SendCommand(ApplicationCmd.SaveCmd);
        }

        void mClose_Click(object sender, EventArgs e)
        {
            IDocument doc = DocumentManager.GetInst().ActiveDocument;
            if (doc != null)
                MainWindow.inst().DockingPanel.ActiveDocument.DockHandler.Close();
        }
    }
}
