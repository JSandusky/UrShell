using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BindingGenerator
{
    public static class MethodWriter
    {
        public static void WriteMethods(StringBuilder header, StringBuilder source, ClassInfo clazz, List<ClassInfo> classes, List<EnumData> enums)
        {
            foreach (MethodInfo method in clazz.Methods)
            {
                string methodName = method.GetMethodName();
                string tail = methodName.Equals("ToString") ? " override" : "";
                header.AppendFormat(HEADER_METHOD, 
                    TypeHandling.ASToCSharpType(method.GetReturnValue(), classes), 
                    methodName, 
                    method.HeaderDeclaration(classes),
                    tail);
                //return, clazz, methodname, parameters, RETURN IF NOT VOID, CMethodName, parameter names
                string getText = TypeHandling.CPPTypeToCSharpValue(method.GetReturnValue(), String.Format("{0}_->{1}({2})", clazz.Name.ToLower(), method.CMethodName, method.SourceCall(classes, enums)), classes, enums);
                source.AppendFormat(SOURCE_METHOD, 
                    TypeHandling.ASToCSharpType(method.GetReturnValue(), classes),  //Return type
                    clazz.Name, 
                    methodName, 
                    method.SourceDeclaration(classes), //
                    method.GetReturnValue().Equals("void") ? "" : "return ", 
                    getText,
                    "");
            }
        }

        static readonly string HEADER_METHOD =
"        {0} {1}({2}){3};\r\n"; // return type, CMethodName, params

        static readonly string SOURCE_METHOD =
"{0} {1}::{2}({3}) {6} {{ {4} {5}; }}\r\n\r\n"; //return, clazz, methodname, parameters, RETURN IF NOT VOID, CMethodName, parameter names


    }
}
