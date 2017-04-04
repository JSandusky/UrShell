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

namespace EditorWinForms.Documents.Model
{
    public partial class ModelView : EditorCore.Controls.DocumentDockContent
    {
        public EditorCore.RenderView.OrbitZoomRender renderer;
        public ModelView(IDocument doc) : base(doc)
        {
            InitializeComponent();
            renderer = new EditorCore.RenderView.OrbitZoomRender(doc, "Data/Scripts/ModelViewer.as");
            
            renderer.ExecuteMethod = "void StartForFile(String)";
            UrhoBackend.VariantVector createParams = new UrhoBackend.VariantVector();
            createParams.Add(new UrhoBackend.Variant(doc.Uri.LocalPath));
            renderer.ExecuteParams = createParams;
            renderer.Dock = DockStyle.Fill;

            Controls.Add(renderer);
        }
    }
}
