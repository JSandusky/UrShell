using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeifenLuo.WinFormsUI.Docking;

namespace EditorCore.Controls
{
    /// <summary>
    /// Base class for docking content that is never truly removed
    /// </summary>
    public abstract class PanelDockContent : DockContent
    {
        static List<PanelDockContent> content_ = new List<PanelDockContent>();

        internal static List<PanelDockContent> GetList() { return content_; }

        public PanelDockContent()
        {
            content_.Add(this);
            CloseButton = false;
            CloseButtonVisible = false;
        }
    }
}
