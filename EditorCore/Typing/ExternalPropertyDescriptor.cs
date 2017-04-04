using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EditorCore.Typing
{
    [Interfaces.IProgramInitializer("Type System")]
    public abstract class ExternalPropertyDescriptor
    {
        static List<ExternalPropertyDescriptor> descriptors_ = new List<ExternalPropertyDescriptor>();

        public static ExternalPropertyDescriptor GetDescriptor(Type type)
        {
            foreach (ExternalPropertyDescriptor desc in descriptors_)
            {
                if (desc.Handles(type))
                    return desc;
            }
            return null;
        }

        public abstract bool Handles(Type type);
        public abstract object GetValue(PropertyInfo pi, object from);
        public abstract void SetValue(PropertyInfo pi, object into, object value);
        public abstract void ResetValue(PropertyInfo pi, object into);
        public abstract TypeConverter GetConverter(Type type);
        public abstract object GetEditor(PropertyInfo pi);

        static void ProgramInitialized()
        {
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    foreach (Type t in asm.GetTypes())
                    {
                        if (!t.IsAbstract && typeof(ExternalPropertyDescriptor).IsAssignableFrom(t))
                            descriptors_.Add(Activator.CreateInstance(t) as ExternalPropertyDescriptor);
                    }
                }
                catch (Exception) { }
            }
        }
    }

    public abstract class BaseExternalPropertyDescriptor : ExternalPropertyDescriptor
    {
        public override object GetValue(PropertyInfo pi, object from)
        {
            return pi.GetValue(from);
        }
        public override void SetValue(PropertyInfo pi, object into, object value)
        {
            pi.SetValue(into, value);
        }
        public override void ResetValue(PropertyInfo pi, object into)
        {

        }
    }
}
