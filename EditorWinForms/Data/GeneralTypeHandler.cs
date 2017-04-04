using EditorCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EditorWinForms.Data
{
    public class GeneralPropertyDescriptor : PropertyDescriptor
    {
        PropertyInfo property_;
        Type component_;
        public GeneralPropertyDescriptor(Type comp, PropertyInfo prop) : base(prop.Name, prop.GetCustomAttributes().ToArray() )
        {
            property_ = prop;
            component_ = comp;
        }

        public override Type ComponentType
        {
            get { return component_; }
        }
        public override bool IsReadOnly
        {
            get { return false; }
        }
        public override Type PropertyType
        {
            get { return property_.PropertyType; }
        }
        public override bool CanResetValue(object component)
        {
            return false;
        }
        public override object GetValue(object component)
        {
            if (component == null)
                return null;
            if (PropertyType.IsEnum)
            {

            }
            if (PropertyType == typeof(UrhoBackend.Color))
            {
                return EditorCore.Data.ColorConverter.ConvertTo(property_.GetValue(component), typeof(System.Drawing.Color));
            }
            return property_.GetValue(component);
        }
        public override void SetValue(object component, object value)
        {
            try
            {
                if (PropertyType.IsEnum)
                {
                    property_.SetValue(component, Enum.Parse(PropertyType, value.ToString()));
                    return;
                }
                if (PropertyType == typeof(UrhoBackend.ResourceRef))
                {
                    //UrhoBackend.ResourceRef newRef = value as UrhoBackend.ResourceRef;
                    UrhoBackend.ResourceRef baseRef = GetValue(component) as UrhoBackend.ResourceRef;
                    baseRef.SetResourceName(value.ToString());
                    property_.SetValue(component, baseRef);
                    return;
                }
                if (value != null)
                {
                    if (PropertyType == typeof(UrhoBackend.Color))
                    {
                        property_.SetValue(component, EditorCore.Data.ColorConverter.ConvertFrom(value));
                        return;
                    }
                    property_.SetValue(component, value);
                }
            }
            catch (Exception ex) {
                ErrorHandler.GetInst().Error(ex);
            }
        }
        public override void ResetValue(object component)
        {
            
        }
        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }
        public override object GetEditor(Type editorBaseType)
        {
            // NUMBERS
            if (PropertyType == typeof(int) || PropertyType == typeof(float) || PropertyType == typeof(uint))
                return new Sce.Atf.Controls.PropertyEditing.NumericEditor(PropertyType);
            // BOOL
            else if (PropertyType == typeof(bool))
                return new Sce.Atf.Controls.PropertyEditing.BoolEditor();
            // STRING
            else if (PropertyType == typeof(string))
                return base.GetEditor(editorBaseType);

            // VECTORS
            else if (PropertyType == typeof(UrhoBackend.Vector2))
                return new EditorCore.Controls.PropertyGrid.VectorEditor(PropertyType, new string[] { "X", "Y" });
            else if (PropertyType == typeof(UrhoBackend.IntVector2))
                return new EditorCore.Controls.PropertyGrid.VectorEditor(PropertyType, new string[] { "X", "Y" });
            else if (PropertyType == typeof(UrhoBackend.Vector3))
                return new EditorCore.Controls.PropertyGrid.VectorEditor(PropertyType, new string[] { "X", "Y", "Z" });
            else if (PropertyType == typeof(UrhoBackend.Vector4))
                return new EditorCore.Controls.PropertyGrid.VectorEditor(PropertyType, new string[] { "X", "Y", "Z", "W" });
            else if (PropertyType == typeof(UrhoBackend.Quaternion))
                return new EditorCore.Controls.PropertyGrid.VectorEditor(typeof(UrhoBackend.Vector3), new string[] { "X", "Y", "Z" });
            else if (PropertyType == typeof(UrhoBackend.Color))
                return new EditorCore.Controls.PropertyGrid.ColorPickerEditor { EnableAlpha = true };
            else if (PropertyType == typeof(UrhoBackend.Rect))
                return new EditorCore.Controls.PropertyGrid.VectorEditor(PropertyType, new string[] { "L", "T", "R", "B" });
            else if (PropertyType == typeof(UrhoBackend.IntRect))
                return new EditorCore.Controls.PropertyGrid.VectorEditor(PropertyType, new string[] { "L", "T", "R", "B" });
            else if (PropertyType == typeof(UrhoBackend.ResourceRef))
            {
                UrhoBackend.UrhoResource metaAttr = PropertyType.GetCustomAttribute<UrhoBackend.UrhoResource>();
                if (metaAttr == null)
                    return new Sce.Atf.Controls.PropertyEditing.FileUriEditor();
                return new Sce.Atf.Controls.PropertyEditing.FileUriEditor(EditorCore.Interop.UrhoConstants.GetFilters(metaAttr.ResourceTypes));
            }

            // ENUMERATIONS
            else if (PropertyType.IsEnum)
            {
                return new EditorCore.Controls.PropertyGrid.NamedEnumEditor(Enum.GetNames(PropertyType));
            }
            else if (typeof(UrhoBackend.IParentedList).IsAssignableFrom(PropertyType))
            {
                Sce.Atf.Controls.PropertyEditing.EmbeddedCollectionEditor editor = new Sce.Atf.Controls.PropertyEditing.EmbeddedCollectionEditor();
                editor.GetItemInsertersFunc = (context) =>
                {
                    List<Sce.Atf.Controls.PropertyEditing.EmbeddedCollectionEditor.ItemInserter> ret = new List<Sce.Atf.Controls.PropertyEditing.EmbeddedCollectionEditor.ItemInserter>();
                    UrhoBackend.CollectionInsert insertAttr = property_.GetCustomAttribute<UrhoBackend.CollectionInsert>();
                    if (insertAttr != null)
                    {
                        Sce.Atf.Controls.PropertyEditing.EmbeddedCollectionEditor.ItemInserter inserter = new Sce.Atf.Controls.PropertyEditing.EmbeddedCollectionEditor.ItemInserter("Record", () =>
                        {
                            UrhoBackend.IParentedList src = context.GetValue() as UrhoBackend.IParentedList;
                            return src.Parent.GetType().GetMethod(insertAttr.Method).Invoke(src.Parent, null);
                        });
                        ret.Add(inserter);
                    }
                    return ret;
                };
                UrhoBackend.CollectionRemove remover = property_.GetCustomAttribute<UrhoBackend.CollectionRemove>();
                if (remover != null)
                    editor.RemoveItemFunc = (context, obj) =>
                    {
                        UrhoBackend.IParentedList src = context.GetValue() as UrhoBackend.IParentedList;
                        uint index = (uint)src.IndexOf(obj);
                        src.Parent.GetType().GetMethod(remover.Method).Invoke(src.Parent, new object[] { index });
                    };
                return editor;
            }
            return base.GetEditor(editorBaseType);
        }

        public override PropertyDescriptorCollection GetChildProperties(object instance, Attribute[] filter)
        {
            return new PropertyDescriptorCollection(null);
        }

        public override TypeConverter Converter
        {
            get
            {
                if (PropertyType == typeof(UrhoBackend.Quaternion))
                    return new EditorCore.Data.QuaternionConverter();
                //else if (PropertyType == typeof(UrhoBackend.Color))
                //    return new EditorCore.Data.ColorConverter();
                return null;
            }
        }
    }

    public class GeneralTypeHandler : ICustomTypeDescriptor
    {
        Type t;
        object src;
    
        public GeneralTypeHandler(Type t, object src)
        {
            this.t = t;
            this.src = src;
        }

        public AttributeCollection GetAttributes()
        {
            return new AttributeCollection(null);
        }

        public string GetClassName()
        {
            return t.Name;
        }

        public string GetComponentName()
        {
            return t.Name;
        }

        public TypeConverter GetConverter()
        {
            if (src == null)
                return null;
            return TypeDescriptor.GetConverter(src);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(src);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(src);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(t, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return new EventDescriptorCollection(null);
        }

        public EventDescriptorCollection GetEvents()
        {
            return new EventDescriptorCollection(null);
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return GetProperties();
        }

        public PropertyDescriptorCollection GetProperties()
        {
            List<PropertyDescriptor> desc = new List<PropertyDescriptor>();

            foreach (PropertyInfo pi in t.GetProperties())
            {
                if (pi.GetCustomAttribute<BrowsableAttribute>() != null)
                    continue;
                desc.Add(new GeneralPropertyDescriptor(t, pi));
            }

            PropertyDescriptorCollection ret = new PropertyDescriptorCollection(desc.ToArray());
            return ret;
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return null;
        }

        
    }

    public class GeneralTypeDescriptionProvider : System.ComponentModel.TypeDescriptionProvider
    {
        public override System.ComponentModel.ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
            return new GeneralTypeHandler(objectType, instance);
        }
    }
}
