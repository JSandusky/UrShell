using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorWinForms.Documents.Model
{
    public class ModelViewerSettings : EditorCore.Settings.SettingsObject
    {
        [Description("Rendering style")]
        public ViewportRenderMode RenderMode { get; set; }
    }
}
