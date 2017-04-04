using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BindingGenerator
{
    public static class ReferenceResolver
    {
        public static void BakeReferences(ClassInfo forClass, List<ClassInfo> classes)
        {
            foreach (MethodInfo mi in forClass.Methods)
                BakeReferences(forClass, mi, classes);
            foreach (PropertyPair pi in forClass.Properties)
            {
                if (pi.Getter != null) BakeReferences(forClass, pi.Getter, classes);
                if (pi.Setter != null) BakeReferences(forClass, pi.Setter, classes);
            }
            foreach (FieldInfo fi in forClass.Fields)
                CheckTypeName(forClass, fi.Typename, classes);
        }

        public static void BakeEnumReferences(ClassInfo forClass, List<EnumData> enums)
        {
            foreach (MethodInfo mi in forClass.Methods)
                BakeEnumReferences(forClass, mi, enums);
            foreach (PropertyPair pi in forClass.Properties)
            {
                if (pi.Getter != null) BakeEnumReferences(forClass, pi.Getter, enums);
                if (pi.Setter != null) BakeEnumReferences(forClass, pi.Setter, enums);
            }
            foreach (FieldInfo fi in forClass.Fields)
                CheckTypeName(forClass, fi.Typename, enums);
        }

        static void BakeReferences(ClassInfo forClass, MethodInfo method, List<ClassInfo> classes)
        {
            CheckTypeName(forClass, method.GetReturnValue(), classes);
            foreach (string param in method.ParameterTypes)
                CheckTypeName(forClass, param, classes);
        }

        static void BakeEnumReferences(ClassInfo forClass, MethodInfo method, List<EnumData> enums)
        {
            CheckTypeName(forClass, method.GetReturnValue(), enums);
            foreach (string param in method.ParameterTypes)
                CheckTypeName(forClass, param, enums);
        }

        static void CheckTypeName(ClassInfo forClass, string typeName, List<ClassInfo> classes)
        {
            string trimmed = TypeHandling.TrimTypeName(typeName);
            if (!TypeHandling.IsPrimitive(trimmed))
            {
                if (!trimmed.Equals(forClass.Name))
                {
                    ClassInfo clazz = classes.GetClass(trimmed);
                    if (clazz != null && !forClass.Referenced.Contains(clazz))
                        forClass.Referenced.Add(clazz);
                }
            }
        }

        static void CheckTypeName(ClassInfo forClass, string typeName, List<EnumData> enums)
        {
            string trimmed = TypeHandling.TrimTypeName(typeName);
            if (!TypeHandling.IsPrimitive(trimmed))
            {
                if (!trimmed.Equals(forClass.Name))
                {
                    EnumData clazz = enums.GetEnum(trimmed);
                    if (clazz != null && !forClass.ReferencedEnums.Contains(clazz))
                        forClass.ReferencedEnums.Add(clazz);
                }
            }
        }
    }
}
