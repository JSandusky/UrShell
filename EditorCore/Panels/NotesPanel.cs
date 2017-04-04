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
using EditorCore.Documents;
using Sce.Atf.Controls;
using EditorCore;
using Sce.Atf;

namespace EditorCore.Panels
{
    [PanelHint(WeifenLuo.WinFormsUI.Docking.DockState.DockLeftAutoHide, true)]
    class ItemView : Sce.Atf.Applications.IItemView
    {

        public void GetInfo(object item, Sce.Atf.Applications.ItemInfo info)
        {
            info.AllowLabelEdit = true;
            info.IsLeaf = true;
            info.Label = item as string;
            info.Description = item as string;
            info.AllowSelect = true;
        }
    }

    public partial class NotesPanel : Controls.PanelDockContent
    {
        List<string> cached = new List<string>();
        Sce.Atf.Controls.TreeControl treeList;

        public NotesPanel()
        {
            InitializeComponent();
            Text = "Notes".Localize();

            treeList = new Sce.Atf.Controls.TreeControl(TreeControl.Style.Tree);
            treeList.ShowRoot = false;
            treeList.Dock = DockStyle.Fill;
            treeList.LabelEditMode = TreeControl.LabelEditModes.EditOnClick;
            treeList.NodeLabelEdited += treeList_NodeLabelEdited;
            this.splitContainer1.Panel2.Controls.Add(treeList);

            DocumentManager.GetInst().DocumentChanged += NotesPanel_DocumentChanged;
        }

        void treeList_NodeLabelEdited(object sender, TreeControl.NodeEventArgs e)
        {
            int index = cached.IndexOf(e.Node.Tag as string);
            cached[index] = e.Node.Label;
            e.Node.Tag = e.Node.Label;
            e.Node.AllowLabelEdit = true;
            INotableDocument doc = DocumentManager.GetInst().ActiveDocument as INotableDocument;
            if (doc != null)
                doc.NoteUpdated(e.Node.Parent.Children.IndexOf(e.Node), e.Node.Label);
        }

        void NotesPanel_DocumentChanged(object sender, DocumentChangedEventArgs args)
        {
            INotableDocument doc = args.New as INotableDocument;
            if (doc != null)
            {
                treeList.Root.Clear();
                ICollection<string> notes = doc.Notes;
                if (notes == null)
                    return;
                foreach (string n in notes)
                {
                    TreeControl.Node node = treeList.Root.Add(n);
                    if (n.Length > 0)
                        node.Label = n;
                    else
                        node.Label = "<empty note>".Localize();
                    node.AllowLabelEdit = true;
                }
            }
            else
                treeList.Root.Clear();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            INotableDocument doc = DocumentManager.GetInst().ActiveDocument as INotableDocument;
            if (doc != null)
            {
                doc.Notes.Add("<empty note>".Localize());
                int sub = cached.Count;
                cached.Add("<empty note>");
                TreeControl.Node nd = treeList.Root.Add(sub);
                nd.Tag = "<empty note>";
                nd.Label = "<empty note>";
                nd.IsLeaf = true;
                nd.AllowLabelEdit = true;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }
    }
}
