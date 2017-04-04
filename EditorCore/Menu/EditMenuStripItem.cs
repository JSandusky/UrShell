using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sce.Atf;
using Sce.Atf.Applications;

namespace EditorCore.Menu
{
    /// <summary>
    /// Constructs and fill an "Edit" menu
    /// </summary>
    public class EditMenuStripItem : ToolStripMenuItem
    {
        public EditMenuStripItem() : base("&Edit".Localize())
        {
            Name = "MenuEdit";
            ToolStripMenuItem undo = new ToolStripMenuItem("Undo".Localize());
            ToolStripMenuItem redo = new ToolStripMenuItem("Redo".Localize());

            ToolStripMenuItem cut = new ToolStripMenuItem("Cut".Localize());
            ToolStripMenuItem copy = new ToolStripMenuItem("&Copy".Localize());
            ToolStripMenuItem paste = new ToolStripMenuItem("&Paste".Localize());

            ToolStripMenuItem settings = new ToolStripMenuItem("Settings".Localize());
            settings.Click += settings_Click;
            ToolStripMenuItem keybindings = new ToolStripMenuItem("Keybindings".Localize());
            keybindings.Click += keybindings_Click;

            DropDownItems.Add(undo);
            DropDownItems.Add(redo);
            DropDownItems.Add("-");
            DropDownItems.Add(cut);
            DropDownItems.Add(copy);
            DropDownItems.Add(paste);
            DropDownItems.Add("-");
            DropDownItems.Add(settings);
            DropDownItems.Add(keybindings);
        }

        void keybindings_Click(object sender, EventArgs e)
        {
            List<Sce.Atf.Controls.CustomizeKeyboardDialog.Shortcut> shortcuts = CommandRegistry.GetInst().GetShortcuts();
            Sce.Atf.Controls.CustomizeKeyboardDialog dlg = new Sce.Atf.Controls.CustomizeKeyboardDialog(shortcuts, new Dictionary<Sce.Atf.Input.Keys, string>());
            dlg.ShowDialog();
            foreach (Sce.Atf.Controls.CustomizeKeyboardDialog.Shortcut s in shortcuts)
            {
                s.Info.Shortcuts = s.Keys;
            }
        }

        void settings_Click(object sender, EventArgs e)
        {
            Dialogs.SettingsDlg.ShowDialog();
        }
    }
}
