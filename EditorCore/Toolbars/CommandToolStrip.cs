using Sce.Atf.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditorCore.Toolbars
{
    /// <summary>
    /// A ToolStrip that is automatically filled with a list of CommandInfo
    /// </summary>
    public class CommandToolStrip : ToolStrip
    {
        public CommandToolStrip(string name, List<CommandInfo> commands)
        {
            this.Name = name;
            Fill(this, commands);
        }

        public static void Fill(ToolStrip strip, List<CommandInfo> commands)
        {
            object lastGroup = null;
            foreach (CommandInfo ci in commands)
            {
                if ((ci.Visibility | CommandVisibility.Toolbar) == 0)
                    continue;
                if (lastGroup == null && lastGroup != ci.GroupTag)
                    strip.Items.Add("-");
                strip.Items.Add(Menu.MenuUtil.CreateButton(ci));
                lastGroup = ci.GroupTag;
            }
        }
    }
}
