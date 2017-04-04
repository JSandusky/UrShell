using EditorCore;
using EditorCore.Documents;
using Sce.Atf;
using Sce.Atf.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorWinForms.Documents.Model
{
    [DocumentFilter("Models (*.mdl)|*.mdl", "Model")]
    public class ModelDocument : IDocument, ICommandClient
    {
        ModelView view;

        [OpenDocumentFactory("Model", new string[] { ".mdl" })]
        public static ModelDocument FactoryMethod(string path)
        {
            if (System.IO.File.Exists(path))
                return new ModelDocument(path);
            return null;
        }

        private ModelDocument(string path)
        {
            uri_ = new Uri(path);
            view = new ModelView(this);
            view.Show(MainWindow.inst().DockingPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
        }

        public bool Dirty {
            get { return false; }
            set { /*do nothing*/ }
        }
        public event EventHandler DirtyChanged;

        public bool IsReadOnly { get { return true; } }

        public string Type { get { return "Model Viewer".Localize(); } }

        Uri uri_;
        public Uri Uri {
            get {
                return uri_;
            }
            set {
                if (uri_ != null && !uri_.Equals(value)) {
                    Uri old = uri_;
                    uri_ = value;
                    if (UriChanged != null)
                        UriChanged(this, new UriChangedEventArgs(old));
                }
            }
        }
        public event EventHandler<UriChangedEventArgs> UriChanged;

        public bool CanDoCommand(object commandTag)
        {
            if (commandTag.Equals("CMD_VIEW_WIREFRAME"))
                return true;
            else if (commandTag.Equals("CMD_VIEW_TEXTURED"))
                return true;
            return false;
        }

        public void DoCommand(object commandTag)
        {
            if (commandTag.Equals("CMD_VIEW_WIREFRAME"))
            {
                view.renderer.Execute("SetWireframe()");
            }
            else if (commandTag.Equals("CMD_VIEW_TEXTURED"))
            {
                view.renderer.Execute("SetTextured()");
            }
        }

        public void UpdateCommand(object commandTag, CommandState commandState)
        {
            
        }
    }
}
