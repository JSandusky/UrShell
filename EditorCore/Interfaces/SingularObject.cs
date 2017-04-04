using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sce.Atf;

namespace EditorCore.Interfaces
{
    /// <summary>
    /// Throws an exception if constructed more than once
    /// </summary>
    public class SingularObject
    {
        static List<Type> instances_ = new List<Type>();

        public SingularObject()
        {
            if (instances_.Contains(GetType()))
            {
                throw new Exception(String.Format("Only a single instance of {0} is permitted".Localize(), GetType().Name));
            }
            else
                instances_.Add(GetType());
        }
    }
}
