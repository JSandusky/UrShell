using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BindingGenerator
{
    public static class PropertyWriter
    {
        public static void WriteProperties(StringBuilder header, StringBuilder source, ClassInfo clazz, List<ClassInfo> classes, List<EnumData> enums)
        {
            foreach (PropertyPair property in clazz.Properties)
            {

                if (property.IsIndexer)
                {

                    if (property.Setter != null && property.Getter != null)
                    {
                        header.AppendFormat(HEADER_INDEXER_GET_SET,
                            TypeHandling.ASToCSharpType(property.Getter.GetReturnValue(), classes),
                            property.GetPropertyName(),
                            property.Getter.HeaderDeclaration(classes));


                        string getText = TypeHandling.CPPTypeToCSharpValue(property.Getter.GetReturnValue(), String.Format("{0}_->{1}({2})", clazz.Name.ToLower(), property.Getter.CMethodName, property.Getter.SourceCall(classes, enums)), classes, enums);
                        source.AppendFormat(SOURCE_INDEX_GET,
                            TypeHandling.ASToCSharpType(property.Getter.GetReturnValue(), classes),
                            clazz.Name,
                            property.GetPropertyName(),
                            property.Getter.SourceDeclaration(classes),
                            getText);

                        source.AppendFormat(SOURCE_INDEX_SET,
                            clazz.Name,
                            property.GetPropertyName(),
                            property.Setter.SourceDeclaration(classes),
                            clazz.Name.ToLower(),
                            property.Setter.CMethodName,
                            property.Setter.SourceCall(classes, enums));
                    } 
                    else if (property.Getter != null)
                    {
                        header.AppendFormat(HEADER_INDEXER_GET,
                            TypeHandling.ASToCSharpType(property.Getter.GetReturnValue(), classes),
                            property.GetPropertyName(),
                            property.Getter.HeaderDeclaration(classes));
                        
                        //"{0} {1}::{2}::get({3} value) {{ return {4}; }}\r\n\r\n";
                        string getText = TypeHandling.CPPTypeToCSharpValue(property.Getter.GetReturnValue(), String.Format("{0}_->{1}({2})", clazz.Name.ToLower(), property.Getter.CMethodName, property.Getter.SourceCall(classes, enums)), classes, enums);
                        source.AppendFormat(SOURCE_INDEX_GET,
                            TypeHandling.ASToCSharpType(property.Getter.GetReturnValue(), classes),
                            clazz.Name,
                            property.GetPropertyName(),
                            property.Getter.SourceDeclaration(classes),
                            getText);
                    }
                    else if (property.Setter != null)
                    {
                        header.AppendFormat(HEADER_INDEXER_SET,
                            TypeHandling.ASToCSharpType(property.Setter.GetReturnValue(), classes),
                            property.GetPropertyName(),
                            property.Setter.HeaderDeclaration(classes));

                        //"void {0}::{1}::set({2}) {{ {3}_->{4}({5}); }}\r\n\r\n";
                        source.AppendFormat(SOURCE_INDEX_SET,
                            clazz.Name,
                            property.GetPropertyName(),
                            property.Setter.SourceDeclaration(classes),
                            clazz.Name.ToLower(),
                            property.Setter.CMethodName,
                            property.Setter.SourceCall(classes, enums));
                    }
                }
                else
                {
                    string body = "";
                    string value = "";
                    if (property.Setter != null && property.Getter != null)
                    {
                        body = String.Format(HEADER_BODY_GETSET,
                            TypeHandling.ASToCSharpType(property.Getter.GetReturnValue(), classes)); //return/assignment type
                        value = TypeHandling.ASToCSharpType(property.Getter.GetReturnValue(), classes);
                    }
                    else if (property.Setter != null)
                    {
                        body = String.Format(HEADER_BODY_SET,
                            TypeHandling.ASToCSharpType(property.Setter.HeaderDeclaration(classes), classes)); // assignment type
                        value = TypeHandling.ASToCSharpType(property.Setter.HeaderDeclaration(classes), classes);
                    }
                    else
                    {
                        body = String.Format(HEADER_BODY_GET,
                            TypeHandling.ASToCSharpType(property.Getter.GetReturnValue(), classes)); // return type
                        value = TypeHandling.ASToCSharpType(property.Getter.GetReturnValue(), classes);
                    }

                    header.AppendFormat(HEADER_PROPERTY,
                        value, // Type of the property
                        property.GetPropertyName(),  //name of the property
                        body); // get() set() contents
                    //return value, typename, propertyname, gettername, settername        

                    WriteGetter(header, source, property, clazz, classes, enums);
                    WriteSetter(header, source, property, clazz, classes, enums);
                }
            }
        }

        static void WriteSetter(StringBuilder header, StringBuilder source, PropertyPair property, ClassInfo clazz, List<ClassInfo> classes, List<EnumData> enums)
        {
            if (property.Setter != null)      //input value, typename, propertyname, settername
            {
                string setValue = TypeHandling.CPPTypeFromCSharpValue(property.Setter.ParameterTypes[0], "value", classes, enums);

                source.AppendFormat(SOURCE_PROPERTY_SET,
                    TypeHandling.ASToCSharpType(property.Setter.HeaderDeclaration(classes), classes), // Type of setter
                    clazz.Name,  //Name of class
                    property.GetPropertyName(), //Name of property
                    property.Setter.CMethodName,  //Method to invoke
                    setValue,
                    clazz.Name.ToLower()); //Conversion into CPP
            }
        }

        static void WriteGetter(StringBuilder header, StringBuilder source, PropertyPair property, ClassInfo clazz, List<ClassInfo> classes, List<EnumData> enums)
        {
            if (property.Getter != null)      //return value, typename, propertyname, gettername
            {
                string getText = TypeHandling.CPPTypeToCSharpValue(property.Getter.GetReturnValue(), String.Format("{0}_->{1}()", clazz.Name.ToLower(), property.Getter.CMethodName), classes, enums);
                source.AppendFormat(SOURCE_PROPERTY_GET, 
                    TypeHandling.ASToCSharpType(property.Getter.GetReturnValue(), classes), // Return type name
                    clazz.Name,  //Class name
                    property.GetPropertyName(), //Name of property
                    getText); // Value being set
            }
        }

        static readonly string HEADER_INDEXER_GET =
"        property {0} {1}[{2}] {{ {0} get({2}); }}\r\n";

        static readonly string HEADER_INDEXER_SET =
"        property {0} {1}[{2}] {{ void set({2}, {0}); }}\r\n";

        static readonly string HEADER_INDEXER_GET_SET =
"        property {0} {1}[{2}] {{ {0} get({2}); void set({2}, {0}); }}\r\n";

        static readonly string HEADER_PROPERTY = //return value, propertyname
"        property {0} {1} {{ {2} }}\r\n";

        static readonly string HEADER_BODY_GETSET =
"{0} get(); void set({0});";

        static readonly string HEADER_BODY_GET =
"{0} get();";

        static readonly string HEADER_BODY_SET =
"void set({0});";

        static readonly string SOURCE_PROPERTY_GET = //return value, typename, propertyname, gettername
"{0} {1}::{2}::get() {{ return {3}; }}\r\n";

        static readonly string SOURCE_PROPERTY_SET =//return value, typename, propertyname, settername
"void {1}::{2}::set({0} value) {{ {5}_->{3}({4}); }}\r\n\r\n";

        static readonly string SOURCE_INDEX_GET =
"{0} {1}::{2}::get({3}) {{ return {4}; }}\r\n\r\n";

        static readonly string SOURCE_INDEX_SET =
"void {0}::{1}::set({2}) {{ {3}_->{4}({5}); }}\r\n\r\n";
    }
}
