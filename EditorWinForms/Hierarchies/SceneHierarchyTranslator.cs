using EditorCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorWinForms.Hierarchies
{
    [EditorCore.Interfaces.IProgramInitializer("Scene hierarchy translation")]
    public class SceneHierarchyTranslator : NodeHierarchyTranslator
    {
        static void ProgramInitialized()
        {
            EditorCore.Translators.HierarchyTranslator.AddTranslator(typeof(UrhoBackend.Scene), new SceneHierarchyTranslator());
        }

        static NamedImageList imageList_;
        public override object GetStatusTag(object forObject)
        {
            return String.Format("SCENE:{0}", ((UrhoBackend.Scene)forObject).GetID());
        }

        public override string GetLabel(object forObject, out FontStyle fontStyle)
        {
            fontStyle = FontStyle.Bold | FontStyle.Underline;
            UrhoBackend.Scene nd = ((UrhoBackend.Scene)forObject);
            string name = "Scene";
            if (!String.IsNullOrWhiteSpace(nd.GetName()))
                name = nd.GetName();
            return String.Format("{0} ({1})", name, nd.GetID());
        }

        // Scene root is not draggable
        public override bool CanDrag()
        {
            return false;
        }

        public override int GetImage(object forObject)
        {
            if (imageList_ != null)
                return imageList_.IndexOf("images/scene.png");
            return -1;
        }
    }
}
