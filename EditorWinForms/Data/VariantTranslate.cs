using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UrhoBackend;

namespace EditorWinForms.Data
{
    public static class VariantTranslate
    {
        public static Type FromVariantTypeName(string str)
        {
            if (str.Equals("None")) return null;
            if (str.Equals("Int")) return typeof(int);
            if (str.Equals("Bool")) return typeof(bool);
            if (str.Equals("Float")) return typeof(float);
            if (str.Equals("Vector2")) return typeof (UrhoBackend.Vector2);
            if (str.Equals("Vector3")) return typeof(Vector3);
            if (str.Equals("Vector4")) return typeof(Vector4);
            if (str.Equals("Quaternion")) return typeof(Quaternion);
            if (str.Equals("Color")) return typeof(Color);
            if (str.Equals("String")) return typeof(string);
            if (str.Equals("Buffer")) return null; //??
            if (str.Equals("VoidPtr")) return typeof(IntPtr);
            if (str.Equals("ResourceRef")) return typeof(ResourceRef);
            if (str.Equals("ResourceRefList")) return typeof(ResourceRefList);
            if (str.Equals("VariantVector")) return typeof(VariantVector);
            if (str.Equals("VariantMap")) return typeof(VariantMap);
            if (str.Equals("IntRect")) return typeof(IntRect);
            if (str.Equals("IntVector2")) return typeof(IntVector2);
            if (str.Equals("Ptr")) return null;
            if (str.Equals("Matrix3")) return null;
            if (str.Equals("Matrix3x4")) return null;
            if (str.Equals("Matrix4")) return null;
            return null;
        }

        public static Variant VariantFromObject(object obj)
        {
            Variant ret = new Variant();
            if (obj.GetType() == typeof(int))
                ret.SetInt((int)obj);
            else if (obj.GetType() == typeof(bool))
                ret.SetBool((bool)obj);
            else if (obj.GetType() == typeof(float))
                ret.SetFloat((float)obj);
            else if (obj.GetType() == typeof(Vector2))
                ret.SetVector2((Vector2)obj);
            else if (obj.GetType() == typeof(Vector3))
                ret.SetVector3((Vector3)obj);
            else if (obj.GetType() == typeof(Vector4))
                ret.SetVector4((Vector4)obj);
            else if (obj.GetType() == typeof(Quaternion))
                ret.SetQuaternion((Quaternion)obj);
            else if (obj.GetType() == typeof(System.Drawing.Color))
                ret.SetColor(new Color(((System.Drawing.Color)obj).R / 255.0f, ((System.Drawing.Color)obj).G / 255.0f, ((System.Drawing.Color)obj).B / 255.0f, ((System.Drawing.Color)obj).A / 255.0f));
            else if (obj.GetType() == typeof(Color))
                ret.SetColor((Color)obj);
            else if (obj.GetType() == typeof(String))
                ret.SetString((String)obj);
            else if (obj.GetType() == typeof(ResourceRef))
                ret.SetResourceRef((ResourceRef)obj);
            else if (obj.GetType() == typeof(ResourceRefList))
                ret.SetResourceRefList((ResourceRefList)obj);
            else if (obj.GetType() == typeof(VariantVector))
                ret.SetVariantVector((VariantVector)obj);
            else if (obj.GetType() == typeof(VariantMap))
                ret.SetVariantMap((VariantMap)obj);
            else if (obj.GetType() == typeof(IntRect))
                ret.SetIntRect((IntRect)obj);
            else if (obj.GetType() == typeof(IntVector2))
                ret.SetIntVector2((IntVector2)obj);

            return ret;
        }

        public static object ExtractFromVariant(Variant var)
        {
            if (var == null)
                return null;
            switch (var.GetVarTypeID())
            {
                case 0: return null;
                case 1: return var.GetInt();
                case 2: return var.GetBool();
                case 3: return var.GetFloat();
                case 4: return var.GetVector2();
                case 5: return var.GetVector3();
                case 6: return var.GetVector4();
                case 7: return var.GetQuaternion();
                case 8: return var.GetColor();
                case 9: return var.GetString();
                case 10: return null; //buffer
                case 11: return null; //void pointer
                case 12: return var.GetResourceRef();
                case 13: return var.GetResourceRefList();
                case 14: return var.GetVariantVector();
                case 15: return var.GetVariantMap();
                case 16: return var.GetIntRect();
                case 17: return var.GetIntVector2();
                case 18: return null; //ptr
                case 29: return null; // matrix3
                case 20: return null; //matrix3x4
                case 21: return null; //matrix4
            }
            return null;
        }
    }
}
