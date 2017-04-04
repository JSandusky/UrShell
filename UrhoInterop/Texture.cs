using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Urho {

    public enum FilteringMode {
        DEFAULT,
        NEAREST,
        BILINEAR,
        TRILINEAR,
        ANISOTROPIC
    }

    public enum TextureAddress {
        WRAP,
        MIRROR,
        REPEAT,
        CLAMP,
        BORDER
    }

    [ResourceType(new string[] { "Texture2D" })]
    public class Texture : BaseTexture {
        UColor border_ = new UColor(1,1,1,1);
        bool srgb_ = false;
        bool mips_ = false;
        int lowQuality_ = 0;
        int highQuality_ = 0;
        int medQuality_ = 0;
        FilteringMode filter_;
        TextureAddress uAddress_;
        TextureAddress vAddress_;
        TextureAddress wAddress_;
        string fileName_;

        [DefaultValue(FilteringMode.DEFAULT)]
        public FilteringMode FilteringMode { get { return filter_; } set { filter_ = value; OnPropertyChanged("FilteringMode"); } }

        [DefaultValue(TextureAddress.WRAP)]
        public TextureAddress UAddress { get { return uAddress_; } set { uAddress_ = value; OnPropertyChanged("UAddress"); } }
        [DefaultValue(TextureAddress.WRAP)]
        public TextureAddress VAddress { get { return vAddress_; } set { vAddress_ = value; OnPropertyChanged("VAddress"); } }
        [DefaultValue(TextureAddress.WRAP)]
        public TextureAddress WAddress { get { return wAddress_; } set { wAddress_ = value; OnPropertyChanged("WAddress"); } }

        [DefaultValue(null)]
        public UColor BorderColor { get { return border_; } set { border_ = value; OnPropertyChanged("BorderColor"); } }
        [DefaultValue(false)]
        public bool Mipmapping { get { return mips_; } set { mips_ = value; OnPropertyChanged("Mipmapping"); } }
        [DefaultValue(false)]
        public bool SRGBEnabled { get { return srgb_; } set { srgb_ = value; OnPropertyChanged("SRGBEnabled"); } }

        [DefaultValue(0)]
        public int LowQualityMIP { get { return lowQuality_; } set { lowQuality_ = value; OnPropertyChanged("LowQualityMIP"); } }
        [DefaultValue(0)]
        public int MedQualityMIP { get { return medQuality_; } set { medQuality_ = value; OnPropertyChanged("MedQualityMIP"); } }
        [DefaultValue(0)]
        public int HighQualityMIP { get { return highQuality_; } set { highQuality_ = value; OnPropertyChanged("HighQualityMIP"); } }

        public Texture(string fromFile) {
            //Check for XML file
            //load xml file
            //Else create a default xml file
            Name = fromFile;
            fileName_ = fromFile;
            string path = System.IO.Path.ChangeExtension(fromFile, "xml");
            try
            {
                if (System.IO.File.Exists(path))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(path);
                    XmlElement root = doc.DocumentElement;
                    foreach (XmlElement elem in root.ChildNodes)
                    {
                        if (elem.Name.ToLower().Equals("address"))
                        {
                            string coord = elem.GetAttribute("coord");
                            string mode = elem.GetAttribute("mode");
                            TextureAddress addr = TextureAddress.CLAMP;
                            if (mode.Equals("wrap"))
                                addr = TextureAddress.WRAP;
                            else if (mode.Equals("repeat"))
                                addr = TextureAddress.REPEAT;
                            else if (mode.Equals("clamp"))
                                addr = TextureAddress.CLAMP;
                            else if (mode.Equals("border"))
                                addr = TextureAddress.BORDER;
                            else if (mode.Equals("mirror"))
                                addr = TextureAddress.MIRROR;

                            if (coord.Equals("u"))
                                this.UAddress = addr;
                            else if (coord.Equals("v"))
                                this.VAddress = addr;
                            else if (coord.Equals("w"))
                                this.WAddress = addr;
                        }
                        else if (elem.Name.ToLower().Equals("border"))
                        {
                            if (elem.HasAttribute("color"))
                                BorderColor = UColor.FromString(elem.GetAttribute("color"));
                        }
                        else if (elem.Name.ToLower().Equals("filter"))
                        {
                            if (elem.HasAttribute("mode"))
                            {
                                string str = elem.GetAttribute("mode").ToLower();
                                if (str.Equals("nearest"))
                                {
                                    FilteringMode = FilteringMode.NEAREST;
                                }
                                else if (str.Equals("bilinear"))
                                {
                                    FilteringMode = FilteringMode.BILINEAR;
                                }
                                else if (str.Equals("trilinear"))
                                {
                                    FilteringMode = FilteringMode.TRILINEAR;
                                }
                                else if (str.Equals("anisotropic"))
                                {
                                    FilteringMode = FilteringMode.ANISOTROPIC;
                                }
                                else
                                {
                                    FilteringMode = FilteringMode.DEFAULT;
                                }
                            }
                        }
                        else if (elem.Name.ToLower().Equals("mipmap"))
                        {
                            if (elem.HasAttribute("enable"))
                                Mipmapping = elem.GetAttribute("enable").Equals("true");
                        }
                        else if (elem.Name.ToLower().Equals("quality"))
                        {
                            if (elem.HasAttribute("low"))
                                LowQualityMIP = int.Parse(elem.GetAttribute("low"));
                            if (elem.HasAttribute("medium"))
                                MedQualityMIP = int.Parse(elem.GetAttribute("medium"));
                            if (elem.HasAttribute("high"))
                                HighQualityMIP = int.Parse(elem.GetAttribute("high"));
                        }
                        else if (elem.Name.ToLower().Equals("srgb"))
                        {
                            if (elem.HasAttribute("enable"))
                                SRGBEnabled = elem.GetAttribute("enable").Equals("true");
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        public override void Save() {
            string path = System.IO.Path.ChangeExtension(fileName_, "xml");
            if (DefaultValue.IsDefault(this)) {
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);
                return;
            }

            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("texture");
            doc.AppendChild(root);

            XmlElement addr = writeAddress(doc, "u", UAddress);
            if (addr != null && !DefaultValue.IsDefault(this, "UAddress"))
                root.AppendChild(addr);
            addr = writeAddress(doc, "v", VAddress);
            if (addr != null && !DefaultValue.IsDefault(this, "VAddress"))
                root.AppendChild(addr);
            addr = writeAddress(doc, "w", WAddress);
            if (addr != null && !DefaultValue.IsDefault(this, "WAddress"))
                root.AppendChild(addr);

            if (BorderColor != null && !DefaultValue.IsDefault(this, "BorderColor")) {
                XmlElement bc = doc.CreateElement("border");
                bc.SetAttribute("color", UColor.ColorToString(BorderColor));
                root.AppendChild(bc);
            }

            XmlElement filt = doc.CreateElement("filter");
            filt.SetAttribute("mode", FilteringMode.ToString().ToLower());
            if (!DefaultValue.IsDefault(this, "FilteringMode"))
                root.AppendChild(filt);

            if (Mipmapping && !DefaultValue.IsDefault(this, "Mipmapping")) {
                XmlElement elem = doc.CreateElement("mipmap");
                elem.SetAttribute("enabled", "true");
                root.AppendChild(elem);
            }

            if (SRGBEnabled && !DefaultValue.IsDefault(this, "SRGBEnabled")) {
                XmlElement elem = doc.CreateElement("srgb");
                elem.SetAttribute("enabled", "true");
                root.AppendChild(elem);
            }

            XmlWriterSettings xws = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true };
            using (XmlWriter xw = XmlWriter.Create(System.IO.Path.ChangeExtension(Name, "xml"), xws))
                doc.Save(xw);
        }

        XmlElement writeAddress(XmlDocument doc, string coord, TextureAddress mode) {
            if (mode != TextureAddress.CLAMP) {
                XmlElement elem = doc.CreateElement("address");
                elem.SetAttribute("coord", coord);
                elem.SetAttribute("mode", mode.ToString().ToLower());
            } return null;
        }

        public override bool IsCube {
            get { return false; }
        }
    }
}
