using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BindingGenerator
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("BindingGenerator - Copyright (C) 2015 JSandusky");
                Console.WriteLine("    Purpose: Generates C++/CLI binding classes based on dox attributes data");
                Console.WriteLine("    Basic: BindingGenerator OutputDirectory");
                Console.WriteLine("    AS: BindingGenerator OutputDirectory InputDir");
                return;
            }

            if (args.Length == 2)
            {
                ASFromXML.Process(args[0], args[1]);
                //List<string> desired = new List<string>();
                //List<ClassInfo> classes = new List<ClassInfo>();
                //List<EnumData> enums = new List<EnumData>();
                //foreach (string file in System.IO.Directory.EnumerateFiles(args[1]))
                //{
                //    if (file.EndsWith("API.cpp"))
                //    {
                //        ASBindingScanner.ScanFile(file, desired, classes, enums);
                //    }
                //}
                //foreach (ClassInfo ci in classes)
                //{
                //    TypeWriter.WriteClass(args[0], ci);
                //}
                //
                //TypeWriter.WriteEnums(System.IO.Path.Combine(args[0], "Enumerations.h"), enums);
            }
            else
            {

                APIDocumentation docs = new APIDocumentation();
                APINode node = docs.DocumentNode.Children.FirstOrDefault(p => p.Name.Equals("Attribute list"));
                foreach (APINode clazz in node.Children)
                {
                    if (clazz.Name.Equals("Node") || clazz.Name.Equals("Scene") || clazz.Name.Equals("UIElement"))
                        continue;
                    string code = String.Format(FRONT_SOURCE, clazz.Name);
                    foreach (APILeaf attr in clazz.Children)
                    {

                        string spacelessName = attr.Name.Replace(" ", "");
                        spacelessName = spacelessName.Substring(0, spacelessName.IndexOf(':'));
                        spacelessName = spacelessName.Replace("-", "");
                        spacelessName = spacelessName.Replace("/", "");
                        spacelessName = spacelessName.Replace(".", "");

                        // If our attribute name collides with our classname, append the last character in repetition
                        // in order to resolve the name collision: ie. the "Text" attribute of "Text" class is exposed as "Textt"
                        // UI hopefully refers to the UAttribute attribute for the correct display name to avoid displaying the minor typographical error
                        if (spacelessName.Equals(clazz.Name))
                            spacelessName += spacelessName.Last();

                        string trimmedName = attr.Name.Substring(0, attr.Name.IndexOf(':') - 1);
                        string attrCode = String.Format(ATTR_ANNOTE, trimmedName);
                        string appendCode = String.Format(ATTR_SOURCE, ConvertTypeName(attr.TypeAnnote), spacelessName, trimmedName, ConvertTypeFuncName(attr.TypeAnnote));
                        code += attrCode;
                        code += appendCode;
                    }
                    code += BACK_SOURCE;
                    System.IO.File.WriteAllText(System.IO.Path.Combine(args[0], clazz.Name + ".h"), code);
                }

                System.IO.File.WriteAllText(System.IO.Path.Combine(args[0], "ComponentCast.h"), CAST_HEADER);

                string convertCode = CAST_SOURCE_FILE_BEGIN;
                foreach (APINode clazz in node.Children)
                {
                    if (clazz.Name.Equals("Node") || clazz.Name.Equals("Scene") || clazz.Name.Equals("UIElement"))
                        continue;
                    convertCode += "#include \"" + clazz.Name + ".h\"\r\n";
                }

                convertCode += CAST_SOURCE_FUNC_BEGIN;
                foreach (APINode clazz in node.Children)
                {
                    if (clazz.Name.Equals("Node") || clazz.Name.Equals("Scene") || clazz.Name.Equals("UIElement"))
                        continue;
                    convertCode += String.Format("        if (typeName->Equals(\"{0}\")) return gcnew {0}(comp);\r\n", clazz.Name);
                }
                convertCode += CAST_SOURCE_FILE_END;
                System.IO.File.WriteAllText(System.IO.Path.Combine(args[0], "ComponentCast.cpp"), convertCode);
            }
        }

        public static string ConvertTypeFuncName(string input)
        {
            if (input.Equals("int"))
                return "GetInt()";
            else if (input.Equals("String"))
                return "GetString()";
            else if (input.Equals("float"))
                return "GetFloat()";
            else if (input.Equals("bool"))
                return "GetBool()";
            else if (input.Equals("Vector2"))
                return "GetVector2()";
            else if (input.Equals("Vector3"))
                return "GetVector3()";
            else if (input.Equals("Vector4"))
                return "GetVector4()";
            else if (input.Equals("Quaternion"))
                return "GetQuaternion()";
            else if (input.Equals("IntVector2"))
                return "GetIntVector2()";
            else if (input.Equals("Color"))
                return "GetColor()";
            else if (input.Equals("IntRect"))
                return "GetIntRect()";
            else if (input.Equals("ResourceRef"))
                return "GetResourceRef()";
            else if (input.Equals("ResourceRefList"))
                return "GetResourceRefList()";
            else if (input.Equals("VariantMap"))
                return "GetVariantMap()";
            else if (input.Equals("VariantVector"))
                return "GetVariantVector()";
            else if (input.Equals("Buffer"))
                return "GetBuffer()";
            return "GetInt()";
        }

        public static string ConvertTypeName(string input)
        {
            if (input.Equals("int"))
                return "int";
            else if (input.Equals("String"))
                return "System::String^";
            else if (input.Equals("float"))
                return "float";
            else if (input.Equals("bool"))
                return "bool";
            else if (input.Equals("Vector2"))
                return "UrhoBackend::Vector2^";
            else if (input.Equals("Vector3"))
                return "UrhoBackend::Vector3^";
            else if (input.Equals("Vector4"))
                return "UrhoBackend::Vector4^";
            else if (input.Equals("Quaternion"))
                return "UrhoBackend::Quaternion^";
            else if (input.Equals("IntVector2"))
                return "UrhoBackend::IntVector2^";
            else if (input.Equals("Color"))
                return "UrhoBackend::Color^";
            else if (input.Equals("IntRect"))
                return "UrhoBackend::IntRect^";
            else if (input.Equals("ResourceRef"))
                return "ResourceRef^";
            else if (input.Equals("ResourceRefList"))
                return "ResourceRefList^";
            else if (input.Equals("VariantMap"))
                return "UrhoBackend::VariantMap^";
            else if (input.Equals("VariantVector"))
                return "UrhoBackend::VariantVector^";
            else if (input.Equals("Buffer"))
                return "UrhoBackend::Buffer^";
            return "int";
        }

        static readonly string CAST_HEADER =
"//THIS FILE IS AUTOGENERATED - DO NOT MODIFY\r\n" +
"#pragma once\r\n\r\n" +
"namespace Urho3D {\r\n" +
"    class Component;\r\n" +
"}\r\n\r\n" +
"namespace UrhoBackend {\r\n\r\n" +
"    ref class Component;\r\n\r\n" +
"    Component^ CreateProperComponent(Urho3D::Component*);\r\n\r\n}";

        static readonly string CAST_SOURCE_FILE_BEGIN =
"//THIS FILE IS AUTOGENERATED - DO NOT MODIFY\r\n" +
"#include \"stdafx.h\"\r\n" +
"#include \"ComponentCast.h\"\r\n" + 
"#include <Urho3D/Urho3D.h>\r\n" +
"#include <Urho3D/Scene/Component.h>\r\n" + 
"#include \"../MathBind.h\"\r\n" +
"#include \"../ResourceRefWrapper.h\"\r\n" +
"#include \"../SceneWrappers.h\"\r\n" +
"#include \"../Variant.h\"\r\n" +
"#include \"../StringHash.h\"\r\n";

        static readonly string CAST_SOURCE_FUNC_BEGIN =
"namespace UrhoBackend {\r\n\r\n" + 
"    Component^ CreateProperComponent(Urho3D::Component* comp) {\r\n" + 
"        System::String^ typeName = gcnew System::String(comp->GetTypeName().CString());\r\n\r\n";

        static readonly string CAST_SOURCE_FILE_END =
"        return nullptr;\r\n" +
"    }\r\n" +
"}\r\n";

        static readonly string FRONT_SOURCE =
"//THIS FILE IS AUTOGENERATED - DO NOT MODIFY\r\n" +
"#pragma once\r\n\r\n" +
"#include <Urho3D/Urho3D.h>\r\n" +
"#include <Urho3D/Scene/Component.h>\r\n" +
"#include \"../MathBind.h\"\r\n" +
"#include \"../ResourceRefWrapper.h\"\r\n" +
"#include \"../SceneWrappers.h\"\r\n" +
"#include \"../Variant.h\"\r\n" +
"#include \"../StringHash.h\"\r\n\r\n" +
"#include \"../Attributes.h\"\r\n\r\n" +
"namespace UrhoBackend {{\r\n\r\n" +
"    ref class {0} : public UrhoBackend::Component {{\r\n" +
"    public:\r\n" +
"        {0}(Urho3D::Component* comp) : Component(comp) {{ }}\r\n\r\n";

        static readonly string ATTR_ANNOTE =
"        [UrhoBackend::UAttribute(\"{0}\")]\r\n";

        static readonly string ATTR_SOURCE =
"        property {0} {1} {{ {0} get() {{ return GetAttribute(\"{2}\")->{3}; }} void set({0} value) {{ SetAttribute(\"{2}\", gcnew Variant(value)); }} }}\r\n";

        static readonly string BACK_SOURCE =
"    };\r\n}\r\n";

        static readonly string FRONT_CAST =
"//THIS FILE IS AUTOGENERATED - DO NOT MODIFY\r\n" +
"#pragma once\r\n\r\n" +
"#include \"SceneWrapper.h\"" +
"namespace UrhoBackend {" +
"    template<class T>" +
"    T^ Cast(Component^ comp) {{\r\n";

        static readonly string LINE_CAST =
"        if (comp->GetTypeName()->Equals(\"{0}\"))\r\n" +
"            return gcnew T(comp);";
    }
}
