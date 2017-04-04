using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BindingGenerator
{
    public static class FieldWriter
    {

        public static void WriteFields(StringBuilder header, StringBuilder source, ClassInfo clazz, List<ClassInfo> classes, List<EnumData> enums)
        {
            foreach (FieldInfo fi in clazz.Fields)
            {
                string fieldName = fi.BoundName;
                string typeName = TypeHandling.ASToCSharpType(fi.Typename, classes);
                string retValue = TypeHandling.CPPTypeToCSharpValue(fi.Typename, String.Format("{0}_->{1}_", clazz.Name.ToLower(), fi.BoundName), classes, enums);
                string setValue = TypeHandling.CPPTypeFromCSharpValue(fi.Typename, "value", classes, enums);
                header.AppendFormat("        property {0} {1} {{ {0} get(); void set({0} value); }}\r\n", typeName, fieldName);
                source.AppendFormat("{0} {1}::{2}::get() {{ return {3}; }}\r\n", typeName, clazz.Name, fieldName, retValue);
                source.AppendFormat("void {0}::{1}::set({2} value) {{ {4}_->{1}_ = {3}; }}\r\n\r\n", clazz.Name, fieldName, typeName, setValue, clazz.Name.ToLower());
            }
        }
    }
}
