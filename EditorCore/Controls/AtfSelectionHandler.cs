using Sce.Atf.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorCore.Controls
{
    public class AtfSelectionHandler : ISelectionContext
    {
        List<object> selected_ = new List<object>();

        public T GetLastSelected<T>() where T : class
        {
            if (LastSelected != null && LastSelected.GetType() == typeof(T))
                return (T)LastSelected;
            return default(T);
        }

        public IEnumerable<T> GetSelection<T>() where T : class
        {
            List<T> ret = new List<T>();
            foreach (object o in selected_)
                if (o.GetType() == typeof(T))
                    ret.Add((T)o);
            return ret;
        }

        public object LastSelected
        {
            get;
            set;
        }

        public IEnumerable<object> Selection
        {
            get
            {
                return selected_;
            }
            set
            {
                if (SelectionChanging != null)
                    SelectionChanging(this, new EventArgs { });
                selected_.Clear();
                foreach (object o in value)
                    selected_.Add(o);
                if (SelectionChanged != null)
                    SelectionChanged(this, null);
            }
        }

        public event EventHandler SelectionChanged;

        public event EventHandler SelectionChanging;

        protected void ResetEvents()
        {
            SelectionChanged = null;
            SelectionChanging = null;
        }

        public bool SelectionContains(object item)
        {
            return selected_.Contains(item);
        }

        public int SelectionCount
        {
            get { return selected_.Count; }
        }
    }
}
