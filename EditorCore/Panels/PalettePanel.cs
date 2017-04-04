using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sce.Atf;

namespace EditorCore.Panels
{
    [PanelHint(WeifenLuo.WinFormsUI.Docking.DockState.DockLeft, true)]
    public partial class PalettePanel : Controls.PanelDockContent
    {
        public PalettePanel()
        {
            InitializeComponent();
            Text = "Palette".Localize();
        }
    }
}
