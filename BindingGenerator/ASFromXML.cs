using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BindingGenerator
{
    public static class ASFromXML
    {
        static readonly string[] ExplicitTypes = new string[]
        {
            "Node",
            "Scene",
            "Component",
            "Serializable",
            "Vector2",
            "Vector3",
            "Vector4",
            "IntVector2",
            "IntRect",
            "Rect",
            "Color",
            "Quaternion",
            "StringHash",
            "String",
            "ResourceRef",
            "ResourceRefList",
            "Variant",
            "VariantMap",
            "Matrix3",
            "Matrix3x4",
            "Matrix4",
            "AttributeInfo"
        };

        public static void Process(string outputDir, string headersPath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Scripting.xml");

            #region LOAD_CLASSES
            List<ClassInfo> classes = new List<ClassInfo>();
            XmlElement types = doc.GetElementsByTagName("Types").Item(0) as XmlElement;
            foreach (XmlElement type in types.GetElementsByTagName("type"))
            {
                ClassInfo clazz = new ClassInfo(type);
                classes.Add(clazz);

                foreach (XmlElement behavior in type.GetElementsByTagName("behavior"))
                {
                    
                }

                List<MethodInfo> tempProperties = new List<MethodInfo>();
                foreach (XmlElement method in type.GetElementsByTagName("method"))
                {
                    MethodInfo mi = new MethodInfo(method);
                    if (mi.CMethodName != null)
                    {
                        if (mi.Declaration.Contains(" get_") || mi.Declaration.Contains(" set_"))
                            tempProperties.Add(mi);
                        else
                            clazz.Methods.Add(mi);
                    }
                }

                List<MethodInfo> ignore = new List<MethodInfo>();
                for (int i = 0; i < tempProperties.Count; ++i)
                {
                    MethodInfo left = tempProperties[i];
                    if (!left.IsProperty)
                        continue;
                    if (ignore.Contains(left))
                        continue;
                    bool found = false;
                    for (int x = 0; x < tempProperties.Count; ++x)
                    {
                        if (i == x)
                            continue;
                        MethodInfo right = tempProperties[x];
                        if (!right.IsProperty)
                            continue;
                        if (ignore.Contains(right))
                            continue;
                        if (left.IsPropertyCompliment(right))
                        {
                            tempProperties.RemoveAt(i);
                            --i;
                            found = true;
                            ignore.Add(left);
                            ignore.Add(right);
                            clazz.Properties.Add(new PropertyPair
                            {
                                Setter = left.IsSetter ? left : right,
                                Getter = left.IsSetter ? right : left
                            });
                            break;
                        }
                    }
                    if (!found)
                    {
                        tempProperties.RemoveAt(i);
                        --i;
                        ignore.Add(left);
                        clazz.Properties.Add(new PropertyPair
                        {
                            Setter = left.IsSetter ? left : null,
                            Getter = !left.IsSetter ? left : null
                        });
                    }
                }

                foreach (XmlElement field in type.GetElementsByTagName("field"))
                {
                    FieldInfo fi = new FieldInfo { Declaration = field.GetAttribute("declaration").Replace("const ", "") };
                    clazz.Fields.Add(fi);
                }
            }
            #endregion

            #region TRIM_CLASS
            foreach (ClassInfo clazz in classes)
            {
                if (clazz.BaseClass != null && clazz.BaseClass.Length > 0)
                {
                    ClassInfo baseClass = classes.FirstOrDefault(o => o.Name.Equals(clazz.BaseClass));
                    if (baseClass == null)
                        continue;
                    do
                    {
                        for (int i = 0; i < clazz.Methods.Count; ++i)
                        {
                            MethodInfo mi = clazz.Methods[i];
                            if (baseClass.Methods.FirstOrDefault(m => m.CMethodName.Equals(mi.CMethodName)) != null)
                            {
                                clazz.Methods.RemoveAt(i);
                                --i;
                            }
                        }

                        for (int i = 0; i < clazz.Properties.Count; ++i)
                        {
                            PropertyPair pi = clazz.Properties[i];
                            if (pi.Setter != null)
                            {
                                if (baseClass.Properties.FirstOrDefault(m => m.Setter != null && m.Setter.CMethodName.Equals(pi.Setter.CMethodName)) != null)
                                {
                                    pi.Setter = null;
                                }
                            }
                            if (pi.Getter != null)
                            {
                                if (baseClass.Properties.FirstOrDefault(m => m.Getter != null && m.Getter.CMethodName.Equals(pi.Getter.CMethodName)) != null)
                                {
                                    pi.Getter = null;
                                }
                            }

                            if (pi.Setter == null && pi.Getter == null)
                            {
                                clazz.Properties.RemoveAt(i);
                                --i;
                            }
                        }
                        baseClass = classes.FirstOrDefault(o => o.Name.Equals(baseClass.BaseClass));
                    } while (baseClass != null);
                }
            }
            #endregion

            #region RESOLVE_BASE_CLASSES
            foreach (ClassInfo clazz in classes)
            {
                ClassInfo cl = classes.GetClass(clazz.BaseClass);
                while (cl != null && cl.BaseClass != null && cl.BaseClass.Length > 0)
                {
                    clazz.BaseClasses.Add(cl.Name);
                    cl = classes.GetClass(cl.BaseClass);
                }
            }
            #endregion

            #region CLASS_REFERENCES
            foreach (ClassInfo clazz in classes)
                ReferenceResolver.BakeReferences(clazz, classes);
            #endregion

            List<EnumData> enumerations = new List<EnumData>();
            XmlElement enums = doc.GetElementsByTagName("Enums").Item(0) as XmlElement;
            foreach (XmlElement enu in enums.GetElementsByTagName("enum"))
            {
                EnumData e = new EnumData { Name = enu.GetAttribute("name") };
                foreach (XmlElement val in enu.GetElementsByTagName("value"))
                {
                    e.Values.Add(val.GetAttribute("text"));
                }
                enumerations.Add(e);
            }

            foreach (ClassInfo clazz in classes)
                ReferenceResolver.BakeEnumReferences(clazz, enumerations);

            foreach (ClassInfo clazz in classes)
            {
                if (ExplicitTypes.Contains(clazz.Name))
                    clazz.Ignored = true;
            }

            //TypeWriter.WriteLazyHeader(outputDir, classes);
            //TypeWriter.WriteLazySourceHeader(outputDir, headersPath, classes);
            foreach (ClassInfo clazz in classes)
            {
                if (clazz.Ignored)
                    continue;
                TypeWriter.WriteClass(outputDir, headersPath, clazz, classes, enumerations);
            }
            TypeWriter.WriteEnums(System.IO.Path.Combine(outputDir, "Enumerations.h"), enumerations);
        }
    }
}
