using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditorWinForms.Documents.SceneUI
{
    internal static class SceneGUI
    {
        public static List<ToolStripMenuItem> CreateMenus()
        {
            List<ToolStripMenuItem> ret = new List<ToolStripMenuItem>();

            ToolStripMenuItem createMenu = new EditorCore.Menu.DocumentMenuStripItem<SceneDocument>("&Create");
            ret.Add(createMenu);

            {
                createMenu.DropDownItems.Add(EditorCore.Menu.MenuUtil.CreateMenuItem(SceneUI.SceneCmd.CreateLocalNodeCmd));
                createMenu.DropDownItems.Add(EditorCore.Menu.MenuUtil.CreateMenuItem(SceneUI.SceneCmd.CreateReplicatedNodeCmd));

                ToolStripMenuItem builtins = new ToolStripMenuItem("Built In") { Name = "CreateBuiltIn" };
                builtins.DropDownItems.Add(EditorCore.Menu.MenuUtil.CreateMenuItem(SceneUI.SceneCmd.CreateBuiltInBox));
                builtins.DropDownItems.Add(EditorCore.Menu.MenuUtil.CreateMenuItem(SceneUI.SceneCmd.CreateBuiltInPlane));
                builtins.DropDownItems.Add(EditorCore.Menu.MenuUtil.CreateMenuItem(SceneUI.SceneCmd.CreateBuiltInSphere));
                builtins.DropDownItems.Add(EditorCore.Menu.MenuUtil.CreateMenuItem(SceneUI.SceneCmd.CreateBuiltInCone));
                createMenu.DropDownItems.Add(builtins);
            }

            return ret;
        }

        public static List<ToolStrip> CreateToolstrips()
        {
            List<ToolStrip> ret = new List<ToolStrip>();

            EditorCore.Toolbars.DocumentToolStrip selectionBar = new EditorCore.Toolbars.DocumentToolStrip("SceneSelectionTB", typeof(SceneDocument));

            EditorCore.Toolbars.DocumentToolStrip viewBar = new EditorCore.Toolbars.DocumentToolStrip("SceneViewTB", typeof(SceneDocument));

            return ret;
        }
    }
}
