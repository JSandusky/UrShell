using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorWinForms.Data
{
    public class ExternalTypeRegister
    {
        List<Type> restricted = new List<Type>();
        public ExternalTypeRegister()
        {
            restricted.Add(typeof(UrhoBackend.Vector2));
            restricted.Add(typeof(UrhoBackend.Vector3));
            restricted.Add(typeof(UrhoBackend.Vector4));
            restricted.Add(typeof(UrhoBackend.Quaternion));
            restricted.Add(typeof(UrhoBackend.IntVector2));
            restricted.Add(typeof(UrhoBackend.IntRect));
            restricted.Add(typeof(UrhoBackend.StringHash));
            restricted.Add(typeof(UrhoBackend.ResourceRef));
            restricted.Add(typeof(UrhoBackend.ResourceRefList));
            restricted.Add(typeof(UrhoBackend.VariantVector));
            restricted.Add(typeof(UrhoBackend.VariantMap));
        }

        public bool Restricted(Type t)
        {
            return restricted.Contains(t);
        }
    }
}
