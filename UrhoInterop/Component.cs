using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Urho {
    public class Component : Node {
        string type_;
        
        public string Type { get { return type_; } set { type_ = value; OnPropertyChanged("Type"); } }

        public new static Component FromXml(XmlElement aElem) {
            Component ret = new Component();
            ret.Type = aElem.GetAttribute("type");
            ret.LoadXML(aElem);
            return ret;
        }

        public override XmlElement SaveTo(XmlDocument doc) {
            XmlElement r = doc.CreateElement("component");
            r.SetAttribute("type", Type);
            WriteInto(r, doc);
            return r;
        }
    }
}
