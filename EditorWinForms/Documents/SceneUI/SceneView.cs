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
using UrhoBackend;

namespace EditorWinForms.Documents.SceneUI
{
    public partial class SceneView : EditorCore.Controls.DocumentDockContent
    {
        public UrhoBackend.UrControl Urho3D;

        public SceneView(string path, IDocument document) : base(document)
        {
            InitializeComponent();

            Urho3D = new UrControl(path);
            Urho3D.Dock = DockStyle.Fill;
            Controls.Add(Urho3D);
            CloseButton = false;
            CloseButtonVisible = false;

            Text = "Engine View";
            if (path != null)
                Text = System.IO.Path.GetFileName(path);
        }

        ~SceneView()
        {
            Urho3D.Exit();
        }
    }
}
