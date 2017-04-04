using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrhoBackend;

namespace EditorWinForms.Data
{
    public enum VariantType
    {
        None,
        Int,
        Bool,
        Float,
        Vector2,
        Vector3,
        Vector4,
        Quaternion,
        Color,
        String,
        Buffer,
        VoidPtr,
        ResourceRef,
        ResourceRefList,
        VariantVector,
        VariantMa,
        IntRect,
        IntVector2,
        Ptr,
        Matrix3,
        Matrix3x4,
        Matrix4,
    }

    public class VariantMapList : List<VariantMapRecord>
    {
        public PropertyDescriptor Descriptor;
        public object SrcObject;

        public VariantMapList(PropertyDescriptor descriptor, object src)
        {
            Descriptor = descriptor;
            SrcObject = src;
        }

        public new void Add(VariantMapRecord obj)
        {
            obj.Owner = this;
            base.Add(obj);
        }
    }

    public class VariantMapRecord
    {
        public VariantMapList Owner;
        public bool Enabled = false;

        public VariantMapRecord(VariantMapList owner)
        {
            Owner = owner;
        }

        public VariantMapRecord() { }

        public string DisplayKey { 
            get { return Key != null ? Key : (ExplicitKey != null ? ExplicitKey.ToString() : ""); }
            set { Key = value; Update(); }
        }

        VariantType type_;
        public VariantType Type { get { return type_; } set { type_ = value; Update(); } }

        string value_;
        public string Value { get { return value_; } set { value_ = value; Update(); } }

        [Browsable(false)]
        public StringHash ExplicitKey { get; set; }
        
        [Browsable(false)]
        public string Key { get; set; }

        public static void Fill(VariantMap map, VariantMapList values)
        {
            string[] varNames = Enum.GetNames(typeof(VariantType));
            foreach (VariantMapRecord rec in values)
            {
                Variant var = new Variant();
                string keyname = rec.Key;
                string name = varNames[(int)rec.Type];
                string value = rec.Value;
                
                var.FromString(varNames[(int)rec.Type], rec.Value);
                if (rec.Key != null)
                    map.Set(rec.Key, var);
                else
                    map.Set(rec.ExplicitKey, var);
            }
        }

        bool IsValid()
        {
            return Type != VariantType.None && DisplayKey.Length > 0;
        }

        void Update()
        {
            if (IsValid() && Enabled)
                Owner.Descriptor.SetValue(Owner.SrcObject, Owner);
        }

        public static VariantMapList Get(object src, PropertyDescriptor desc, VariantMap map)
        {
            Scene scene = src as Scene;
            if (scene == null)
            {
                Node nd = src as Node;
                if (nd == null)
                {
                    UrhoBackend.Component cp = src as UrhoBackend.Component;
                    scene = cp.GetScene();
                }
                else
                {
                    scene = nd.GetScene();
                }
            }

            VariantMapList keys = new VariantMapList(desc, src);
            List<string> varNames = new List<string>(Enum.GetNames(typeof(VariantType)));
            List<StringHash> hashes = map.Keys();
            foreach (StringHash sh in hashes)
            {
                Variant v = map.Get(sh);
                string value = v.ToString();
                VariantType vt = (VariantType)varNames.IndexOf(v.GetVarType());
                VariantMapRecord rec = new VariantMapRecord(keys) { ExplicitKey = sh, Type = (VariantType)v.GetVarTypeID(), Value = v.ToString() };
                if (scene != null)
                    rec.Key = VariableUtils.GetVariableName(scene, rec.ExplicitKey);
                rec.Enabled = true;
                keys.Add(rec);
            }
            return keys;
        }
    }

    

    public class VariantVectorRecord
    {
        public VariantType Type;
        public string Value;

        public static List<VariantVectorRecord> From(VariantVector vector)
        {
            List<VariantVectorRecord> ret = new List<VariantVectorRecord>();
            for (int i = 0; i < vector.Count; ++i)
            {
                ret.Add(new VariantVectorRecord { Type = (VariantType)vector[i].GetVarTypeID(), Value = vector[i].ToString() });
            }
            return ret;
        }

        public static void To(VariantVector target, List<VariantVectorRecord> records)
        {
            foreach (VariantVectorRecord rec in records)
            {
                Variant v = new Variant();
                v.FromString(rec.Type.ToString(), rec.Value);
                target.Add(v);
            }
        }
    }

    public static class VariableUtils
    {
        public static string[] GetVariableNames(Scene scene)
        {
            return scene.GetAttribute("Variable Names").GetString().Split(';');
        }

        public static string GetVariableName(Scene scene, StringHash hash)
        {
            string[] names = GetVariableNames(scene);
            foreach (string str in names)
            {
                StringHash sh = new StringHash(str);
                if (sh.Value == hash.Value)
                    return str;
            }
            return null;
        }

        public static void SetVariableNames(Serializable serial, VariantMapList list)
        {
            Scene scene = serial as Scene;
            if (scene != null)
            {
                SetVariableNames(scene, list);
                return;
            }
            Node nd = serial as Node;
            if (nd != null)
            {
                SetVariableNames(nd.GetScene(), list);
                return;
            }
            UrhoBackend.Component comp = serial as UrhoBackend.Component;
            if (comp != null)
            {
                SetVariableNames(comp.GetScene(), list);
                return;
            }
        }

        public static void SetVariableNames(Scene scene, VariantMapList list)
        {
            List<string> names = new List<string>(GetVariableNames(scene));
            foreach (VariantMapRecord rec in list)
            {
                if (names.Contains(rec.Key))
                    continue;
                else
                    names.Add(rec.Key);
            }
            scene.SetAttribute("Variable Names", new Variant(String.Join(";", names.ToArray())));
        }
    }
}
