using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BindingGenerator
{
    public static class TypeHandling
    {
        static readonly string[] PRIMS = { "int8", "uint8", "int16", "uint16", "int", "uint", "float", "bool" };

        public static string TrimTypeName(string type)
        {
            type = type.Replace("& in", "");
            type = type.Replace("&in", "");
            type = type.Replace("&", "");
            type = type.Replace("@+", "");
            type = type.Replace("@", "");
            type = type.Replace("*", "");
            type = type.Trim();
            return type;
        }

        public static bool IsPrimitive(string type)
        {
            type = TrimTypeName(type);

            if (PRIMS.Contains(type))
                return true;

            return false;
        }

        public static string CPPTypeVariantGetter(string input)
        {
            input = TrimTypeName(input);
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
            else if (input.Equals("Rect"))
                return "GetRect()";
            else if (input.Equals("IntRect"))
                return "GetIntRect()";
            else if (input.Equals("Matrix3"))
                return "GetMatrix3()";
            else if (input.Equals("Matrix3x4"))
                return "GetMatrix3x4()";
            else if (input.Equals("Matrix4"))
                return "GetMatrix4()";
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

        public static string CPPTypeToCSharp(string input)
        {
            input = TrimTypeName(input);
            if (input.CompareTo("int") == 0)
                return "int";
            else if (input.Equals("uint"))
                return "unsigned";
            else if (input.CompareTo("String") == 0)
                return "System::String^";
            else if (input.Equals("float"))
                return "float";
            else if (input.CompareTo("bool") == 0)
                return "bool";
            else if (input.Equals("Vector2"))
                return "UrhoBackend::Vector2^";
            else if (input.Equals("Vector3"))
                return "UrhoBackend::Vector3^";
            else if (input.Equals("Vector4"))
                return "UrhoBackend::Vector4^";
            else if (input.Equals("Matrix3"))
                return "UrhoBackend::Matrix3^";
            else if (input.Equals("Matrix3x4"))
                return "UrhoBackend::Matrix3x4^";
            else if (input.Equals("Matrix4"))
                return "UrhoBackend::Matrix4^";
            else if (input.Equals("Quaternion"))
                return "UrhoBackend::Quaternion^";
            else if (input.Equals("IntVector2"))
                return "UrhoBackend::IntVector2^";
            else if (input.Equals("StringHash"))
                return "UrhoBackend::StringHash^";
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
            return null;
        }

        // Used on Return Values
        public static string CPPTypeToCSharpValue(string type, string value, List<ClassInfo> types, List<EnumData> enums)
        {
            bool isRef = type.Contains("&in");
            bool isPtr = type.Contains("@") || type.Contains("*");
            type = TrimTypeName(type);

            if (type.Equals("String"))
                return "gcnew System::String(" + value + ".CString())";
            else if (type.Equals("Vector2"))
                return "gcnew UrhoBackend::Vector2(" + value + ")";
            else if (type.Equals("Vector3"))
                return "gcnew UrhoBackend::Vector3(" + value + ")";
            else if (type.Equals("Vector4"))
                return "gcnew UrhoBackend::Vector4(" + value + ")";
            else if (type.Equals("Quaternion"))
                return "gcnew UrhoBackend::Quaternion(" + value + ")";
            else if (type.Equals("IntVector2"))
                return "gcnew UrhoBackend::IntVector2(" + value + ")";
            else if (type.Equals("Color"))
                return "gcnew Color(" + value + ")";
            else if (type.Equals("UrhoBackend::IntRect"))
                return "gcnew IntRect(" + value + ")";
            else if (type.Equals("UrhoBackend::Matrix3"))
                return "gcnew Matrix3(" + value + ")";
            else if (type.Equals("UrhoBackend::Matrix3x4"))
                return "gcnew Matrix3x4(" + value + ")";
            else if (type.Equals("UrhoBackend::Matrix4"))
                return "gcnew Matrix4(" + value + ")";
            else if (type.Equals("Rect"))
                return "gcnew UrhoBackend::Rect(" + value + ")";
            else if (type.Equals("StringHash"))
                return "gcnew UrhoBackend::StringHash(" + value + ")";
            else if (type.Equals("ResourceRef"))
                return "gcnew UrhoBackend::ResourceRef(" + value + ")";
            else if (type.Equals("ResourceRefList"))
                return "gcnew UrhoBackend::ResourceRefList" + value + ")";
            else if (type.Equals("VariantMap"))
                return "gcnew UrhoBackend::VariantMap(" + value + ")";
            else if (type.Equals("VariantVector"))
                return "gcnew UrhoBackend::VariantVector(" + value + ")";
            else if (types.ContainsType(type))
                return String.Format("gcnew UrhoBackend::{0}({1})", type, value);
            else
            {
                foreach (EnumData d in enums)
                {
                    if (d.Name.Equals(type))
                    {
                        return string.Format("(UrhoBackend::{0}){1}", type, value);
                    }
                }
            }
            return value;
        }

        // Used on Parameters
        public static string CPPTypeFromCSharpValue(string type, string value, List<ClassInfo> types, List<EnumData> enums)
        {
            bool isRef = type.Contains("&in");
            bool isPtr = type.Contains("@");
            type = TrimTypeName(type);

            if (type.Equals("String"))
                return "Urho3D::String(ToCString(" + value + "))";
            else if (type.Equals("Vector2"))
                return value + "->ToVector2()";
            else if (type.Equals("Vector3"))
                return value + "->ToVector3()";
            else if (type.Equals("StringHash"))
                return value + "->ToStringHash()";
            else if (type.Equals("Vector4"))
                return value + "->ToVector4()";
            
            else if (type.Equals("Matrix3"))
                return value + "->ToMatrix3()";
            else if (type.Equals("Matrix3x4"))
                return value + "->ToMatrix3x4()";
            else if (type.Equals("Matrix4"))
                return value + "->ToMatrix4()";

            else if (type.Equals("Quaternion"))
                return value + "->ToQuaternion()";
            else if (type.Equals("IntVector2"))
                return value + "->ToIntVector2()";
            else if (type.Equals("Color"))
                return value + "->ToColor()";
            else if (type.Equals("IntRect"))
                return value + "->ToIntRect()";
            else if (type.Equals("Rect"))
                return value + "->ToRect()";
            else if (type.Equals("ResourceRef"))
                return (isPtr ? "" : "*") + value + "->ref_";
            else if (type.Equals("ResourceRefList"))
                return (isPtr ? "" : "*") + value + "->refList_";
            else if (type.Equals("VariantMap"))
                return (isPtr ? "" : "*") + value + "->map_";
            else if (type.Equals("VariantVector"))
                return value + "->ToVariantVector()";
            else if (type.Equals("Buffer"))
                return value + "->Buffer_";
            else
            {
                foreach (EnumData en in enums)
                {
                    if (en.Name.Equals(type))
                        return string.Format("(Urho3D::{0}){1}", type, value);
                }
                foreach (ClassInfo ci in types)
                {
                    if (ci.Name.Equals(type))
                        return (isPtr ? "" : "*") + value + String.Format("->{0}_", type.ToLower());
                }
            }
            return value;
        }

        public static string ASToCSharpType(string type, List<ClassInfo> types)
        {
            type = TrimTypeName(type);

            if (type.Equals("uint8"))
                return "unsigned char";
            if (type.Equals("int8"))
                return "char";
            if (type.Equals("uint16"))
                return "unsigned short";
            if (type.Equals("int16"))
                return "short";
            if (type.Equals("uint"))
                return "unsigned";

            string ret = CPPTypeToCSharp(type);
            if (ret != null)
                return ret;

            ClassInfo ty = types.GetClass(type);
            if (ty != null)
                return ty.Name + "^";

            return type;
        }
    }
}
