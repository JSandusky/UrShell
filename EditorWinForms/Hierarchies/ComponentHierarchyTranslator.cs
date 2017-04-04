using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorWinForms.Hierarchies
{
    [EditorCore.Interfaces.IProgramInitializer("Component translation")]
    public class ComponentHierarchyTranslator : EditorCore.Translators.HierarchyTranslator
    {
        static void ProgramInitialized()
        {
            EditorCore.Translators.HierarchyTranslator.AddTranslator(typeof(UrhoBackend.Component), new ComponentHierarchyTranslator());
        }

        public override object GetStatusTag(object forObject)
        {
            return String.Format("COMPONENT:{0}", ((UrhoBackend.Component)forObject).GetID());
        }

        public override string GetLabel(object forObject, out FontStyle fontStyle)
        {
            UrhoBackend.Component nd = ((UrhoBackend.Component)forObject);
            string netType = "";
            if (nd.GetID() >= 0x1000000)
                netType = "Local ";
            fontStyle = FontStyle.Regular;
            return String.Format("{0} ({1}{2})", nd.GetTypeName(), netType, nd.GetID());
        }

        public override int GetImage(object forObject)
        {
            return -1;
        }

        public override List<object> GetChildren(object forObject)
        {
            // Components do not have real children
            List<object> ret = new List<object>();
            return ret;
        }
    }
}
