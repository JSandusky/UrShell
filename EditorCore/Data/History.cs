using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorCore.Data
{
    public class History<T>
    {
        Stack<T> past_ = new Stack<T>();
        Stack<T> future_ = new Stack<T>();
        T current_;

        public void SetCurrent(T value)
        {
            if (current_ != null)
                past_.Push(current_);
            current_ = value;
            future_.Clear();
        }

        public void Add(T value)
        {
            past_.Push(value);
            future_.Clear();
        }

        public bool HasPast { get { return past_.Count > 1;  } }
        public bool HasFuture { get { return future_.Count > 0; } }

        public T Past()
        {
            T ret = past_.Pop();
            future_.Push(ret);
            return past_.Peek();
        }

        public T Future()
        {
            T ret = future_.Pop();
            past_.Push(ret);
            return ret;
        }
    }
}
