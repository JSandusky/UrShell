using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sce.Atf.Applications;

namespace EditorCore.Panels
{
    [PanelHint(WeifenLuo.WinFormsUI.Docking.DockState.DockLeft, true)]
    public partial class HierarchyPanel : Controls.PanelDockContent
    {
        Controls.HierarchyTree tree;
        public HierarchyPanel()
        {
            InitializeComponent();
            Text = "Hierarchy";
            DocumentManager.GetInst().DocumentChanged += HierarchyPanel_DocumentChanged;
            tree = new Controls.HierarchyTree();
            tree.Dock = DockStyle.Fill;
            tree.SelectionChanged += tree_SelectionChanged;
            Controls.Add(tree);
        }

        void tree_SelectionChanged(object sender, EventArgs e)
        {
            ISelectionContext context = DocumentManager.GetInst().ActiveDocument as ISelectionContext;
            if (context != null)
            {
                List<object> selection = new List<object>();
                foreach (Sce.Atf.Controls.TreeControl.Node nd in tree.SelectedNodes)
                    selection.Add(nd.Tag);
                context.Selection = selection;
            }
        }

        void HierarchyPanel_DocumentChanged(object sender, DocumentChangedEventArgs args)
        {
            if (args.New is EditorCore.Documents.IHierarchialDocument)
            {
                tree.SetRootObject(((EditorCore.Documents.IHierarchialDocument)args.New).TreeRoot);
                ((EditorCore.Documents.IHierarchialDocument)args.New).TreeRootChanged += HierarchyPanel_TreeRootChanged;
            }
            else
                tree.SetRootObject(null);
        }

        void HierarchyPanel_TreeRootChanged(object sender, EventArgs e)
        {
            if (sender is EditorCore.Documents.IHierarchialDocument)
            {
                tree.SetRootObject(((EditorCore.Documents.IHierarchialDocument)sender).TreeRoot);
            }
        }
    }
}
