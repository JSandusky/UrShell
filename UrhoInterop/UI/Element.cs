using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Urho {
    public class Element : NamedBaseClass {
        string type_;
        string style_;
        bool auto_ = true;
        bool internal_ = false;
        ObservableCollection<Attribute> attributes_;
        ObservableCollection<Element> children_;

        public bool IsInternal { get { return internal_; } set { internal_ = value; OnPropertyChanged("IsInternal"); } }
        public bool IsAuto { get { return auto_; } set { auto_ = value; OnPropertyChanged("IsAuto"); } }
        public string Style { get { return style_; } set { style_ = value; OnPropertyChanged("Style"); } }
        public string Type { get { return type_; } set { type_ = value; OnPropertyChanged("Type"); } }

        public ObservableCollection<Attribute> Attributes { get { return attributes_; } }
        public ObservableCollection<Element> Children { get { return children_; } }

        public Element() {
            attributes_ = new ObservableCollection<Attribute>();
            children_ = new ObservableCollection<Element>();
        }

        public Element(string fromFile) {
            //??
        }

        public static Element FromXml(XmlElement aElem) {
            Element r = new Element();
            if (aElem.HasAttribute("style"))
                r.Style = aElem.GetAttribute("style");
            if (aElem.HasAttribute("auto"))
                r.IsAuto = aElem.GetAttribute("auto").Equals("true");
            if (aElem.HasAttribute("internal"))
                r.IsInternal = aElem.GetAttribute("internal").Equals("true");
            foreach (XmlElement e in aElem.ChildNodes) {
                if (e.Name.Equals("attribute"))
                    r.Attributes.Add(Attribute.FromXml(e));
                else if (e.Name.Equals("element"))
                    r.Children.Add(Element.FromXml(e));
            }
            return r;
        }

        public virtual XmlElement SaveTo(XmlDocument aDoc) {
            XmlElement ret = aDoc.CreateElement("element");
            WriteInto(ret, aDoc);
            return ret;
        }

        public void WriteInto(XmlElement aElem, XmlDocument aDoc) {
            if (Type != null && Type.Trim().Length > 0)
                aElem.SetAttribute("type", Type);
            if (Style != null && Style.Trim().Length > 0)
                aElem.SetAttribute("style", Style);
            if (!IsAuto)
                aElem.SetAttribute("auto", "false");
            if (IsInternal)
                aElem.SetAttribute("internal","true");
            foreach (Attribute attr in Attributes)
                aElem.AppendChild(attr.SaveTo(aDoc));
            foreach (Element e in Children)
                aElem.AppendChild(e.SaveTo(aDoc));
        }
    }
}
