using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorCore.Data
{

    public class EnumConvert : TypeConverter
    {
        string[] values;

        public EnumConvert(string[] vals) { values = vals; }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(int);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value.GetType() == typeof(int))
                return value.ToString();
            for (int i = 0; i < values.Length; ++i)
            {
                if (values[i].Equals(value.ToString()))
                    return i;
            }
            return null;
        }
    }

    [EditorCore.Interfaces.IProgramInitializer("Type Conversions")]
    public class ResourceRefConverter : TypeConverter
    {
        string resName;
        public ResourceRefConverter(string resName)
        {
            this.resName = resName;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value != null && value.GetType() == typeof(string))
                return new UrhoBackend.ResourceRef(resName, value.ToString());
            return null;
        }

        static void ProgramInitialized()
        {
            
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value.GetType() == typeof(UrhoBackend.ResourceRef))
                return ((UrhoBackend.ResourceRef)value).Name;
            else if (destinationType == typeof(UrhoBackend.ResourceRef))
                return new UrhoBackend.ResourceRef(resName, value.ToString());
            return null;
        }
    }

    public class QuaternionConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(UrhoBackend.Vector3);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(UrhoBackend.Vector3) && value.GetType() == typeof(UrhoBackend.Quaternion))
            {
                return ((UrhoBackend.Quaternion)value).ToEulerAngles();
            }
            else if (destinationType == typeof(UrhoBackend.Quaternion) && value.GetType() == typeof(UrhoBackend.Vector3))
            {
                UrhoBackend.Vector3 vec = ((UrhoBackend.Vector3)value);
                return new UrhoBackend.Quaternion(vec.x, vec.y, vec.z);
            }
            else if (value.GetType() == typeof(UrhoBackend.Vector3) && destinationType == typeof(UrhoBackend.Vector3))
                return value;
            return null;
        }
    }

    public class ColorConverter : TypeConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return false;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(UrhoBackend.Color) || 
                destinationType == typeof(string) || 
                destinationType == typeof(System.Drawing.Color);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return CanConvertTo(context, sourceType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            return ConvertTo(value, destinationType);
        }

        public static object ConvertTo(object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                UrhoBackend.Color col = value as UrhoBackend.Color;
                return String.Format("{0} {1} {2} {3}", col.r, col.g, col.b, col.a);
            }
            else if (destinationType == typeof(System.Drawing.Color))
            {
                UrhoBackend.Color col = value as UrhoBackend.Color;
                return System.Drawing.Color.FromArgb((byte)(col.a * 255), (byte)(col.r * 255), (byte)(col.g * 255), (byte)(col.b * 255));
            }
            return value;
        }

        public static object ConvertFrom(object value)
        {
            if (value.GetType() == typeof(string))
            {
                string[] vals = value.ToString().Split(' ');
                UrhoBackend.Color ret = new UrhoBackend.Color();
                for (int i = 0; i < vals.Length; ++i)
                {
                    float fval = 0.0f;
                    float.TryParse(vals[i], out fval);
                    ret.Set(i, fval);
                }
                return ret;
            }
            else if (value.GetType() == typeof(System.Drawing.Color))
            {
                System.Drawing.Color c = (System.Drawing.Color)value;
                UrhoBackend.Color ret = new UrhoBackend.Color();
                ret.r = c.R / 255.0f;
                ret.g = c.G / 255.0f;
                ret.b = c.B / 255.0f;
                ret.a = c.A / 255.0f;
                return ret;
            }
            return value;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            return ConvertFrom(value);
        }
    }
}
