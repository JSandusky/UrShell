using Sce.Atf.Controls.PropertyEditing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorWinForms.Documents.SceneUI
{
    public class SceneSettings : EditorCore.Settings.SettingsObject
    {
        public SceneSettings()
        {
            RenderModes = new ViewportRenderMode[4];
        }

        [Category("Snap")]
        [Description("Units by which to snap translation")]
        public float SnapSize { get; set; } // Number of units to snap movement
        [Category("Snap")]
        [Description("Number of degrees by which to snap rotation")]
        public float RotateSnap { get; set; } // Number of degrees to snap rotation
        [Category("Snap")]
        [Editor(typeof(Sce.Atf.Controls.PropertyEditing.BoolEditor), typeof(IPropertyEditor))]
        [Description("Whether translation is snapped or not")]
        public bool SnapMove { get; set; }
        [Category("Snap")]
        [Editor(typeof(Sce.Atf.Controls.PropertyEditing.BoolEditor), typeof(IPropertyEditor))]
        [Description("Whether rotations are snapped or not")]
        public bool SnapRotate { get; set; }

        [Category("Viewport")]
        [Browsable(false)]
        [Description("Rendering style")]
        public ViewportRenderMode RenderMode { get; set; } // Wireframe, Solid, Textured
        
        [Category("Viewport")]
        public ViewportStyle ViewportMode { get; set; } // Single, Double Vert, Double Hor, Triple Left, Triple Top, Triple Right, Triple Bottom, Quad
        [Category("Viewport")]
        [Browsable(false)]
        public ViewportRenderMode[] RenderModes { get; set; }

        // Positions of viewport sashes
        [Category("Viewport")]
        [Browsable(false)]
        public float HorizontalSashPos { get; set; }
        [Category("Viewport")]
        [Browsable(false)]
        public float VerticalSashPos { get; set; }
    }
}
