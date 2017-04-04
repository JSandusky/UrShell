using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UrhoBackend
{
    public interface IParentedList : IList
    {
        object Parent { get; set; }
    }

    public class ParentedList<T> : List<T>, IParentedList
    {
        public object Parent { get; set; }

        public ParentedList(object parent)
        {
            Parent = parent;
        }
    }
}
