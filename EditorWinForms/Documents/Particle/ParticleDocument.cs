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

namespace EditorWinForms.Documents.Particle
{
    [DocumentFilter("Particle Effects (*.xml)|*.xml", "Particle Effect")]
    public class ParticleDocument : IDocument, IPropertyDocument, ILoggingDocument, ICommandClient
    {
        List<string> logMessages = new List<string>();
        public UrhoBackend.ParticleEffect effect;
        public Particle.ParticleView view;

        [OpenDocumentFactory("Particle Effect", new string[] { ".xml" })]
        public static ParticleDocument FactoryMethod(string path)
        {
            if (System.IO.Path.GetExtension(path).Equals(".xml"))
            {
                // Verify it's a valid particle effect
                try
                {
                    return new ParticleDocument(path);
                }
                catch (Exception ex) {
                    ErrorHandler.GetInst().Error(ex);
                }
            }
            return null;
        }

        [NewDocumentFactory("Particle Effect", "Particle Effects (*.xml)|*.xml", new string[] { ".xml" })]
        public static ParticleDocument NewMethod()
        {
            return new ParticleDocument(null);
        }

        private ParticleDocument(string path)
        {
            if (path != null)
                uri_ = new Uri(path);
            view = new Particle.ParticleView(this, path);
            view.Show(MainWindow.inst().DockingPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            view.Urho3D.SubscribeCallback(this);
        }

        #region IPropertyBound
        public event EventHandler PropertyBoundChanged;
        public object PropertyBound
        {
            get { return effect; }
        }
        #endregion

        [UrhoBackend.HandleEvent(EventName = "LogMessage")]
        public void OnLogMessage(uint eventType, UrhoBackend.VariantMap eventData)
        {
            int level = eventData.Get("Level").GetInt();
            string msg = eventData.Get("Message").GetString();

            AddLogMessage(msg);
        }

        [UrhoBackend.HandleEvent(EventName = "ParticleData")]
        public void OnParticleDataSet(uint eventType, UrhoBackend.VariantMap eventData)
        {
            effect = new UrhoBackend.ParticleEffect(eventData.Get("Effect").GetPtr());
            effect.PropertyChanged += effect_PropertyChanged;
            if (PropertyBoundChanged != null)
                PropertyBoundChanged(this, new EventArgs { });
        }

        void effect_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            view.Urho3D.Execute("void UpdateEffect()");
        }

        #region ILoggingDocument
        public event LogMessageAdded LogAppended;

        public void AddLogMessage(string msg)
        {
            logMessages.Add(msg);
            if (LogAppended != null)
                LogAppended(this, new EventArgs());
        }

        public List<string> Log
        {
            get { return logMessages; }
        }
        #endregion

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
            get { return "Particle Effect".Localize(); }
        }

        Uri uri_;
        public Uri Uri
        {
            get { return uri_; }
            set
            {
                if (!uri_.Equals(value))
                {
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
            XmlDocument doc = effect.Save().ToDocument();
            if (needsPrompt)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Particle Effects (*.xml)|*.xml";
                dlg.Title = "Save Particle Effect".Localize();
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
