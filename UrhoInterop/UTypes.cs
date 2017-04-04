using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Urho {

    public static class Extensions {
        public static XmlElement ValueTag(this XmlElement elem, string value) {
            return elem.SimpleTag("value", value);
        }
        public static XmlElement SimpleTag(this XmlElement elem, string propertyName, string propertyValue) {
            elem.SetAttribute(propertyName, propertyValue);
            return elem;
        }
    }

    public class UColor : BaseClass {
        float r_;
        float g_;
        float b_;
        float a_;

        public float R { get { return r_; } set { r_ = value; OnPropertyChanged("R"); } }
        public float G { get { return g_; } set { g_ = value; OnPropertyChanged("G"); } }
        public float B { get { return b_; } set { b_ = value; OnPropertyChanged("B"); } }
        public float A { get { return a_; } set { a_ = value; OnPropertyChanged("A"); } }

        public UColor() {
            r_ = g_ = b_ = a_ = 1f;
        }

        public static readonly UColor DEFAULT = new UColor(1, 1, 1);

        public UColor(float r, float g, float b) {
            r_ = r;
            g_ = g;
            b_ = b;
            a_ = 1f;
        }

        public UColor(float r, float g, float b, float a) {
            r_ = r;
            g_ = g;
            b_ = b;
            a_ = a;
        }

        public static UColor FromString(string str) {
            UColor c = new UColor();
            string[] parts = str.Split(' ');
            c.R = float.Parse(parts[0]);
            c.G = float.Parse(parts[1]);
            c.B = float.Parse(parts[2]);
            if (str.Length == 4)
                c.A = float.Parse(parts[3]);
            else
                c.A = 1f;
            return c;
        }

        public static string ColorToString(UColor input) {
            StringBuilder ret = new StringBuilder();
            ret.Append(input.R);
            ret.Append(" ");
            ret.Append(input.G);
            ret.Append(" ");
            ret.Append(input.B);
            ret.Append(" ");
            ret.Append(input.A);
            return ret.ToString();
        }

        public override string ToString() {
            return ColorToString(this);
        }
    }

    public class MinMax : NamedBaseClass {
        float min_ = 0;
        float max_ = 1;

        public MinMax() {
        }

        public MinMax(float min, float max) {
            min_ = min;
            max_ = max;
        }

        public float Min {
            get { return min_; }
            set {
                min_ = value;
                OnPropertyChanged("Min");
            }
        }
        public float Max {
            get { return max_; }
            set {
                max_ = value;
                OnPropertyChanged("Max");
            }
        }

        public static MinMax FromElement(XmlElement e) {
            MinMax ret = new MinMax();
            if (e.HasAttribute("value")) {
                ret.Min = ret.Max = float.Parse(e.GetAttribute("value"));
            } else {
                ret.Min = float.Parse(e.GetAttribute("min"));
                ret.Max = float.Parse(e.GetAttribute("max"));
            }
            return ret;
        }

        public XmlElement ToElement(XmlElement e) {
            e.SetAttribute("min", min_.ToString());
            e.SetAttribute("max", max_.ToString());
            return e;
        }
    }

    public class Vector2 : NamedBaseClass {
        float x_ = 0;
        float y_ = 0;

        public static readonly Vector2 Zero = new Vector2(0, 0);

        public Vector2() { }
        public Vector2(float x, float y) {
            x_ = x;
            y_ = y;
        }

        public float X {
            get { return x_; }
            set { x_ = value; OnPropertyChanged("X"); }
        }
        public float Y {
            get { return y_; }
            set { y_ = value; OnPropertyChanged("Y"); }
        }

        public static Vector2 FromString(string str) {
            string[] parts = str.Split(' ');
            Vector2 r = new Vector2();
            r.X = float.Parse(parts[0]);
            r.Y = float.Parse(parts[1]);
            return r;
        }

        public override string ToString() {
            return X + " " + Y;
        }

        public static Vector2 operator-(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(lhs.X - rhs.X, lhs.Y - rhs.Y);
        }
    }

    public class Vector3 : Vector2 {
        float z_ = 0;

        public static new readonly Vector3 Zero = new Vector3(0, 0, 0);

        public Vector3() { }
        public Vector3(float x, float y, float z) {
            X = x;
            Y = y;
            z_ = z;
        }
        
        public float Z {
            get { return z_; }
            set { z_ = value; OnPropertyChanged("Z"); }
        }

        public new static Vector3 FromString(string str) {
            string[] parts = str.Split(' ');
            Vector3 r = new Vector3();
            r.X = float.Parse(parts[0]);
            r.Y = float.Parse(parts[1]);
            r.Z = float.Parse(parts[2]);
            return r;
        }

        public override string ToString() {
            return X + " " + Y + " " + Z;
        }
    }

    public class Vector4 : Vector3 {
        float w_ = 0;

        public Vector4() { }
        public Vector4(float x, float y, float z, float w) {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public static new readonly Vector4 Zero = new Vector4(0, 0, 0, 0);
        public static new readonly Vector4 One = new Vector4(1, 1, 1, 1);

        public float W {
            get { return w_; }
            set { w_ = value; OnPropertyChanged("W"); }
        }

        public new static Vector4 FromString(string str) {
            string[] parts = str.Split(' ');
            Vector4 r = new Vector4();
            r.X = float.Parse(parts[0]);
            if (parts.Length >= 2)
                r.Y = float.Parse(parts[1]);
            if (parts.Length >= 3)
                r.Z = float.Parse(parts[2]);
            if (parts.Length >= 4)
                r.W = float.Parse(parts[3]);
            return r;
        }

        public override string ToString() {
            return X + " " + Y + " " + Z + " " + W;
        }
    }

    public class Rect
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public float XMax
        {
            get
            {
                return X + Width;
            }
            set
            {
                Width = value - X;
            }
        }

        public float YMax
        {
            get
            {
                return Y + Height;
            }
            set
            {
                Height = value - Y;
            }
        }

        public Rect() { X = Y = Width = Height = 0.0f; }
        public Rect(float x, float y, float w, float h) { X = x; Y = y; Width = w; Height = h; }

        public bool Contains(float x, float y)
        {
            return x >= X && y >= Y && x < X + Width && y < Y + Height;
        }

        public bool Overlaps(Rect other)
        {
            return XMax > other.X && YMax > other.Y && X < other.XMax && Y < other.YMax;
        }
    }

    public class Matrix4x4
    {
        float[,] values = new float[4,4];

        public static Matrix4x4 Identity()
        {
            Matrix4x4 ret = new Matrix4x4();
            ret.values[0,0] = 1.0f;
            ret.values[1, 1] = 1.0f;
            ret.values[2, 2] = 1.0f;
            ret.values[3, 3] = 1.0f;
            return ret;
        }

        public float m00 { get { return values[0, 0]; } set { values[0, 0] = value; } }
        public float m01 { get { return values[0, 1]; } set { values[0, 1] = value; } }
        public float m02 { get { return values[0, 2]; } set { values[0, 2] = value; } }
        public float m03 { get { return values[0, 3]; } set { values[0, 3] = value; } }

        public float m10 { get { return values[1, 0]; } set { values[1, 0] = value; } }
        public float m11 { get { return values[1, 1]; } set { values[1, 1] = value; } }
        public float m12 { get { return values[1, 2]; } set { values[1, 2] = value; } }
        public float m13 { get { return values[1, 3]; } set { values[1, 3] = value; } }

        public float m20 { get { return values[2, 0]; } set { values[2, 0] = value; } }
        public float m21 { get { return values[2, 1]; } set { values[2, 1] = value; } }
        public float m22 { get { return values[2, 2]; } set { values[2, 2] = value; } }
        public float m23 { get { return values[2, 3]; } set { values[2, 3] = value; } }

        public float m30 { get { return values[3, 0]; } set { values[3, 0] = value; } }
        public float m31 { get { return values[3, 1]; } set { values[3, 1] = value; } }
        public float m32 { get { return values[3, 2]; } set { values[3, 2] = value; } }
        public float m33 { get { return values[3, 3]; } set { values[3, 3] = value; } }
    }
}
