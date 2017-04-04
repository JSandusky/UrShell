using EditorCore;
using EditorCore.Documents;
using Sce.Atf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EditorWinForms.Documents.ObjectAnim
{
    [DocumentFilter("Object Animations (*.xml)|*.xml", "Object Animation")]
    public class ObjectAnimationDocument : IDocument
    {
        public Timelines.ObjectAnimTimelineDocument TimelineDocument;
        ObjectAnim.ObjectAnimView view;

        [OpenDocumentFactory("Object Animation", new string[] { ".xml", ".as" })]
        public static ObjectAnimationDocument FactoryMethod(string path)
        {
            // Verify it's a valid object animation
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                if (!doc.DocumentElement.Name.Equals("objectanimation"))
                {
                    return null;
                }
            }
            catch (Exception ex) {
                ErrorHandler.GetInst().Error(ex);
            }
            return null;
        }

        [NewDocumentFactory("Object Animation", "Object Animations (*.xml)|*.xml", new string[] { ".xml"})]
        public static ObjectAnimationDocument NewDocMethod()
        {
            ObjectAnimationDocument document = new ObjectAnimationDocument(null);
            return document;
        }

        // From file document
        ObjectAnimationDocument(XmlDocument src, string path)
        {
            if (path != null)
            {
                uri_ = new Uri(path);
            }
        }

        //new ctor
        ObjectAnimationDocument(string path)
        {
            this.TimelineDocument = new Timelines.ObjectAnimTimelineDocument(null);
            
            view = new ObjectAnim.ObjectAnimView(path, TimelineDocument);
            view.Tag = new WeakReference<IDocument>(this);
            view.Show(MainWindow.inst().DockingPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
        }

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
            get { return "Object Animation".Localize(); }
        }

        Uri uri_;
        public Uri Uri
        {
            get
            {
                return uri_;
            }
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
    }
}
