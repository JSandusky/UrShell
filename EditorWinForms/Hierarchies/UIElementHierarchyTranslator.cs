using EditorCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorWinForms.Hierarchies
{
    public class UIElementHierarchyTranslator : EditorCore.Translators.HierarchyTranslator
    {
        static NamedImageList imageList_;

        public override object GetStatusTag(object forObject)
        {
            return null; //Let the tree provide its own
        }

        public override string GetLabel(object forObject, out FontStyle fontStyle)
        {
            UrhoBackend.UIElement nd = ((UrhoBackend.UIElement)forObject);
            fontStyle = FontStyle.Regular;
            if (!String.IsNullOrWhiteSpace(nd.GetName()))
                return String.Format("{0} {1}", nd.GetTypeName(), nd.GetName());
            return String.Format("{0}", nd.GetTypeName());
        }

        public override int GetImage(object forObject)
        {
            return imageList_.IndexOf("images/uielement.png");
        }

        public override List<object> GetChildren(object forObject)
        {
            List<object> ret = new List<object>();
            foreach (UrhoBackend.UIElement comp in ((UrhoBackend.UIElement)forObject).GetChildren())
                ret.Add(comp);
            return ret;
        }
    }
}
