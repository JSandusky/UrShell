using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorWinForms.Documents.Material
{
    public enum PreviewShape
    {
        Sphere,
        Box,
        Plane,
        Cylinder,
        Ring
    }

    public class MaterialSettings : EditorCore.Settings.SettingsObject
    {
        [Description("Geometric shape to use for material preview")]
        public PreviewShape PreviewShape { get; set; }
    }
}
