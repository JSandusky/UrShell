using Sce.Atf.Applications;
using Sce.Atf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorWinForms.Documents
{
    public static class SpaceCmd
    {
        public static readonly CommandInfo TranslateCmd = new CommandInfo("CMD_TRANSLATE",
            "Space",
            "Space",
            "Translate".Localize(),
            "Translates the selection".Localize())
            {
                Visibility = CommandVisibility.Toolbar,
                ImageName = "Resources/images/TranslateManip32.png"
            };

        public static readonly CommandInfo RotateCmd = new CommandInfo("CMD_ROTATE",
            "Space",
            "Space",
            "Rotate".Localize(),
            "Rotates the selection".Localize())
            {
                Visibility = CommandVisibility.Toolbar,
                ImageName = "Resources/images/RotateManip32.png"
            };

        public static readonly CommandInfo ScaleCmd = new CommandInfo("CMD_SCALE",
            "Space",
            "Space",
            "Scale".Localize(),
            "Scales the selection".Localize())
            {
                Visibility = CommandVisibility.Toolbar,
                ImageName = "Resources/images/ScaleManip32.png"
            };

        public static readonly CommandInfo PickerCmd = new CommandInfo("CMD_SELECT",
            "Space",
            "Space",
            "Select".Localize(),
            "Selects nodes".Localize())
            {
                Visibility = CommandVisibility.Toolbar,
                ImageName = "Resources/images/Selection32.png"
            };


// VIEWS
        public static readonly CommandInfo WireframeCmd = new CommandInfo("CMD_VIEW_WIREFRAME",
            "View",
            "View",
            "Wireframe".Localize(),
            "Toggles wireframe rendering".Localize())
            {
                Visibility = CommandVisibility.Toolbar,
                ImageName = "Resources/images/Wireframe32.png"
            };

        public static readonly CommandInfo TexturedCmd = new CommandInfo("CMD_VIEW_TEXTURED",
            "View",
            "View",
            "Textured".Localize(),
            "Toggles textured rendering".Localize())
            {
                Visibility = CommandVisibility.Toolbar,
                ImageName = "Resources/images/SmoothShading32.png"
            };

        public static readonly CommandInfo BookmarkView = new CommandInfo("CMD_BOOKMARK_VIEW",
            "View",
            "View",
            "Bookmark".Localize(),
            "Places a bookmark for the current view".Localize())
            {
                Visibility = CommandVisibility.Toolbar
            };
    }
}
