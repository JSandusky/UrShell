using Sce.Atf.Applications;
using Sce.Atf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorWinForms.Documents.UI
{
    public static class UIEditorCmd
    {
        public static readonly CommandInfo GenerateCodeBehind = new CommandInfo("CMD_GEN_CODE_BEHIND",
            "UI",
            "UI",
            "Generate Code Behind".Localize(),
            "Generates Angelscript base code for binding the user interface".Localize());

        public static readonly CommandInfo GenerateUIAtlas = new CommandInfo("CMD_GEN_UI_ATLAS",
            "UI",
            "UI",
            "Generate UI Atlas".Localize(),
            "Generates a UI atlas and styles from the contents of a folder".Localize());
    }
}
