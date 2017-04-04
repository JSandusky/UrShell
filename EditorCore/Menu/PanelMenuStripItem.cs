using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sce.Atf;

namespace EditorCore.Menu
{
    // Manages menus for active/inactive panels
    public class PanelMenuStripItem : ToolStripMenuItem
    {
        public PanelMenuStripItem()
            : base("&Windows".Localize())
        {
            Name = "MenuWindows";
            foreach (Controls.PanelDockContent panel in Controls.PanelDockContent.GetList())
            {
                ToolStripMenuItem pnl = new ToolStripMenuItem(panel.Text);
                pnl.CheckOnClick = true;
                pnl.Checked = !panel.IsHidden;
                panel.VisibleChanged += (sender, args) => { if (pnl.Checked != !panel.IsHidden) pnl.Checked = !panel.IsHidden; };
                pnl.CheckedChanged += (sender, args) => { if (!panel.IsHidden != pnl.Checked) panel.IsHidden = !pnl.Checked; };
                DropDownItems.Add(pnl);
            }

            ToolStripMenuItem toolbars = new ToolStripMenuItem("Toolbars".Localize());
            DropDownItems.Add(toolbars);
            foreach (ToolStrip st in MainWindow.inst().Toolbars)
            {
                ToolStripMenuItem ti = new ToolStripMenuItem(st.Name);
                ti.Text = st.Text;
                ti.CheckOnClick = true;
                ti.Checked = st.Visible;
                ti.CheckedChanged += (sender, args) => { if (st.Visible != ti.Checked) st.Visible = ti.Checked; };
                st.VisibleChanged += (sender, args) => { if (st.Visible != ti.Checked) ti.Checked = st.Visible; };
                toolbars.DropDownItems.Add(ti);
            }
        }
    }
}
