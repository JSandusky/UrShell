using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Urho {
    [ResourceType(new string[] { "TextureCube" })]
    public class CubeMap : BaseTexture {
        string posX_;
        string negX_;
        string posY_;
        string negY_;
        string posZ_;
        string negZ_;

        [Resource(typeof(Texture))]
        public string PositiveX { get { return posX_; } set { posX_ = value; OnPropertyChanged("PositiveX"); } }
        [Resource(typeof(Texture))]
        public string PositiveZ { get { return posY_; } set { posY_ = value; OnPropertyChanged("PositiveY"); } }
        [Resource(typeof(Texture))]
        public string PositiveY { get { return posZ_; } set { posZ_ = value; OnPropertyChanged("PositiveZ"); } }

        [Resource(typeof(Texture))]
        public string NegativeX { get { return negX_; } set { negX_ = value; OnPropertyChanged("NegativeX"); } }
        [Resource(typeof(Texture))]
        public string NegativeY { get { return negY_; } set { negY_ = value; OnPropertyChanged("NegativeY"); } }
        [Resource(typeof(Texture))]
        public string NegativeZ { get { return negZ_; } set { negZ_ = value; OnPropertyChanged("NegativeZ"); } }

        public CubeMap(string file) {
            //xml file must exist
            Name = file;
            string path = System.IO.Path.ChangeExtension(file, "xml");
            if (System.IO.File.Exists(path)) {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);

                XmlElement first = doc.DocumentElement.FirstChild as XmlElement;
                PositiveX = first.GetAttribute("name");
                first = first.NextSibling as XmlElement;
                NegativeX = first.GetAttribute("name");
                first = first.NextSibling as XmlElement;
                PositiveY = first.GetAttribute("name");
                first = first.NextSibling as XmlElement;
                NegativeY = first.GetAttribute("name");
                first = first.NextSibling as XmlElement;
                PositiveZ = first.GetAttribute("name");
                first = first.NextSibling as XmlElement;
                NegativeZ = first.GetAttribute("name");
            }
        }

        public override void Save() {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("cubemap");
            doc.AppendChild(root);

            XmlElement e = doc.CreateElement("face");
            e.SetAttribute("name", PositiveX);
            root.AppendChild(e);

            e = doc.CreateElement("face");
            e.SetAttribute("name", NegativeX);
            root.AppendChild(e);

            e = doc.CreateElement("face");
            e.SetAttribute("name", PositiveY);
            root.AppendChild(e);

            e = doc.CreateElement("face");
            e.SetAttribute("name", NegativeY);
            root.AppendChild(e);

            e = doc.CreateElement("face");
            e.SetAttribute("name", PositiveZ);
            root.AppendChild(e);

            e = doc.CreateElement("face");
            e.SetAttribute("name", NegativeZ);
            root.AppendChild(e);

            XmlWriterSettings xws = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true };
            using (XmlWriter xw = XmlWriter.Create(System.IO.Path.ChangeExtension(Name, "xml"), xws))
                doc.Save(xw);
        }

        public override bool IsCube { get { return true; } }
    }
}
