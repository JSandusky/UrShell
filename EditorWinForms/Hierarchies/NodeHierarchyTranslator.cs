using EditorCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorWinForms.Hierarchies
{
    [EditorCore.Interfaces.IProgramInitializer("Node hierarchy translation")]
    public class NodeHierarchyTranslator : EditorCore.Translators.HierarchyTranslator
    {
        static void ProgramInitialized()
        {
            EditorCore.Translators.HierarchyTranslator.AddTranslator(typeof(UrhoBackend.Node), new NodeHierarchyTranslator());
        }

        static NamedImageList imageList_;
        public override object GetStatusTag(object forObject)
        {
            return String.Format("NODE:{0}", ((UrhoBackend.Node)forObject).GetID());
        }

        public override string GetLabel(object forObject, out FontStyle fontStyle)
        {
            fontStyle = FontStyle.Bold;
            UrhoBackend.Node nd = ((UrhoBackend.Node)forObject);
            string netType = "";
            if (nd.GetID() >= 0x1000000)
                netType = "Local ";
            string name = "Node";
            if (!String.IsNullOrWhiteSpace(nd.GetName()))
                name = nd.GetName();
            return String.Format("{0} ({1}{2})", name, netType, nd.GetID());
        }

        public override int GetImage(object forObject)
        {
            if (imageList_ != null)
                return imageList_.IndexOf("images/node.png");
            return -1;
        }

        public override List<object> GetChildren(object forObject)
        {
            List<object> ret = new List<object>();
            foreach (UrhoBackend.Component comp in ((UrhoBackend.Node)forObject).GetComponents())
                ret.Add(comp);
            foreach (UrhoBackend.Node nd in ((UrhoBackend.Node)forObject).GetChildren())
                ret.Add(nd);
            return ret;
        }
    }
}
