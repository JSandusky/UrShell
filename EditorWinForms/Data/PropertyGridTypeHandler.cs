using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EditorWinForms.Data
{
    public class AttributePropertyDescriptor : PropertyDescriptor
    {
        UrhoBackend.AttributeInfo attrInfo_;
        UrhoBackend.Serializable source_;
        object lastReturn_;
        String attrName_;
        Type type_;

        public AttributePropertyDescriptor(UrhoBackend.Serializable src, UrhoBackend.AttributeInfo attrInfo, Type typeInfo) :
            base(attrInfo.Name, new Attribute[] {})
        {
            attrInfo_ = attrInfo;
            source_ = src;
            attrName_ = attrInfo.Name;
            type_ = typeInfo;
        }

        public override bool CanResetValue(object component)
        {
            return true;
        }

        public override Type ComponentType
        {
            get { return source_.GetType(); }
        }

        public override object GetValue(object component)
        {
            UrhoBackend.Serializable wrapper = component as UrhoBackend.Serializable;
            PropertyGridTypeHandler handler = component as PropertyGridTypeHandler;
            if (handler != null && wrapper == null)
                wrapper = handler.wrapper;

            if (wrapper != null)
            {
                UrhoBackend.Variant var = wrapper.GetAttribute(attrName_);
                /*if (type_ == typeof(UrhoBackend.ResourceRefList))
                    return var.GetResourceRefList().ToList();
                else */if (attrInfo_.EnumNames != null && attrInfo_.EnumNames.Count > 0)
                {
                    return attrInfo_.EnumNames[var.GetInt()];
                }
                else if (type_ == typeof(UrhoBackend.Quaternion))
                {
                    return var.GetQuaternion().ToEulerAngles();
                }
                else if (type_ == typeof(UrhoBackend.VariantMap))
                {
                    if (!source_.Equals(wrapper) || lastReturn_ == null)
                    {
                        source_ = wrapper;
                        lastReturn_ = VariantMapRecord.Get(wrapper, this, var.GetVariantMap()); ;
                        return lastReturn_;
                    }
                    return lastReturn_;
                }
                else if (type_ == typeof(UrhoBackend.VariantVector))
                {
                    return VariantVectorRecord.From(var.GetVariantVector());
                }
                return VariantTranslate.ExtractFromVariant(var);
            }
            return null;
        }

        public override bool IsReadOnly
        {
            get { return false; }
        }

        public override Type PropertyType
        {
            get {
                if (attrInfo_.EnumNames != null && attrInfo_.EnumNames.Count > 0)
                    return typeof(string);
                return type_;
            }
        }

        public override void ResetValue(object component)
        {
            UrhoBackend.Serializable wrapper = component as UrhoBackend.Serializable;
            PropertyGridTypeHandler handler = component as PropertyGridTypeHandler;
            if (handler != null && wrapper == null)
                wrapper = handler.wrapper;

            if (wrapper != null)
            {
                wrapper.SetAttribute(attrName_, wrapper.GetAttributeDefault(attrName_));
            }
        }

        public override void SetValue(object component, object value)
        {
            UrhoBackend.Serializable wrapper = component as UrhoBackend.Serializable;
            PropertyGridTypeHandler handler = component as PropertyGridTypeHandler;
            if (handler != null && wrapper == null)
                wrapper = handler.wrapper;

            if (wrapper != null)
            {
                if (type_ == typeof(UrhoBackend.ResourceRef))
                {
                    UrhoBackend.ResourceRef newRef = value as UrhoBackend.ResourceRef;
                    UrhoBackend.ResourceRef baseRef = wrapper.GetAttributeDefault(attrName_).GetResourceRef();
                    baseRef.SetResourceName(newRef.GetName());
                    wrapper.SetAttribute(attrName_, VariantTranslate.VariantFromObject(baseRef));
                } 
                //else if (type_ == typeof(UrhoBackend.ResourceRefList))
                //{
                //    List<string> list = value as List<string>;
                //    if (list != null)
                //    {
                //        UrhoBackend.ResourceRefList baseList = wrapper.GetAttributeDefault(attrName_).GetResourceRefList();
                //        baseList.FromList(list);
                //        wrapper.SetAttribute(attrName_, new UrhoBackend.Variant(baseList));
                //    }
                //} 
                else if (attrInfo_.EnumNames != null && attrInfo_.EnumNames.Count > 0)
                {
                    int index = attrInfo_.EnumNames.IndexOf(value.ToString());
                    wrapper.SetAttribute(attrName_, new UrhoBackend.Variant(index));
                } 
                else if (type_ == typeof(UrhoBackend.Quaternion))
                {
                    UrhoBackend.Vector3 vec3 = value as UrhoBackend.Vector3;
                    UrhoBackend.Quaternion quat = new UrhoBackend.Quaternion();
                    quat.FromEulerAngles(vec3.x, vec3.y, vec3.z);
                    wrapper.SetAttribute(attrName_, new UrhoBackend.Variant(quat));
                } 
                else if (type_ == typeof(UrhoBackend.VariantMap))
                {
                    VariantMapList records = lastReturn_ as VariantMapList; // value as VariantMapList;
                    UrhoBackend.VariantMap newMap = new UrhoBackend.VariantMap();
                    VariantMapRecord.Fill(newMap, records);
                    wrapper.SetAttribute(attrName_, new UrhoBackend.Variant(newMap));
                    VariableUtils.SetVariableNames(wrapper, records);
                }
                else if (type_ == typeof(UrhoBackend.VariantVector))
                {
                    List<VariantVectorRecord> records = value as List<VariantVectorRecord>;
                    UrhoBackend.VariantVector newVec = new UrhoBackend.VariantVector();
                    VariantVectorRecord.To(newVec, records);
                    wrapper.SetAttribute(attrName_, new UrhoBackend.Variant(newVec));
                }
                else
                    wrapper.SetAttribute(attrName_, VariantTranslate.VariantFromObject(value));
            }
            //\todo
        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }

        public override object GetEditor(Type editorBaseType)
        {
            if (attrInfo_.EnumNames != null && attrInfo_.EnumNames.Count > 0)
            {
                List<int> enumValues = new List<int>();
                for (int i = 0; i < attrInfo_.EnumNames.Count; ++i)
                    enumValues.Add(i);
                return new EditorCore.Controls.PropertyGrid.NamedEnumEditor(attrInfo_.EnumNames.ToArray());
            }
            if (attrInfo_.Name.EndsWith("Mask"))
                return new EditorCore.Controls.PropertyGrid.MaskEditor();
            if (type_ == typeof(bool))
                return new Sce.Atf.Controls.PropertyEditing.BoolEditor();
            else if (type_ == typeof(int))
                return new Sce.Atf.Controls.PropertyEditing.NumericEditor(typeof(int));
            else if (type_ == typeof(float))
                return new Sce.Atf.Controls.PropertyEditing.NumericEditor(typeof(float));
            else if (type_ == typeof(UrhoBackend.Vector3))
                return new EditorCore.Controls.PropertyGrid.VectorEditor(typeof(UrhoBackend.Vector3), new[] { "X", "Y", "Z" });
            else if (type_ == typeof(UrhoBackend.Vector2))
                return new EditorCore.Controls.PropertyGrid.VectorEditor(typeof(UrhoBackend.Vector2), new[] { "X", "Y", "Z" });
            else if (type_ == typeof(UrhoBackend.Vector4))
                return new EditorCore.Controls.PropertyGrid.VectorEditor(typeof(UrhoBackend.Vector4), new[] { "X", "Y", "Z", "W" });
            else if (type_ == typeof(UrhoBackend.Color))
                return new Sce.Atf.Controls.PropertyEditing.ColorPickerEditor { EnableAlpha = true };
            else if (type_ == typeof(UrhoBackend.ResourceRef))
            {
                return new Sce.Atf.Controls.PropertyEditing.FileUriEditor(EditorCore.Interop.UrhoConstants.GetFilters(source_.GetAttributeDefault(attrName_).GetResourceRef()));
            }
            else if (type_ == typeof(UrhoBackend.VariantVector))
            {

            }
            else if (type_ == typeof(UrhoBackend.ResourceRefList))
            {
                return new EditorCore.Controls.PropertyGrid.ResourceRefListEditor();
            }
            else if (type_ == typeof(UrhoBackend.VariantMap))
            {
                Sce.Atf.Controls.PropertyEditing.EmbeddedCollectionEditor editor = new Sce.Atf.Controls.PropertyEditing.EmbeddedCollectionEditor();
                editor.GetItemInsertersFunc = (context) =>
                {
                    Sce.Atf.Controls.PropertyEditing.EmbeddedCollectionEditor.ItemInserter inserter = new Sce.Atf.Controls.PropertyEditing.EmbeddedCollectionEditor.ItemInserter("Record", () =>
                    {
                        AttributePropertyDescriptor desc = context.Descriptor as AttributePropertyDescriptor;
                        VariantMapList list = desc.lastReturn_ as VariantMapList;
                        if (list == null)
                            return null;
                        VariantMapRecord rec = new VariantMapRecord(list);
                        list.Add(rec);
                        rec.Enabled = true;
                        return rec;
                    });
                    List<Sce.Atf.Controls.PropertyEditing.EmbeddedCollectionEditor.ItemInserter> ret = new List<Sce.Atf.Controls.PropertyEditing.EmbeddedCollectionEditor.ItemInserter>();
                    ret.Add(inserter);
                    return ret;
                };

                editor.RemoveItemFunc = (context, obj) =>
                {
                    AttributePropertyDescriptor desc = context.Descriptor as AttributePropertyDescriptor;
                    VariantMapList list = desc.lastReturn_ as VariantMapList;
                    if (list == null)
                        return;
                    list.Remove(obj as VariantMapRecord);
                    UrhoBackend.VariantMap map = new UrhoBackend.VariantMap();
                    VariantMapRecord.Fill(map, list);
                    SetValue(context.LastSelectedObject, map);
                };

                return editor;
            }
            else if (type_ == typeof(UrhoBackend.VariantVector))
            {
                Sce.Atf.Controls.PropertyEditing.EmbeddedCollectionEditor editor = new Sce.Atf.Controls.PropertyEditing.EmbeddedCollectionEditor();
                return editor;
            }
            else if (type_ == typeof(UrhoBackend.Quaternion))
            {
                return new EditorCore.Controls.PropertyGrid.VectorEditor(typeof(UrhoBackend.Vector3), new[] { "X", "Y", "Z" });
            }
            return base.GetEditor(editorBaseType);
        }

        public override TypeConverter Converter
        {
            get
            {
                try
                {
                    if (PropertyType == typeof(UrhoBackend.Quaternion))
                        return new EditorCore.Data.QuaternionConverter();
                    else if (PropertyType == typeof(UrhoBackend.Color))
                        return new EditorCore.Data.ColorConverter();
                    else if (PropertyType == typeof(UrhoBackend.ResourceRef))
                    {
                        UrhoBackend.ResourceRef reff = attrInfo_.DefaultValue.GetResourceRef();
                        return new EditorCore.Data.ResourceRefConverter(reff.GetResourceType());
                    }
                    TypeConverter ret = base.Converter;
                    return ret;
                } catch (Exception)
                {
                    return null;
                }
            }
        }

        public override PropertyDescriptorCollection GetChildProperties(object instance, Attribute[] filter)
        {
            //if (PropertyType == typeof(UrhoBackend.Vector2) ||
            //    PropertyType == typeof(UrhoBackend.Vector3) ||
            //    PropertyType == typeof(UrhoBackend.Vector4) ||
            //    PropertyType == typeof(UrhoBackend.IntVector2) ||
            //    PropertyType == typeof(UrhoBackend.IntRect) ||
            //    PropertyType == typeof(UrhoBackend.Color) ||
            //    PropertyType == typeof(UrhoBackend.ResourceRef) ||
            //    PropertyType == typeof(UrhoBackend.ResourceRefList) ||
            //    PropertyType == typeof(UrhoBackend.VariantMap) ||
            //    PropertyType == typeof(UrhoBackend.VariantVector) ||
            //    PropertyType == typeof(UrhoBackend.Quaternion) ||
            //    PropertyType == typeof(UrhoBackend.StringHash))
            return new System.ComponentModel.PropertyDescriptorCollection(null);

            //return base.GetChildProperties(instance, filter);
        }
    }

    public class PropertyGridTypeHandler : ICustomTypeDescriptor
    {
        public UrhoBackend.Serializable wrapper;
        public Type type_;

        public PropertyGridTypeHandler(Type t, UrhoBackend.Serializable wrapped)
        {
            type_ = t;
            wrapper = wrapped;
        }

        public PropertyGridTypeHandler(UrhoBackend.Serializable wrapped)
        {
            wrapper = wrapped;
        }

        public string GetClassName()
        {
            if (wrapper != null)
                return wrapper.GetTypeName();
            return "none";
        }

        public PropertyDescriptorCollection GetProperties()
        {
            List<PropertyDescriptor> properties = new List<PropertyDescriptor>();

            if (wrapper != null)
            {
                foreach (UrhoBackend.AttributeInfo info in wrapper.GetAttributeInfo())
                {
                    if ((info.Mode & 0x8) != 0)
                        continue;
                    properties.Add(new AttributePropertyDescriptor(wrapper, info, VariantTranslate.FromVariantTypeName(wrapper.GetAttributeType(info.Name))));
                }                    
            }

            return new PropertyDescriptorCollection(properties.ToArray());
        }

        public AttributeCollection GetAttributes()
        {
            return new AttributeCollection(null);
        }

        public string GetComponentName()
        {
            return wrapper.GetTypeName();
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return wrapper;
        }


        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return GetProperties();
        }


        public TypeConverter GetConverter()
        {
            if (wrapper == null)
                return null;
            return TypeDescriptor.GetConverter(wrapper);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(wrapper);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(wrapper);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(wrapper, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(wrapper, attributes);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(wrapper);
        }
    }

    [EditorCore.Interfaces.IProgramInitializer("Type Conversions")]
    public class UrhoBackendTypeDescriptionProvider : System.ComponentModel.TypeDescriptionProvider
    {
        public override System.ComponentModel.ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
            return new PropertyGridTypeHandler(objectType, instance as UrhoBackend.Serializable);
            //return base.GetTypeDescriptor(objectType, instance);
        }

        static void ProgramInitialized()
        {
            Assembly urhoAsm = Assembly.GetAssembly(typeof(UrhoBackend.Color));
            foreach (Type t in urhoAsm.GetTypes())
            {
                if (t == typeof(UrhoBackend.Color))
                    continue;
                if (t == typeof(UrhoBackend.Vector2))
                    continue;
                if (t == typeof(UrhoBackend.Vector3))
                    continue;
                if (t == typeof(UrhoBackend.Quaternion))
                    continue;
                if (t == typeof(UrhoBackend.Matrix3))
                    continue;
                if (t == typeof(UrhoBackend.Matrix3x4))
                    continue;
                if (t == typeof(UrhoBackend.Matrix4))
                    continue;
                if (t == typeof(UrhoBackend.IntVector2))
                    continue;
                if (t == typeof(UrhoBackend.IntRect))
                    continue;
                if (t == typeof(UrhoBackend.ResourceRef))
                    continue;
                if (t == typeof(UrhoBackend.ResourceRefList))
                    continue;
                if (t.IsEnum)
                    continue;
                if (t == typeof(UrhoBackend.VariantMap))
                    continue;
                if (t == typeof(UrhoBackend.VariantVector))
                    continue;
                if (t == typeof(UrhoBackend.Buffer))
                    continue;
                if (t == typeof(UrhoBackend.StringHash))
                    continue;

                if (typeof(UrhoBackend.Serializable).IsAssignableFrom(t))
                    System.ComponentModel.TypeDescriptor.AddProvider(new UrhoBackendTypeDescriptionProvider(), t);
                else
                    System.ComponentModel.TypeDescriptor.AddProvider(new GeneralTypeDescriptionProvider(), t);
            }
        }
    }
}
