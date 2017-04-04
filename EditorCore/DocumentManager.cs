using Sce.Atf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorCore
{
    public class DocumentChangedEventArgs : EventArgs
    {
        public IDocument Previous { get; set; }
        public IDocument New { get; set; }
    }

    public delegate void DocumentChangedEvent(object sender, DocumentChangedEventArgs args);

    public sealed class DocumentManager
    {
        static DocumentManager inst_;

        List<IDocument> documents_ = new List<IDocument>();
        IDocument current_;
        Documents.DocumentMetaCollection meta_;

        public static DocumentManager GetInst()
        {
            return inst_;
        }

        public static void Init(params Type[] types)
        {
            inst_ = new DocumentManager();
            inst_.meta_ = new Documents.DocumentMetaCollection(types);
        }

        public Documents.DocumentMetaCollection Meta { get { return meta_; } }

        public event DocumentChangedEvent DocumentChanged;

        public void Activate(IDocument document)
        {
            if (!documents_.Contains(document) && document != null)
                documents_.Add(document);
            if (current_ != document)
            {
                DocumentChangedEventArgs args = new DocumentChangedEventArgs();
                args.Previous = current_;
                args.New = document;
                current_ = document;
                if (DocumentChanged != null)
                    DocumentChanged(this, args);
            }
        }

        public void Remove(IDocument document)
        {
            int index = documents_.IndexOf(document);
            documents_.Remove(document);
            if (documents_.Count > 0)
            {
                if (documents_.Count > index)
                    Activate(documents_[index]);
                else
                    Activate(documents_[0]);
            }
            else
                Activate(null);
        }

        public List<T> GetList<T>(Type type)
        {
            List<T> ret = new List<T>();
            foreach (IDocument doc in documents_)
            {
                if (doc.GetType() == type && typeof(T).IsAssignableFrom(doc.GetType()))
                    ret.Add((T)doc);
            }
            return ret;
        }

        public IDocument ActiveDocument { get { return current_; } }
    }
}
