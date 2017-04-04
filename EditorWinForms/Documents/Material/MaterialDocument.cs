using EditorCore;
using EditorCore.Documents;
using EditorWinForms.Panels;
using Sce.Atf;
using Sce.Atf.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace EditorWinForms.Documents.Material
{
    [DocumentFilter("Materials (*.xml)|*.xml", "Material")]
    public class MaterialDocument : IDocument, IPropertyDocument, ILoggingDocument, ICommandClient
    {
        Material.MaterialView view;
        UrhoBackend.Material material;

        [OpenDocumentFactory("Material", new string[] { ".xml" })]
        public static MaterialDocument FactoryMethod(string path)
        {
            // Verify it's a valid material
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                if (doc.DocumentElement.Name.Equals("material"))
                    return new MaterialDocument(path);
            }
            catch (Exception ex) {
                ErrorHandler.GetInst().Error(ex);
            }
            return null;
        }

        [NewDocumentFactory("Material", "Materials (*.xml)|*.xml", new string[] { ".xml" })]
        public static MaterialDocument NewMethod()
        {
            return new MaterialDocument(null);
        }

        private MaterialDocument(string path)
        {
            if (path != null)
                uri_ = new Uri(path);
            view = new Material.MaterialView(this, path);
            view.Show(MainWindow.inst().DockingPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            view.Urho3D.SubscribeCallback(this);
        }

        public event EventHandler PropertyBoundChanged;
        public object PropertyBound
        {
            get { return material;  }
            set
            {
                if (material != value)
                {
                    material = value as UrhoBackend.Material;
                    if (PropertyBoundChanged != null)
                        PropertyBoundChanged(this, new EventArgs { });
                }
            }
        }

        [UrhoBackend.HandleEvent(EventName="LogMessage")]
        public void OnLogMessage(uint eventType, UrhoBackend.VariantMap eventData)
        {
            int level = eventData.Get("Level").GetInt();
            string msg = eventData.Get("Message").GetString();
            AddLogMessage(msg);
        }

        public event LogMessageAdded LogAppended;

        public void AddLogMessage(string msg)
        {
            log_.Add(msg);
            if (LogAppended != null)
                LogAppended(this, new EventArgs { });
        }
        List<string> log_ = new List<string>();
        public List<string> Log
        {
            get { return log_; }
        }

        #region IDocument
        bool dirty_;
        public bool Dirty
        {
            get
            {
                return dirty_;
            }
            set
            {
                if (dirty_ != value)
                {
                    dirty_ = value;
                    if (DirtyChanged != null)
                        DirtyChanged(this, new EventArgs { });
                }
            }
        }
        public event EventHandler DirtyChanged;

        public bool IsReadOnly
        {
            get { return false; }
        }

        public string Type
        {
            get { return "Material".Localize(); }
        }

        Uri uri_;
        public Uri Uri
        {
            get { return uri_; }
            set {
                if (!uri_.Equals(value)) {
                    Uri old = uri_;
                    uri_ = value;
                    if (UriChanged != null)
                        UriChanged(this, new UriChangedEventArgs(old));
                }
            }
        }
        public event EventHandler<UriChangedEventArgs> UriChanged;
        #endregion

        #region ICommandClient
        public bool CanDoCommand(object commandTag)
        {
            if (commandTag.ToString().Equals("SaveCmd"))
                return true;
            if (commandTag.ToString().Equals("SaveAsCmd"))
                return true;
            return false;
        }

        public void DoCommand(object commandTag)
        {
            if (commandTag.ToString().Equals("SaveCmd"))
            {
                DoSave(Uri == null);
            }
            else if (commandTag.ToString().Equals("SaveAsCmd"))
            {
                DoSave(true);
            }
        }

        void DoSave(bool needsPrompt)
        {
            XmlDocument doc = material.Save().ToDocument();
            if (needsPrompt)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Materials (*.xml)|*.xml";
                dlg.Title = "Save Material".Localize();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    doc.Save(dlg.FileName);
                    Uri = new Uri(dlg.FileName);
                    Dirty = false;
                }
            }
            else
            {
                doc.Save(Uri.LocalPath);
                Dirty = false;
            }
        }

        public void UpdateCommand(object commandTag, CommandState commandState) { }
        #endregion
    }
}
