using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorWinForms.Documents.Cooker
{
    public class CookerSettings : EditorCore.Settings.SettingsObject
    {
        [Category("Output")]
        [Description("Folder into which to write results")]
        public string OutputPath { get; set; }
    }
}
