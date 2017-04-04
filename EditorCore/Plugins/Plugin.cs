using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EditorCore.Plugins
{
    /// <summary>
    /// Contains summary information for displaying details about plugins.
    /// </summary>
    public sealed class PluginInfo
    {
        public string TypeName { get; set; }
        public string PluginTypeName { get; set; }
        public string Assembly { get; set; }
        public string Version { get; set; }
    }

    // Automatically collects and instantiates instances of types derived from T
    public abstract class Plugin<T> : IServiceProvider
    {
        List<T> instances_ = new List<T>();

        public Plugin()
        {
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    foreach (Type type in asm.GetTypes())
                    {
                        if (typeof(T).IsAssignableFrom(type) && !type.IsAbstract)
                        {
                            object o = Activator.CreateInstance(type);
                            if (o != null)
                                instances_.Add((T)o);
                        }
                    }
                }
                catch (Exception) 
                {
                    //eat it
                }
            }
        }

        public List<T> Instances { get { return instances_; } }

        public U Get<U>() where U : T
        {
            foreach (T o in instances_)
            {
                if (o.GetType() == typeof(U))
                    return (U)o;
            }
            return default(U);
        }

        public Type Type { get { return typeof(T); } }

        public List<PluginInfo> GetInfo()
        {
            List<PluginInfo> ret = new List<PluginInfo>();
            foreach (T plug in instances_)
            {
                ret.Add(new PluginInfo
                {
                    Assembly = plug.GetType().Assembly.FullName,
                    TypeName = plug.GetType().Name,
                    PluginTypeName = typeof(T).Name,
                    Version = plug.GetType().Assembly.GetName().Version.ToString()
                });
            }
            return ret;
        }

        public object GetService(Type serviceType)
        {
            foreach (T o in instances_)
            {
                if (o.GetType() == serviceType)
                    return o;
            }
            return null;
        }
    }
}
