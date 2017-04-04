using Sce.Atf.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorWinForms.Documents.SceneUI
{
    public class SceneTreeView : ITreeView, IItemView
    {
        UrhoBackend.Scene root_;

        SceneTreeView(UrhoBackend.Scene scene)
        {

        }

        public IEnumerable<object> GetChildren(object parent)
        {
            List<object> children = new List<object>();
            children.AddRange(root_.GetComponents());
            children.AddRange(root_.GetChildren());
            return children;
        }

        public object Root
        {
            get { return root_; }
        }


        public void GetInfo(object item, ItemInfo info)
        {
            if (item is UrhoBackend.Node)
            {
                if (item is UrhoBackend.Scene)
                {
                    UrhoBackend.Scene scene = ((UrhoBackend.Scene)item);
                    info.IsExpandedInView = true;
                    info.IsLeaf = false;
                    if (String.IsNullOrWhiteSpace(scene.GetName()))
                        info.Label = "Scene : " + scene.GetID();
                    else
                        info.Label = scene.GetName() + " : " + scene.GetID();
                    info.FontStyle = System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline;
                    info.AllowLabelEdit = true;
                } 
                else
                {
                    UrhoBackend.Node node = ((UrhoBackend.Node)item);
                    info.IsLeaf = node.GetChildren().Count > 0;
                    if (String.IsNullOrWhiteSpace(node.GetName()))
                        info.Label = "Node" + (node.IsNetworked() ? " : NET " : " : ") + node.GetID();
                    else
                        info.Label = node.GetName() + (node.IsNetworked() ? " : NET " : " : ") + node.GetID();
                    info.FontStyle = System.Drawing.FontStyle.Bold;
                    info.AllowLabelEdit = true;
                }
            }
            else if (item is UrhoBackend.Component)
            {
                UrhoBackend.Component comp = ((UrhoBackend.Component)item);
                info.IsLeaf = true;
                info.Label = comp.GetTypeName() + " : " + comp.GetID();
                info.AllowLabelEdit = false;
            }
        }
    }
}
