using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EditorCore.Typing
{
    public class PrimitiveTyping : BaseExternalPropertyDescriptor
    {
        public override bool Handles(Type type)
        {
            return type == typeof(float) || type == typeof(int) || type == typeof(float) || type == typeof(uint);
        }

        public override TypeConverter GetConverter(Type type)
        {
            return null;
        }

        public override object GetEditor(PropertyInfo pi)
        {
            if (pi.PropertyType == typeof(bool))
                return new Sce.Atf.Controls.PropertyEditing.BoolEditor();
            else if (pi.PropertyType == typeof(float) || pi.PropertyType == typeof(int) || pi.PropertyType == typeof(uint))
                return new Sce.Atf.Controls.PropertyEditing.NumericEditor(pi.PropertyType);
            return null;
        }
    }

    // Covers vector2-4 and Quaternion
    public class VectorTyping : BaseExternalPropertyDescriptor
    {
        public override bool Handles(Type type)
        {
            if (type == typeof(UrhoBackend.Vector2) || type == typeof(UrhoBackend.Vector3) || type == typeof(UrhoBackend.Vector3) || type == typeof(UrhoBackend.Quaternion))
                return true;
            return false;
        }
        public override TypeConverter GetConverter(Type type)
        {
            if (type == typeof(UrhoBackend.Quaternion))
                return new Data.QuaternionConverter();
            return null;
        }
        public override object GetEditor(PropertyInfo pi)
        {
            if (pi.PropertyType == typeof(UrhoBackend.Vector2))
                return new Controls.PropertyGrid.VectorEditor(pi.PropertyType, new string[] { "X", "Y" });
            else if (pi.PropertyType == typeof(UrhoBackend.Vector3))
                return new Controls.PropertyGrid.VectorEditor(pi.PropertyType, new string[] { "X", "Y", "Z" });
            else if (pi.PropertyType == typeof(UrhoBackend.Quaternion))
                return new Controls.PropertyGrid.VectorEditor(pi.PropertyType, new string[] { "X", "Y", "Z" });
            else if (pi.PropertyType == typeof(UrhoBackend.Vector4))
                return new Controls.PropertyGrid.VectorEditor(pi.PropertyType, new string[] { "X", "Y", "Z", "W" });
            return null;
        }
    }

    public class ResourceRefTyping : BaseExternalPropertyDescriptor
    {
        public override bool Handles(Type type)
        {
            return type == typeof(UrhoBackend.ResourceRef);
        }

        public override TypeConverter GetConverter(Type type)
        {
            string propRes = "";
            return new Data.ResourceRefConverter(propRes);
        }

        public override object GetEditor(PropertyInfo pi)
        {
            string propType = "";
            return new Sce.Atf.Controls.PropertyEditing.FileUriEditor(EditorCore.Interop.UrhoConstants.GetFilters(propType));
        }
    }

    public class EnumTyping : BaseExternalPropertyDescriptor
    {
        public override bool Handles(Type type)
        {
            return type.IsEnum;
        }

        public override TypeConverter GetConverter(Type type)
        {
            return null;
        }

        public override object GetEditor(PropertyInfo pi)
        {
            return new EditorCore.Controls.PropertyGrid.NamedEnumEditor(Enum.GetNames(pi.PropertyType));
        }
    }

    public class CollectionTyping : BaseExternalPropertyDescriptor
    {
        public override bool Handles(Type type)
        {
            return typeof(System.Collections.IList).IsAssignableFrom(type);
        }

        public override TypeConverter GetConverter(Type type)
        {
            return null;
        }

        public override object GetEditor(PropertyInfo pi)
        {
            Sce.Atf.Controls.PropertyEditing.EmbeddedCollectionEditor editor = new Sce.Atf.Controls.PropertyEditing.EmbeddedCollectionEditor();
            editor.GetItemInsertersFunc = (context) =>
                {
                    List<Sce.Atf.Controls.PropertyEditing.EmbeddedCollectionEditor.ItemInserter> ret = new List<Sce.Atf.Controls.PropertyEditing.EmbeddedCollectionEditor.ItemInserter>();
                    Sce.Atf.Controls.PropertyEditing.EmbeddedCollectionEditor.ItemInserter inserter = new Sce.Atf.Controls.PropertyEditing.EmbeddedCollectionEditor.ItemInserter("Record", () =>
                    {
                        ISourcedDescriptor desc = context.Descriptor as ISourcedDescriptor;
                        object val = context.Descriptor.GetValue(desc.Source);
                        if (val == null)
                            return null;
                        Type genericType = context.Descriptor.PropertyType.GenericTypeArguments[0];
                        object newObject = Activator.CreateInstance(genericType);
                        ((System.Collections.IList)val).Add(newObject);
                        return newObject;
                    });
                    ret.Add(inserter);
                    return ret;
                };
            editor.RemoveItemFunc = (context, obj) =>
            {
                ISourcedDescriptor desc = context.Descriptor as ISourcedDescriptor;
                object val = context.Descriptor.GetValue(desc.Source);
                if (val == null)
                    return;
                ((System.Collections.IList)val).Remove(obj);
            };
            return editor;
        }
    }
}
