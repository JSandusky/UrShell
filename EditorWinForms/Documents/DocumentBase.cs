using Sce.Atf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorWinForms.Documents
{
    public abstract class DocumentBase : EditorCore.Controls.AtfSelectionHandler, IDocument
    {
        public List<string> Notes
        {
            get { return null; }
            set { }
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

        public virtual string Type
        {
            get { return GetType().Name; }
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
                if (uri_ == null || !uri_.Equals(value))
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
    }
}
