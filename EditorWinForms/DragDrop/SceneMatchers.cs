using EditorCore.DragDrop;
using Sce.Atf.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorWinForms.DragDrop
{
    public class NodeOntoNodeMatcher : TemplateDragMatch<UrhoBackend.Node, UrhoBackend.Node>
    {
        public override bool CanDoDrop(UrhoBackend.Node srcInst, UrhoBackend.Node destInst)
        {
            return !srcInst.EqualsOther(destInst) && !srcInst.GetParent().EqualsOther(destInst); //don't drop node onto itself or its parent
        }
        public override void DoDrop(UrhoBackend.Node srcInst, UrhoBackend.Node destInst, object srcHolder, object destHolder)
        {
            destInst.AddChild(srcInst);

        }
    }

    public class NodeOntoSceneMatcher : TemplateDragMatch<UrhoBackend.Node, UrhoBackend.Scene>
    {
        public override bool CanDoDrop(UrhoBackend.Node srcInst, UrhoBackend.Scene destInst)
        {
            return !srcInst.GetParent().EqualsOther(destInst); //dont drop node onto it's own scene
        }
        public override void DoDrop(UrhoBackend.Node srcInst, UrhoBackend.Scene destInst, object srcHolder, object destHolder)
        {
            destInst.AddChild(srcInst);
        }
    }
}
