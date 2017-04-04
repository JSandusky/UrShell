using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Reflection;
using Sce.Atf.Applications;
using Sce.Atf;

namespace EditorWinForms.Documents.Particle
{
    public partial class ParticleView : EditorCore.Controls.DocumentDockContent, ICommandClient
    {
        public UrhoBackend.UrControl Urho3D;
        public ParticleView(IDocument doc, string file) : base(doc)
        {
            InitializeComponent();
            Urho3D = new EditorCore.RenderView.OrbitZoomRender(doc, "Data/Scripts/ParticleEditor.as");
            Urho3D.Dock = DockStyle.Fill;
            if (file != null)
            {
                Urho3D.ExecuteMethod = "void StartForFile(String)";
                Urho3D.ExecuteParams = new UrhoBackend.VariantVector();
                Urho3D.ExecuteParams.Add(new UrhoBackend.Variant(file));
            }
            Controls.Add(Urho3D);
        }

        public bool CanDoCommand(object commandTag)
        {
            return false;
        }

        public void DoCommand(object commandTag)
        {
            
        }

        public void UpdateCommand(object commandTag, CommandState commandState)
        {
            
        }
    }
}
