using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Urho {
    public class Attribute : NamedBaseClass {

        string strValue_;

        public string StringValue { get { return strValue_; } set { strValue_ = value; OnPropertyChanged("StringValue"); } }

        public static Attribute FromXml(XmlElement elem) {
            Attribute ret = new Attribute();
            ret.Name = elem.GetAttribute("name");
            if (elem.HasAttribute("value")) {
                ret.StringValue = elem.GetAttribute("value");
            } else {
            }
            return ret;
        }

        public XmlElement SaveTo(XmlDocument doc) {
            XmlElement r = doc.CreateElement("attribute");
            r.SetAttribute("name", Name);
            r.SetAttribute("value", StringValue);
            return r;
        }
    }
}
