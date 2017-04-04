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
using Sce.Atf;

namespace EditorWinForms.Documents.Material
{
    public partial class MaterialView : EditorCore.Controls.DocumentDockContent
    {
        UrhoBackend.Material material;
        public UrhoBackend.UrControl Urho3D;
        
        public MaterialView(IDocument doc, string file) : base(doc)
        {
            InitializeComponent();
            Urho3D = new EditorCore.RenderView.OrbitZoomRender(doc, "Data/Scripts/MaterialEditor.as");
            Urho3D.SubscribeCallback(this);
            Urho3D.Dock = DockStyle.Fill;
            if (file != null)
            {
                Urho3D.ExecuteMethod = "void StartForFile(String)";
                Urho3D.ExecuteParams = new UrhoBackend.VariantVector();
                Urho3D.ExecuteParams.Add(new UrhoBackend.Variant(file));
            }
            Controls.Add(Urho3D);
        }

        void material_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            
        }

        [UrhoBackend.HandleEvent(EventName = "MaterialLoaded")]
        public void OnMaterialLoaded(uint eventType, UrhoBackend.VariantMap eventData)
        {
            IntPtr matData = eventData.Get("Material").GetPtr();
            material = new UrhoBackend.Material(matData);
            ((MaterialDocument)Document).PropertyBound = material;
        }
    }
}
