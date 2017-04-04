using Sce.Atf.Controls.PropertyEditing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorCore.Settings
{
    public enum SkinStyle
    {
        Default,
        Medium,
        Dark,
        Night
    }

    public class CoreSettings : SettingsObject
    {
        [DisplayName("Disable DPI Scaling")]
        [Description("Disable Windows scaling on high DPI displays")]
        [Editor(typeof(Sce.Atf.Controls.PropertyEditing.BoolEditor), typeof(IPropertyEditor))]
        public bool DisableDPIScaling { get; set; }

        [DisplayName("Autosave Every N Minutes")]
        [Description("Files will be periodically autosaved with a *.bak added extension")]
        public int AutoSaveTimer { get; set; }

        [DisplayName("Don't Show Errors")]
        [Description("Errors will be logged silently")]
        [Editor(typeof(Sce.Atf.Controls.PropertyEditing.BoolEditor), typeof(IPropertyEditor))]
        public bool DontShowErrors { get; set; }

        [Description("Changes to UI layouts will not be saved")]
        [Editor(typeof(Sce.Atf.Controls.PropertyEditing.BoolEditor), typeof(IPropertyEditor))]
        public bool DoNotSaveLayout { get; set; }

        [Description("UI display theme")]
        public SkinStyle SkinStyle { get; set; }

        [DisplayName("Project Directory")]
        [Description("Path to the root of the Urho3D project")]
        [Editor(typeof(System.Windows.Forms.Design.FolderNameEditor), typeof(UITypeEditor))]
        public string ProjectDirectory { get; set; }
    }
}
