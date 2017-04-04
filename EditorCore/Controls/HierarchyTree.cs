using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sce.Atf.Controls;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace EditorCore.Controls
{
    /// <summary>
    /// Tree control that defers population and drag handling to other types
    /// HierarchyTranslators must exist to populate the tree for the root type given
    /// DragMatchers must exist for the contained types in order to execute drag and drop
    /// </summary>
    public class HierarchyTree : TreeControl
    {
        // A composite control using this control may manipulate this as desired
        public Dictionary<object, bool> expansionStatus = new Dictionary<object, bool>();

        public HierarchyTree()
        {
            ShowRoot = false;

            DragDrop += HierarchyTree_DragDrop;
            DragEnter += HierarchyTree_DragEnter;
            DragOver += HierarchyTree_DragOver;
        }

        void HierarchyTree_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
        {
            HitRecord hit = Pick(PointToClient(new Point(e.X, e.Y)));
            if (hit.Node != null)
            {
                Node dest = hit.Node;
                Node data = e.Data.GetData(typeof(Node)) as Node;
                if (dest == null || data == null)
                {
                    e.Effect = DragDropEffects.None;
                    return;
                }
                EditorCore.DragDrop.DragMatcher matcher = EditorCore.DragDrop.DragMatchCollection.GetMaster().GetBestFor(data.Tag.GetType(), dest.Tag.GetType());
                if (matcher != null && matcher.CanDoDrop(data.Tag, dest.Tag))
                    e.Effect = DragDropEffects.Move;
                else
                    e.Effect = DragDropEffects.None;
            }
        }

        void HierarchyTree_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (e.Data.GetData(typeof(Node)) == null)
                e.Effect = DragDropEffects.None;
            else
                e.Effect = DragDropEffects.All;
        }

        void HierarchyTree_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            Point p = this.PointToClient(new Point(e.X, e.Y));
            HitRecord hit = Pick(p);
            if (hit.Node != null)
            {
                Node dest = hit.Node;
                Node data = e.Data.GetData(typeof(Node)) as Node;
                EditorCore.DragDrop.DragMatcher matcher = EditorCore.DragDrop.DragMatchCollection.GetMaster().GetBestFor(data.Tag.GetType(), dest.Tag.GetType());
                if (matcher != null)
                {
                    if (matcher.CanDoDrop(data.Tag, dest.Tag))
                    {
                        matcher.DoDrop(data.Tag, dest.Tag, data, dest);
                        Node newNode = dest.Add(data.Tag);
                        data.Parent.Remove(data.Tag);
                        UpdateNode(newNode);
                    }
                }
            }
        }

        public void SetRootObject(object obj)
        {
            Root.Clear();
            if (obj == null)
                return;
            Fill(Root, obj);
        }

        void Fill(TreeControl.Node parentNode, object obj)
        {
            Translators.HierarchyTranslator trans = Translators.HierarchyTranslator.GetTranslator(obj.GetType());
            if (trans != null)
            {
                TreeControl.Node nd = parentNode.Add(obj);
                UpdateNode(nd);

                // Attach a property changed notifier
                if (nd.Tag is INotifyPropertyChanged)
                {
                    ((INotifyPropertyChanged)nd.Tag).PropertyChanged += (sender, args) =>
                    {
                        // Only ever concerned about updating our label
                        FontStyle fs = nd.FontStyle;
                        nd.Label = trans.GetLabel(nd.Tag, out fs);
                        nd.FontStyle = fs;
                    };
                }
            }
        }

        void UpdateNode(TreeControl.Node node)
        {
            Translators.HierarchyTranslator trans = Translators.HierarchyTranslator.GetTranslator(node.Tag.GetType());
            if (trans != null)
            {
                node.Clear();
                FontStyle fs = node.FontStyle;
                node.Label = trans.GetLabel(node.Tag, out fs);
                node.FontStyle = fs;
                node.ImageIndex = trans.GetImage(node.Tag);
                object statusTag = trans.GetStatusTag(node.Tag);
                node.Expanded = expansionStatus.ContainsKey(statusTag) && expansionStatus[statusTag];

                List<object> children = trans.GetChildren(node.Tag);
                if (children != null && children.Count > 0)
                {
                    node.IsLeaf = false;
                    foreach (object o in children)
                        Fill(node, o);
                }
                else
                    node.IsLeaf = true;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Sce.Atf.DragDropExtender extender = new Sce.Atf.DragDropExtender(this);
            {
                HitRecord hit = Pick(e.Location);
                if (hit.Node != null && hit.Node.Tag != null)
                {
                    Translators.HierarchyTranslator trans = Translators.HierarchyTranslator.GetTranslator(hit.Node.Tag.GetType());
                    if (trans != null && trans.CanDrag())
                        extender.DoDragDrop(hit.Node, DragDropEffects.All, null, e.Location);
                }
            }
            base.OnMouseDown(e);
        }

        public void SelectObject(object tag)
        {
            Select_(Root, tag);
        }

        public void Update(object tag)
        {
            Update_(Root, tag);
        }

        public void RemoveObject(object tag)
        {
            Remove_(Root, tag);
        }

        bool Select_(TreeControl.Node current, object tag)
        {
            foreach (TreeControl.Node nd in current.Children)
            {
                if (nd.Tag == tag)
                {
                    nd.Selected = true;
                    return true;
                }
                else if (Select_(nd, tag))
                    return true;
            }
            return false; // not found
        }

        bool Update_(TreeControl.Node current, object tag)
        {
            foreach (TreeControl.Node node in current.Children)
            {
                if (node.Tag == tag)
                {
                    UpdateNode(node);
                    return true;
                }
                else if (Update_(node, tag))
                    return true;
            }
            return false;
        }

        bool Remove_(TreeControl.Node current, object tag)
        {
            foreach (TreeControl.Node node in current.Children)
            {
                if (node.Tag == tag)
                {
                    current.Remove(tag);
                    return true;
                }
                else if (Remove_(node, tag))
                    return true;
            }
            return false;
        }
    }
}
