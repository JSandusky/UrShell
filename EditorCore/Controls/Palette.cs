using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorWinForms.Controls
{
    /// <summary>
    /// Utility class for clustering objects into a palette to be used by other editor
    /// </summary>
    public class Palette : Sce.Atf.Controls.TreeControl
    {
        public Palette() : base(Sce.Atf.Controls.TreeControl.Style.CategorizedPalette)
        {
        }
    }
}
