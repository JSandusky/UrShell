using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EditorCore.Translators
{
    /// <summary>
    /// Used by hierarchial views to display a hierarchy of things
    /// </summary>
    public abstract class HierarchyTranslator
    {
        static Dictionary<Type, HierarchyTranslator> translators_ = new Dictionary<Type,HierarchyTranslator>();

        public static void AddTranslator(Type t, HierarchyTranslator ht)
        {
            translators_[t] = ht;
        }

        public static HierarchyTranslator GetTranslator(Type t)
        {
            if (translators_.ContainsKey(t))
                return translators_[t];
            return null;
        }

        public abstract object GetStatusTag(object forObject);          // Gets a 'tag' object to be used for storing state (ie. expanded/collpased)
        public abstract string GetLabel(object forObject, out FontStyle fontStyle);              // Get the display label
        public abstract int GetImage(object forObject);                 // Get the image index into the global list
        public abstract List<object> GetChildren(object forObject);     // Get a list of children
        public virtual bool CanDrag() { return true; }
    }
}
