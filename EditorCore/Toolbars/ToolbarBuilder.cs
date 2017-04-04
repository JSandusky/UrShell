using Sce.Atf.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditorCore.Toolbars
{
    internal static class ToolbarBuilder
    {
        public static List<ToolStrip> BuildToolbars(List<CommandInfo> commands)
        {
            List<ToolStrip> ret = new List<ToolStrip>();
            IEnumerable<IGrouping<object, CommandInfo>> groups = commands.GroupBy(ci => ci.GroupTag);
            foreach (IGrouping<object, CommandInfo> group in groups)
            {
                ToolStrip strip = null;
                foreach (CommandInfo ci in group)
                {
                    if ((ci.Visibility & CommandVisibility.Toolbar) != 0)
                    {
                        if (strip == null)
                            strip = new ToolStrip { Name = group.Key.ToString() + "_TB", Text = group.Key.ToString() };
                        strip.Items.Add(Menu.MenuUtil.CreateButton(ci));
                    }
                }
                if (strip != null && strip.Items.Count > 0)
                    ret.Add(strip);
            }
            return ret;
        }
    }
}
