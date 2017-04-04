using Sce.Atf.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditorCore.Menu
{
    /// <summary>
    /// Constructs a menu item from a a list of commands
    /// </summary>
    public class CommandMenuStripItem : ToolStripMenuItem
    {
        public CommandMenuStripItem(string title, List<CommandInfo> commands) : base(title)
        {
            Name = title;
            object lastGroup = null;
            foreach (CommandInfo ci in commands)
            {
                if ((ci.Visibility | CommandVisibility.Menu) == 0)
                    continue;
                if (lastGroup != null && ci.GroupTag != lastGroup)
                    DropDownItems.Add("-"); //Add a seperator when groups in the list change
                DropDownItems.Add(MenuUtil.CreateMenuItem(ci));
                lastGroup = ci.GroupTag;
            }
        }
    }
}
