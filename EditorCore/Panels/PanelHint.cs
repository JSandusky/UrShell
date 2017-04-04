using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeifenLuo.WinFormsUI.Docking;
using System.Reflection;

namespace EditorCore.Panels
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PanelHint : Attribute
    {
        public PanelHint(DockState dockState, bool persist) { DockState = dockState; Persist = persist; }

        public DockState DockState { get; set; }
        public bool Persist { get; set; }
    }

    public static class PanelUtil
    {
        public static void Show(DockContent content, DockPanel panel)
        {
            PanelHint hint = content.GetType().GetCustomAttribute<PanelHint>();
            if (hint != null)
                content.Show(panel, hint.DockState);
            else
                content.Show(panel, WeifenLuo.WinFormsUI.Docking.DockState.DockLeft);
        }
    }
}
