using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BindingGenerator
{
    public static class TypeWriter
    {
        static string CheckHeader(string headerIn)
        {
            for (int i = 0; i < REPLACE_HEADERS.Length; ++i)
            {
                if (headerIn.Contains(REPLACE_HEADERS[i]))
                    headerIn = headerIn.Replace(REPLACE_HEADERS[i], REPLACE_HEADERS_VALUES[i]);
            }
            return headerIn;
        }
        static readonly string[] REPLACE_HEADERS = { 
            "#include \"Serializable.h\"",
            "#include \"Component.h\"",
            "#include \"UIElement.h\"",
            "#include \"Object.h\"",
            "#include \"RefCounted.h\"",
            
            "Direct3D9/D3D9", // Map graphics headers to the right ones
            "Direct3D11/D3D11",
            "OpenGL/OGL"
        };
        static readonly string[] REPLACE_HEADERS_VALUES = { 
            "","","","","", //Core components
            "","","" //Graphics
        };

        static string FindHeaderQuickCheck(string srcPath, string type, string path, Dictionary<string,string> headerCache)
        {
            foreach (string file in System.IO.Directory.EnumerateFiles(path))
            {
                if (file.Contains(type))
                {
                    string fileCode = System.IO.File.ReadAllText(file);
                    if (fileCode.Contains(String.Format("class URHO3D_API {0}", type)))
                    {
                        string newfile = file.Replace(srcPath + "\\", "");
                        newfile = newfile.Replace("\\", "/");
                        newfile = CheckHeader(newfile);
                        newfile = "<" + newfile + ">";
                        headerCache[type] = newfile;
                        return newfile;
                    }
                    else if (fileCode.Contains(String.Format("struct {0}", type)) || fileCode.Contains(String.Format("struct URHO3D_API {0}", type)))
                    {
                        string newfile = file.Replace(srcPath + "\\", "");
                        newfile = newfile.Replace("\\", "/");
                        newfile = CheckHeader(newfile);
                        newfile = "<" + newfile + ">";
                        headerCache[type] = newfile;
                        return newfile;
                    }
                    else if (fileCode.Contains(String.Format("enum {0}\r", type)))
                    {
                        string newfile = file.Replace(srcPath + "\\", "");
                        newfile = newfile.Replace("\\", "/");
                        newfile = CheckHeader(newfile);
                        newfile = "<" + newfile + ">";
                        headerCache[type] = newfile;
                        return newfile;
                    }
                }
            }
            foreach (string dir in System.IO.Directory.EnumerateDirectories(path))
            {
                string ret = FindHeaderQuickCheck(srcPath, type, dir, headerCache);
                if (ret != null)
                    return ret;
            }
            return null;
        }

        static string FindHeader(string srcPath, string type, string path, Dictionary<string,string> headerCache)
        {
            if (headerCache.ContainsKey(type))
                return headerCache[type];

            string quick = FindHeaderQuickCheck(srcPath, type, path, headerCache);
            if (quick != null)
                return quick;

            foreach (string file in System.IO.Directory.EnumerateFiles(path))
            {
                if (file.EndsWith(".h"))
                {
                    string fileCode = System.IO.File.ReadAllText(file);
                    if (fileCode.Contains(String.Format("class URHO3D_API {0}", type)))
                    {
                        string newfile = file.Replace(srcPath + "\\", "");
                        newfile = newfile.Replace("\\", "/");
                        newfile = CheckHeader(newfile);
                        newfile = "<" + newfile + ">";
                        headerCache[type] = newfile;
                        return newfile;
                    }
                    else if (fileCode.Contains(String.Format("struct {0}\r", type)) || fileCode.Contains(String.Format("struct URHO3D_API {0}\r", type))) //Is it a struct?
                    {
                        string newfile = file.Replace(srcPath + "\\", "");
                        newfile = newfile.Replace("\\", "/");
                        newfile = CheckHeader(newfile);
                        newfile = "<" + newfile + ">";
                        headerCache[type] = newfile;
                        return newfile;
                    }
                    else if (fileCode.Contains(String.Format("enum {0}\r", type)))
                    {
                        string newfile = file.Replace(srcPath + "\\", "");
                        newfile = newfile.Replace("\\", "/");
                        newfile = CheckHeader(newfile);
                        newfile = "<" + newfile + ">";
                        headerCache[type] = newfile;
                        return newfile;
                    }
                }
            }
            foreach (string dir in System.IO.Directory.EnumerateDirectories(path))
            {
                string ret = FindHeader(srcPath, type, dir, headerCache);
                if (ret != null)
                    return ret;
            }
            return null;
        }

        public static void WriteClass(string outDir, string headersPath, ClassInfo clazz, List<ClassInfo> clazzes, List<EnumData> enums)
        {
            StringBuilder header = new StringBuilder();
            StringBuilder source = new StringBuilder();

            string namespaceList = "namespace Urho3D {\r\n";
            namespaceList += string.Format("    class {0};\r\n", clazz.Name);
            foreach (ClassInfo cl in clazz.Referenced)
                namespaceList += string.Format("    class {0};\r\n", cl.Name); ;
            namespaceList += "}\r\n";

            string backendNS = "";
            foreach (ClassInfo cl in clazz.Referenced)
            {
                if (cl.Ignored)
                    continue;
                backendNS += string.Format("    ref class {0};\r\n", cl.Name);
            }

            header.AppendFormat(HEADER_START, clazz.Name, clazz.BaseClass, namespaceList);
            source.AppendFormat(SOURCE_BEGIN, clazz.Name);
            
            //#includes //header automatically includes the base class
            Dictionary<string,string> headercache = new Dictionary<string,string>();
            string myHeader = FindHeader(headersPath, clazz.Name, headersPath, headercache);
            if (myHeader != null)
                source.AppendFormat("#include {0}\r\n", myHeader);
            foreach (ClassInfo cl in clazz.Referenced)
            {
                if (cl.Ignored)
                    continue;
                if (!headercache.ContainsKey(cl.Name))
                {
                    string headerFile = FindHeader(headersPath, cl.Name, headersPath, headercache);
                    if (headerFile != null)
                        source.AppendFormat("#include {0}\r\n", headerFile);
                    source.AppendFormat("#include \"{0}.h\"\r\n", cl.Name);
                }
            }
            foreach (EnumData en in clazz.ReferencedEnums)
            {
                if (!headercache.ContainsKey(en.Name))
                {
                    string headerFile = FindHeader(headersPath, en.Name, headersPath, headercache);
                    if (headerFile != null)
                        source.AppendFormat("#include {0}\r\n", headerFile);
                }
            }

            source.Append(SOURCE_NAMESPACE);
            if (clazz.BaseClass != null && clazz.BaseClass.Length > 0)
            {
                string inc = string.Format("#include \"{0}.h\"", clazz.BaseClass);
                inc = CheckHeader(inc);
                header.AppendFormat(HEADER_WITH_BASECLASS, clazz.Name, clazz.BaseClass, backendNS, inc);
                source.AppendFormat(SOURCE_CTOR_BASECLASS, clazz.Name, clazz.BaseClass, clazz.Name.ToLower());
            }
            else
            {
                string h = FindHeader(headersPath, clazz.Name, headersPath, headercache);
                if (h != null)
                {
                    h = CheckHeader(h);
                    header.AppendFormat("#include {0}\r\n\r\n", h);
                }
                header.AppendFormat(HEADER_NOBASE, clazz.Name, backendNS);
                source.AppendFormat(SOURCE_CTOR_NOBASE, clazz.Name, clazz.Name.ToLower());
            }

            header.AppendLine("// Properties");
            PropertyWriter.WriteProperties(header, source, clazz, clazzes, enums);

            header.AppendLine("// Methods");
            MethodWriter.WriteMethods(header, source, clazz, clazzes, enums);

            header.AppendLine("// Fields");
            FieldWriter.WriteFields(header, source, clazz, clazzes, enums);

            header.AppendFormat(HEADER_END, clazz.Name, clazz.Name.ToLower());
            source.Append(SOURCE_END);

            try
            {
                System.IO.File.WriteAllText(System.IO.Path.Combine(outDir, clazz.Name + ".h"), header.ToString());
                System.IO.File.WriteAllText(System.IO.Path.Combine(outDir, clazz.Name + ".cpp"), source.ToString());
            }
            catch (Exception) { }
        }

        static readonly string HEADER_START =
"//THIS FILE IS AUTOGENERATED - DO NOT MODIFY\r\n" +
"#pragma once\r\n\r\n" +
"#include <Urho3D/Urho3D.h>\r\n" +
"#include <Urho3D/Scene/Component.h>\r\n" +
"#include \"../MathBind.h\"\r\n" +
"#include \"../ResourceRefWrapper.h\"\r\n" +
"#include \"../SceneWrappers.h\"\r\n" +
"#include \"../Variant.h\"\r\n" +
"#include \"../StringHash.h\"\r\n" +
"#include \"../Attributes.h\"\r\n\r\n" +
"#include \"Enumerations.h\"\r\n\r\n" +
"{2}\r\n";

        static readonly string HEADER_WITH_BASECLASS =
"{3}\r\n\r\n" +
"namespace UrhoBackend {{\r\n\r\n" + 
"{2}\r\n" +
"    public ref class {0} : public UrhoBackend::{1} {{\r\n" +
"    public:\r\n" +
"        {0}(Urho3D::{0}* comp);\r\n" + 
"        {0}(System::IntPtr^ ptr);\r\n\r\n"; //Classname, Baseclassname

        static readonly string HEADER_NOBASE =
"namespace UrhoBackend {{\r\n\r\n" + 
"{1}\r\n" +
"    public ref class {0} {{\r\n" +
"    public:\r\n" +
"        {0}(Urho3D::{0} comp);\r\n" +
"        {0}(System::IntPtr^ ptr);\r\n" +
"        ~{0}();\r\n\r\n"; //Classname, Baseclassname

        static readonly string HEADER_END = // classname
"\r\n" +
"        Urho3D::{0}* {1}_;\r\n" +
"    }};\r\n" +
"}}\r\n";

        static readonly string SOURCE_BEGIN =
"//THIS FILE IS AUTOGENERATED - DO NOT MODIFY\r\n" +
"#include \"stdafx.h\"\r\n" +
"#include <Urho3D/Urho3D.h>\r\n" +
"#include <Urho3D/Scene/Component.h>\r\n" +
"#include <Urho3D/Container/Str.h>\r\n" +
"#include \"../MathBind.h\"\r\n" +
"#include \"../ResourceRefWrapper.h\"\r\n" +
"#include \"../SceneWrappers.h\"\r\n" +
"#include \"../Variant.h\"\r\n" +
"#include \"../StringHash.h\"\r\n" +
"#include \"../UrControl.h\"\r\n" +
"#include \"{0}.h\"\r\n\r\n";

        static readonly string SOURCE_NAMESPACE =
"\r\nnamespace UrhoBackend {\r\n\r\n";

        static readonly string SOURCE_CTOR_BASECLASS =
"{0}::{0}(Urho3D::{0}* fromUrho) : {1}(fromUrho) {{ {2}_ = fromUrho; }}\r\n" +
"{0}::{0}(System::IntPtr^ ptr) : {0}((Urho3D::{0}*)ptr->ToPointer()) {{ }}\r\n\r\n";

        static readonly string SOURCE_CTOR_NOBASE =
"{0}::{0}(Urho3D::{0} fromUrho) {{ {1}_ = new Urho3D::{0}(); *{1}_ = fromUrho; }}\r\n" +
"{0}::{0}(System::IntPtr^ ptr) : {0}(*((Urho3D::{0}*)ptr->ToPointer())) {{ }}\r\n" +
"{0}::~{0}() {{ delete {1}_; }}\r\n";

        static readonly string SOURCE_END =
"}\r\n"; //end namespace



        public static void WriteEnums(string path, List<EnumData> enums)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("#pragma once\r\n");

            sb.AppendLine("namespace UrhoBackend {\r\n");

            foreach (EnumData en in enums)
            {
                sb.AppendFormat("enum {0} {{\r\n", en.Name);

                for (int i = 0; i < en.Values.Count; ++i)
                {
                    if (i != en.Values.Count - 1)
                        sb.AppendLine("    " + en.Values[i] + ",");
                    else
                        sb.AppendLine("    " + en.Values[i]);
                }

                sb.AppendLine("};\r\n");
            }

            sb.AppendLine("}");

            System.IO.File.WriteAllText(path, sb.ToString());
        }

        public static void WriteLazyHeader(string path, List<ClassInfo> classes)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("// THIS FILE IS AUTOGENERATED - DO NOT MODIFY\r\n");
            sb.AppendLine("#pragma once");

            sb.AppendLine("namespace Urho3D {");
            foreach (ClassInfo cl in classes)
            {
                sb.AppendFormat("    class {0};\r\n", cl.Name);
            }
            sb.AppendLine("}\r\n");

            sb.AppendLine("namespace UrhoBackend {");
            foreach (ClassInfo cl in classes)
            {
                sb.AppendFormat("    ref class {0};\r\n", cl.Name);
            }
            sb.AppendLine("}");

            System.IO.File.WriteAllText(System.IO.Path.Combine(path, "LazyHeader.h"), sb.ToString());
        }

        public static void WriteLazySourceHeader(string path, string headerPath, List<ClassInfo> classes)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("// THIS FILE IS AUTOGENERATED - DO NOT MODIFY\r\n");
            sb.AppendLine("#pragma once");

            Dictionary<string,string> headerCache = new Dictionary<string,string>();
            sb.AppendLine("\r\n// Urho3D Headers\r\n");
            sb.AppendLine("#include <Urho3D/Urho3D.h>");
            foreach (ClassInfo cl in classes)
            {
                string headerFile = FindHeader(headerPath, cl.Name, headerPath, headerCache);
                if (headerFile != null)
                    sb.AppendFormat("#include {0}\r\n", headerFile);
            }

            sb.AppendLine("\r\n// C++/CLI Types\r\n");
            foreach (ClassInfo cl in classes)
            {
                sb.AppendFormat("#include \"{0}.h\"\r\n", cl.Name);
            }

            System.IO.File.WriteAllText(System.IO.Path.Combine(path, "LazySourceHeader.h"), sb.ToString());
        }
    }
}
