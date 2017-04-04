using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using EditorCore;

namespace EditorCore.Settings
{
    public abstract class SettingsObject : EditorCore.Interfaces.SingularObject
    {
        public virtual void RestoreSettings(XmlElement element)
        {
            foreach (PropertyInfo pi in GetType().GetProperties())
            {
                // Should be only one
                foreach (XmlElement propertyElem in element.GetElementsByTagName(pi.Name))
                {
                    System.ComponentModel.TypeConverter converter = System.ComponentModel.TypeDescriptor.GetConverter(pi.PropertyType);
                    if (converter != null)
                    {
                        try
                        {
                            pi.SetValue(this, converter.ConvertFromString(propertyElem.InnerText));
                        } catch (Exception)
                        {

                        }
                    }
                }
            }
        }

        public virtual void StoreSettings(XmlDocument intoDocument, XmlElement intoElement)
        {
            foreach (PropertyInfo pi in GetType().GetProperties())
            {
                object valueObject = pi.GetValue(this);
                if (valueObject == null)
                    continue;

                XmlElement propertyElement = intoDocument.CreateElement(pi.Name);
                intoElement.AppendChild(propertyElement);

                System.ComponentModel.TypeConverter converter = System.ComponentModel.TypeDescriptor.GetConverter(pi.PropertyType);
                if (converter != null)
                    propertyElement.InnerText = converter.ConvertToString(valueObject);
                else
                    propertyElement.InnerText = valueObject.ToString();
            }
        }
    }
}
