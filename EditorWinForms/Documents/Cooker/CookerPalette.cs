using Sce.Atf.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditorWinForms.Documents.Cooker
{
    public class CookerPalette : Sce.Atf.Controls.TreeControl
    {
        public CookerPalette() : base(Style.CategorizedPalette)
        {
            AllowDrop = false;
            ShowRoot = false;

            // Actions
            {
                TreeControl.Node actions = Root.Add(null);
                actions.Label = "Actions";
                actions.HoverText = "Activities that are performed on files or folders";

                TreeControl.Node batch = actions.Add(typeof(CookBatch));
                batch.Label = "Batch Cmd";
                batch.HoverText = "Runs a .bat file";

                TreeControl.Node process = actions.Add(typeof(CookProcess));
                process.Label = "Run Exe";
                process.HoverText = "Runs an executable with arbitrary parameters";

                TreeControl.Node package = actions.Add(typeof(CookPackageBuilder));
                package.Label = "Package";
                package.HoverText = "Runs the Urho3D package compiler";
            }

            // Targets
            {
                TreeControl.Node targets = Root.Add(null);
                targets.Label = "Targets";

                TreeControl.Node cookItem = targets.Add(typeof(CookFileTarget));
                cookItem.Label = "File";
                cookItem.HoverText = "A single file target";

                TreeControl.Node folderItem = targets.Add(typeof(CookFolderTarget));
                folderItem.Label = "Folder";
                folderItem.HoverText = "All files in the specified folder will be processed by cooking";

                TreeControl.Node groupItem = targets.Add(typeof(CookGroupTarget));
                groupItem.Label = "Group";
                groupItem.HoverText = "Logically cluster cooking commands together";
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Sce.Atf.DragDropExtender extender = new Sce.Atf.DragDropExtender(this);
            {
                HitRecord hit = Pick(e.Location);
                if (hit.Node != null && hit.Node.Tag != null)
                {
                    extender.DoDragDrop(hit.Node, DragDropEffects.All, null, e.Location);
                }
            }
            base.OnMouseDown(e);
        }
    }

    public class CookerTree : Sce.Atf.Controls.TreeControl
    {
        CookerDocument document;
        public CookerTree(CookerDocument document)
        {
            ShowRoot = false;
            AllowDrop = true;
            this.document = document;
            DragDrop += CookerTree_DragDrop;
            DragOver += CookerTree_DragOver;
            DragEnter += CookerTree_DragEnter;
            SelectionChanged += CookerTree_SelectionChanged;
        }

        public void DeleteSelected()
        {
            while (SelectedNodes.Count() > 0)
            {
                // Such an incredibly obtuse interface
                Node nd = SelectedNodes.First();
                nd.Parent.Remove(nd.Tag);
            }
        }

        void CookerTree_SelectionChanged(object sender, EventArgs e)
        {
            List<object> sel = new List<object>();
            foreach (Node nd in this.SelectedNodes)
                sel.Add(nd.Tag);
            document.Selection = sel;
        }

        void CookerTree_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(Node)) == null)
                e.Effect = DragDropEffects.None;
            else
                e.Effect = DragDropEffects.All;
        }

        void CookerTree_DragOver(object sender, DragEventArgs e)
        {
            HitRecord hit = Pick(PointToClient(new Point(e.X, e.Y)));
            
            Node dest = hit.Node;
            Node data = e.Data.GetData(typeof(Node)) as Node;
            if (data == null)
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            if (dest == null && typeof(CookTask).IsAssignableFrom(((Type)data.Tag)))
                e.Effect = DragDropEffects.Move;
            else if (dest != null && ((ICookBase)dest.Tag).AcceptsChild((Type)data.Tag))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        void CookerTree_DragDrop(object sender, DragEventArgs e)
        {
            Node data = e.Data.GetData(typeof(Node)) as Node;
            if (data == null)
                return;
            Point p = this.PointToClient(new Point(e.X, e.Y));
            HitRecord hit = Pick(p);
            if (hit.Node != null)
            {
                Node dest = hit.Node;
                if (!(dest.Tag is CookTask))
                    return;
                dest.Expanded = true;
                ICookBase cb = Activator.CreateInstance((Type)data.Tag) as ICookBase;
                Node newNode = dest.Add(cb);
                newNode.Label = cb.GetName();
                cb.PropertyChanged += (src, args) => {
                    newNode.Label = cb.GetName();
                };

                {
                    ((CookTask)hit.Node.Tag).Targets.Add((CookTarget)cb);
                    newNode.IsLeaf = true;
                }
            }
            else
            {
                ICookBase cb = Activator.CreateInstance((Type)data.Tag) as ICookBase;
                Node newNode = Root.Add(cb);
                newNode.Label = cb.GetName();
                cb.PropertyChanged += (src, args) =>
                {
                    newNode.Label = cb.GetName();
                };
                document.CookingItems.Add(cb);
            }
        }
    }
}
