using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EditorCore
{
    /// <summary>
    /// Utility functions for using reflection with less verbosity
    /// </summary>
    public static class Reflect
    {
        public static T GetField<T>(object from, string name)
        {
            FieldInfo fi = from.GetType().GetField(name, BindingFlags.Public | BindingFlags.NonPublic);
            if (fi != null)
                return (T)fi.GetValue(from);
            return default(T);
        }

        public static bool SetField(object from, string name, object value)
        {
            FieldInfo fi = from.GetType().GetField(name, BindingFlags.Public | BindingFlags.NonPublic);
            if (fi != null)
            {
                fi.SetValue(from, value);
                return true;
            }
            return false;
        }

        public static T GetProperty<T>(object from, string name)
        {
            PropertyInfo fi = from.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.NonPublic);
            if (fi != null)
                return (T)fi.GetValue(from);
            return default(T);
        }

        public static bool SetProperty(object from, string name, object value)
        {
            PropertyInfo fi = from.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.NonPublic);
            if (fi != null)
            {
                fi.SetValue(from, value);
                return true;
            }
            return false;
        }

        public static bool HasMethod(object from, string name)
        {
            return from.GetType().GetMethod(name, BindingFlags.Public | BindingFlags.NonPublic) != null;
        }

        public static object Invoke(object on, string name, params object[] args)
        {
            MethodInfo mi = on.GetType().GetMethod(name, BindingFlags.Public | BindingFlags.NonPublic);
            if (mi != null)
            {
                return mi.Invoke(on, args);
            }
            return null;
        }
    }
}
