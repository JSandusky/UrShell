using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace BindingGenerator
{
    public class PropertyPair
    {
        public MethodInfo Getter;
        public MethodInfo Setter;

        public bool IsIndexer
        {
            get
            {
                return (Getter != null && Getter.ParameterTypes != null && Getter.ParameterTypes.Length > 0) || (Setter != null && Setter.ParameterTypes != null && Setter.ParameterTypes.Length > 1);
            }
        }

        public string GetPropertyName()
        {
            if (Getter != null)
                return Getter.ASProperty;
            return Setter.ASProperty;
        }
    }

    public class FieldInfo
    {
        public string Declaration;

        public string Typename { get { return Declaration.Split(' ')[0]; } }

        public string BoundName { get { return Declaration.Split(' ')[1]; } }

        public string MemberName { get { return Declaration.Split(' ')[0] + "_"; } }
    }

    public class MethodInfo
    {
        public MethodInfo()
        {

        }

        public MethodInfo(XmlElement elem)
        {
            Declaration = elem.GetAttribute("declaration");
            Declaration = Declaration.Replace("const ", "");
            Declaration = Declaration.Replace("const", "");
            if (elem.HasAttribute("cmethod"))
                CMethodName = elem.GetAttribute("cmethod");
            if (elem.HasAttribute("return"))
                ReturnValue = elem.GetAttribute("return").Replace("const ", "");
            if (elem.HasAttribute("signature"))
                Signature = elem.GetAttribute("signature");

            ParameterTypes = GetDeclarationTypes();
            ParameterNames = GetDeclarationNames();
        }

        public string Declaration;
        public string CMethodName;

        public string ReturnValue;
        public string Signature;

        public string[] ParameterTypes;
        public string[] ParameterNames;

        public string GetReturnValue()
        {
            if (ReturnValue != null && ReturnValue.Length > 0)
                return ReturnValue;
            ReturnValue = Declaration.Substring(0, Declaration.IndexOf(' '));
            return ReturnValue;
        }

        public bool IsProperty
        {
            get
            {
                return Declaration.Contains(" get_") || Declaration.Contains(" set_");
            }
        }

        public bool IsSetter
        {
            get { return Declaration.Contains(" set_"); }
        }

        public string GetMethodName()
        {
            int startidx = Declaration.IndexOf(' ');
            int endIdx = Declaration.IndexOf('(');
            return Declaration.Substring(startidx + 1, endIdx - startidx - 1);
        }

        public string ASProperty
        {
            get
            {
                if (Declaration.Contains("get_"))
                {
                    int startIdx = Declaration.IndexOf("get_") + 4;
                    int endIDx = Declaration.IndexOf('(');
                    return Declaration.Substring(startIdx, endIDx - startIdx);
                }
                else if (Declaration.Contains("set_"))
                {
                    int startIdx = Declaration.IndexOf("set_") + 4;
                    int endIDx = Declaration.IndexOf('(');
                    return Declaration.Substring(startIdx, endIDx - startIdx);
                }
                return "";
            }
        }

        public bool IsPropertyCompliment(MethodInfo other)
        {
            if (!other.IsProperty)
                return false;
            if (IsSetter == other.IsSetter)
                return false;
            string left = ASProperty;
            string right = other.ASProperty;
            if (IsSetter)
                return right.Contains(left.Replace("set_", "get_"));
            else
                return right.Contains(left.Replace("get_", "set_"));
        }

        string[] GetDeclarationTypes()
        {
            List<string> ret = new List<string>();
            int startIdx = Declaration.IndexOf('(');
            int endIDX = Declaration.LastIndexOf(')');
            string working = Declaration.Substring(startIdx + 1, endIDX - startIdx - 1);
            if (working.Length == 0)
                return new string[] { };

            string[] parts = //working.Split(',');
                Regex.Split(working, ",(?![^(]*\\))");
            for (int i = 0; i < parts.Length; ++i)
            {
                if (parts[i].Contains('='))
                {
                    string assgn = parts[i].Substring(0, parts[i].IndexOf('=') - 1).Trim();
                    string[] assgnSplit = assgn.Split(' ');
                    ret.Add(assgnSplit[0]);
                }
                else
                    ret.Add(parts[i]);
            }

            return ret.ToArray();
        }

        string[] GetDeclarationNames()
        {
            List<string> names = new List<string>();

            int startIdx = Declaration.IndexOf('(');
            int endIDX = Declaration.LastIndexOf(')');
            string working = Declaration.Substring(startIdx + 1, endIDX - startIdx - 1);
            if (working.Length == 0)
                return new string[] {};


            string[] parts = //working.Split(',');
                Regex.Split(working, ",(?![^(]*\\))");
            if (parts.Length > 0)
            {
                char ch = 'A';
                for (int i = 0; i < parts.Length; ++i)
                {
                    if (parts[i].Contains('='))
                    {
                        string assgn = parts[i].Substring(0, parts[i].IndexOf('=') - 1).Trim();
                        string[] assgnParts = assgn.Split(' ');
                        if (assgnParts.Length == 2)
                            names.Add(assgnParts[1]);
                        else
                        {
                            names.Add("" + ch);
                            ++ch;
                        }
                    }
                    else
                    {
                        names.Add("" + ch);
                        ++ch;
                    }
                }
            }

            return names.ToArray();
        }

        public string HeaderDeclaration(List<ClassInfo> classes)
        {
            StringBuilder sb = new StringBuilder();
            if (ParameterTypes != null && ParameterTypes.Length > 0)
            {
                for (int i = 0; i < ParameterTypes.Length; ++i)
                {
                    if (sb.Length > 0)
                        sb.Append(", ");
                    sb.Append(TypeHandling.ASToCSharpType(ParameterTypes[i], classes));
                }
            }
            return sb.ToString();
        }

        public string SourceDeclaration(List<ClassInfo> classes)
        {
            StringBuilder sb = new StringBuilder();

            if (ParameterTypes.Length > 0 && ParameterNames.Length > 0)
            {
                for (int i = 0; i < ParameterTypes.Length; ++i)
                {
                    if (sb.Length > 0)
                        sb.Append(", ");
                    sb.Append(TypeHandling.ASToCSharpType(ParameterTypes[i], classes));
                    sb.Append(" ");
                    sb.Append(ParameterNames[i]);
                }
            }

            return sb.ToString();
        }

        public string SourceCall(List<ClassInfo> clazzes, List<EnumData> enums)
        {
            StringBuilder sb = new StringBuilder();

            if (ParameterTypes.Length > 0 && ParameterNames.Length > 0)
            {
                for (int i = 0; i < ParameterTypes.Length; ++i)
                {
                    if (sb.Length > 0)
                        sb.Append(", ");
                    sb.Append(TypeHandling.CPPTypeFromCSharpValue(ParameterTypes[i], ParameterNames[i], clazzes, enums));
                }
            }
            return sb.ToString();
        }
    }

    public class ReducedClassInfo
    {
        public List<MethodInfo> Methods = new List<MethodInfo>();
        public List<PropertyPair> Properties = new List<PropertyPair>();
    }

    public class EnumData
    {
        public string Name;
        public List<string> Values = new List<string>();
    }

    public class ClassInfo
    {
        public ClassInfo() { }

        public ClassInfo(XmlElement element)
        {
            Name = element.GetAttribute("name");
            if (element.HasAttribute("base"))
                BaseClass = element.GetAttribute("base");
        }

        public bool Ignored = false;
        public List<FieldInfo> Fields = new List<FieldInfo>();
        public List<string> AttributeGetters = new List<string>();
        public List<string> AttributeSetters = new List<string>();
        public List<MethodInfo> Methods = new List<MethodInfo>();
        public List<PropertyPair> Properties = new List<PropertyPair>();
        public List<ClassInfo> Referenced = new List<ClassInfo>();
        public List<EnumData> ReferencedEnums = new List<EnumData>();
        public string Name;
        public string BaseClass;
        public List<String> BaseClasses = new List<string>();

        public ReducedClassInfo Reduce()
        {
            ReducedClassInfo ret = new ReducedClassInfo();

            foreach (MethodInfo mi in Methods)
            {
                if (!mi.IsProperty)
                    ret.Methods.Add(mi);
            }

            List<MethodInfo> ignore = new List<MethodInfo>();
            for (int i = 0; i < Methods.Count; ++i)
            {
                MethodInfo left = Methods[i];
                if (!left.IsProperty)
                    continue;
                if (ignore.Contains(left))
                    continue;
                bool found = false;
                for (int x = 0; x < Methods.Count; ++x)
                {
                    if (i == x)
                        continue;
                    MethodInfo right = Methods[x];
                    if (!right.IsProperty)
                        continue;
                    if (ignore.Contains(right))
                        continue;
                    if (left.IsPropertyCompliment(right))
                    {
                        Methods.RemoveAt(i);
                        --i;
                        found = true;
                        ignore.Add(left);
                        ignore.Add(right);
                        ret.Properties.Add(new PropertyPair
                        {
                            Setter = left.IsSetter ? left : right,
                            Getter = left.IsSetter ? right : left
                        });
                        break;
                    }
                }
                if (!found)
                {
                    Methods.RemoveAt(i);
                    --i;
                    ignore.Add(left);
                    ret.Properties.Add(new PropertyPair
                    {
                        Setter = left.IsSetter ? left : null,
                        Getter = !left.IsSetter ? left : null
                    });
                }
            }

            return ret;
        }

        public static string ConvertASToCSharp(string name)
        {
            if (name.Equals("String^"))
                return "System::String^";
            return name;
        }

        public static string ConvertCSharpToCPP(string typeName, string varName)
        {
            if (typeName.Equals("String^"))
                return "ToCString(varName)";
            return varName;
        }
    }

    public static class AttributeScanner
    {

        public static void ScanFile(string filename)
        {
            string[] lines = System.IO.File.ReadAllLines(filename);
            bool hit = false;
            for (int i = 0; i < lines.Length; ++i)
            {
                string trimmed = lines[i].Trim();

                if (trimmed.Contains("::RegisterObject(Context*"))
                {
                    hit = true;
                }
                if (hit)
                {
                    if (trimmed.Equals("}"))
                        return;


                }
            }
        }
    }

    public static class TypeDataExtensions
    {
        public static bool ContainsType(this List<ClassInfo> list, string typeName)
        {
            return list.FirstOrDefault(c => c.Name.Equals(typeName)) != null;
        }

        public static ClassInfo GetClass(this List<ClassInfo> list, string className)
        {
            return list.FirstOrDefault(c => c.Name.Equals(className));
        }

        public static EnumData GetEnum(this List<EnumData> list, string className)
        {
            return list.FirstOrDefault(c => c.Name.Equals(className));
        }
    }
}
