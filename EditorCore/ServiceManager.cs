using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EditorCore
{
    public interface IService
    {

    }

    [Interfaces.IProgramInitializer("Service Manager")]
    public sealed class ServiceManager
    {
        static ServiceManager inst_;

        public static ServiceManager GetInst()
        {
            return inst_;
        }

        static void ProgramInitialized()
        {
            inst_ = new ServiceManager();

            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    foreach (Type type in asm.GetTypes())
                    {
                        
                    }
                }
                catch (Exception) { }
            }
        }
    }
}
