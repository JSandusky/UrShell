using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorCore
{
    public static class TypeUtils
    {
        // Get the distance between types
        public static int GetDistance(Type startType, Type destType)
        {
            if (!destType.IsAssignableFrom(startType))
                return -1;
            Type curType = startType;
            int ct = 0;
            while (curType != null && curType != destType)
            {
                ++ct;
                curType = curType.BaseType;
            }
            return ct;
        }
    }
}
