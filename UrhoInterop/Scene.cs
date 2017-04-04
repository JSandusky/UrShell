using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Urho {
    public class Scene : Node {

        public new static Scene FromXml(XmlElement elem) {
            Scene ret = new Scene();
            ret.LoadXML(elem);
            return ret;
        }

        public override XmlElement SaveTo(XmlDocument doc) {
            XmlElement r = doc.CreateElement("scene");
            WriteInto(r, doc);
            return r;
        }
    }
}
