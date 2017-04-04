using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EditorCore.Interfaces
{
    /// <summary>
    /// At program launch all initializers are executed in an arbitrary sequence
    /// Using this method is preferred for initializing singletons and similar concepts
    /// Eventually it is likely that the interface will change to include a load status progress
    /// Usage of this methodology will be important for that
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class IProgramInitializer : Attribute
    {
        public IProgramInitializer(string name) { Name = name; }
        public string Name { get; private set; }

        public static void Initialize()
        {
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    foreach (Type t in asm.GetTypes())
                    {
                        IProgramInitializer init = t.GetCustomAttribute<IProgramInitializer>();
                        if (init != null)
                        {
                            MethodInfo mi = t.GetMethod("ProgramInitialized", BindingFlags.Static | BindingFlags.NonPublic);
                            if (mi != null)
                                mi.Invoke(null, null);
                        }
                    }
                } catch (Exception)
                {
                    //eat this exception
                }
            }
        }
    }
}
