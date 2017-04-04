using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UrhoBackend
{
    // A function that should be called to trigger addition for a collection
    // Use a method signature of "void METHOD()"
    // mark as: [CollectionInsert("MethodName")]
    [AttributeUsage(AttributeTargets.Property)]
    public class CollectionInsert : Attribute
    {
        public CollectionInsert(string target) { Method = target; }
        public string Method { get; private set; }
    }

    // A function that should be called to trigger removal for a collection
    // Use a method signature of "void METHOD(int)"
    // mark as: [CollectionRemove("MethodName")]
    [AttributeUsage(AttributeTargets.Property)]
    public class CollectionRemove : Attribute
    {
        public CollectionRemove(string target) { Method = target; }
        public string Method { get; private set; }
    }
}
