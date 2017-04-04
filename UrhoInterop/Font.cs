using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Urho {
    public class Font : NamedBaseClass {
        int absoluteX_;
        int absoluteY_;
        float scaledX_;
        float scaledY_;

        public int AbsoluteOffsetX { get { return absoluteX_; } set { absoluteX_ = value; OnPropertyChanged("AbsoluteOffsetX"); } }
        public int AbsoluteOffsetY { get { return absoluteY_; } set { absoluteY_ = value; OnPropertyChanged("AbsoluteOffsetY"); } }
        public float ScaledOffsetX { get { return scaledX_; } set { scaledX_ = value; OnPropertyChanged("ScaledOffsetX"); } }
        public float ScaledOffsetY { get { return scaledY_; } set { scaledY_ = value; OnPropertyChanged("ScaledOffsetY"); } }

        public Font(string name) {
            Name = name;
            string path = System.IO.Path.ChangeExtension(name, "xml");
            if (System.IO.File.Exists(path)) {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                foreach (XmlElement absolute in doc.DocumentElement.GetElementsByTagName("absoluteoffset")) {
                    if (absolute.HasAttribute("x"))
                        AbsoluteOffsetX = int.Parse(absolute.GetAttribute("x"));
                    if (absolute.HasAttribute("y"))
                        AbsoluteOffsetY = int.Parse(absolute.GetAttribute("y"));
                }
                foreach (XmlElement scaled in doc.DocumentElement.GetElementsByTagName("scaledoffset")) {
                    if (scaled.HasAttribute("x"))
                        ScaledOffsetX = float.Parse(scaled.GetAttribute("x"));
                    if (scaled.HasAttribute("y"))
                        ScaledOffsetY = float.Parse(scaled.GetAttribute("y"));
                }
            }
        }

        public override void Save() {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("font");
            doc.AppendChild(root);
            bool shoudSave = false;
            if (AbsoluteOffsetX != 0 && AbsoluteOffsetY != 0) {
                shoudSave = true;
                XmlElement elem = doc.CreateElement("absoluteoffset");
                elem.SetAttribute("x", AbsoluteOffsetX.ToString());
                elem.SetAttribute("y", AbsoluteOffsetY.ToString());
                root.AppendChild(elem);
            }
            if (ScaledOffsetX != 0 && ScaledOffsetY != 0) {
                shoudSave = true;
                XmlElement elem = doc.CreateElement("scaledoffset");
                elem.SetAttribute("x", ScaledOffsetX.ToString());
                elem.SetAttribute("y", ScaledOffsetY.ToString());
                root.AppendChild(elem);
            }
            if (shoudSave) {
                XmlWriterSettings xws = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true };
                using (XmlWriter xw = XmlWriter.Create(System.IO.Path.ChangeExtension(Name, "xml"), xws))
                    doc.Save(xw);
            }
        }
    }
}
