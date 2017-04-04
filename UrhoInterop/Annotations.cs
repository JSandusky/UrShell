using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Urho {

    [AttributeUsage(AttributeTargets.Property)]
    public class DefaultValue : System.Attribute {
        public Object Value { get; set; }

        public DefaultValue(object obj) {
            Value = obj;
        }

        public static bool IsDefault(object obj) {
            foreach (PropertyInfo pi in obj.GetType().GetProperties()) {
                DefaultValue val = pi.GetCustomAttribute<DefaultValue>();
                if (val != null) {
                    if (!val.Value.Equals(pi.GetValue(obj)))
                        return false;
                }
            }
            return true;
        }

        public static bool IsDefault(object obj, string property) {
            foreach (PropertyInfo pi in obj.GetType().GetProperties()) {
                if (pi.Name.Equals(property)) {
                    DefaultValue val = pi.GetCustomAttribute<DefaultValue>();
                    if (val != null) {
                        if (!val.Value.Equals(pi.GetValue(obj)))
                            return false;
                    } else {
                        object objother = Activator.CreateInstance(pi.PropertyType);
                        if (!objother.Equals(pi.GetValue(obj)))
                            return false;
                    }
                }
            }
            return true;
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public class Resource : System.Attribute {

        public Type Type { get; set; }

        public Resource(Type aType) {
            Type = aType;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class ResourceType : System.Attribute
    {
        public ResourceType(string[] tn) { Typenames = tn; }
        public string[] Typenames { get; set; }
    }
}
