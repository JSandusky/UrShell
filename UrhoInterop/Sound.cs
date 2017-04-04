using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Urho {
    public class Sound : NamedBaseClass {
        int freq_;
        bool sixteenBit_ = false;
        bool stereo_ = false;
        bool loop_ = false;
        float loopStart_;
        float loopEnd_;

        [DefaultValue(false)]
        public bool IsStereo { get { return stereo_; } set{ stereo_ = value; OnPropertyChanged("Stereo"); } }
        [DefaultValue(44100)]
        public int Frequency { get { return freq_; } set { freq_ = value; OnPropertyChanged("Frequency"); } }
        [DefaultValue(false)]
        public bool Is16Bit { get { return sixteenBit_; } set { sixteenBit_ = value; OnPropertyChanged("Is16Bit"); } }
        [DefaultValue(false)]
        public bool IsLooping { get { return loop_; } set { loop_ = value; OnPropertyChanged("IsLooping"); } }
        [DefaultValue(0f)]
        public float LoopStart { get { return loopStart_; } set { loopStart_ = value; OnPropertyChanged("LoopStart"); } }
        [DefaultValue(0f)]
        public float LoopEnd { get { return loopEnd_; } set { loopEnd_ = value; OnPropertyChanged("LoopEnd"); } }

        public Sound(string fileName) {
            Name = fileName;
            string path = System.IO.Path.ChangeExtension(fileName, "xml");
            if (System.IO.File.Exists(path)) {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                foreach (XmlElement elem in doc.DocumentElement.GetElementsByTagName("format")) {
                    if (elem.HasAttribute("frequency"))
                        Frequency = int.Parse(elem.GetAttribute("frequency"));
                    if (elem.HasAttribute("sixteenbit"))
                        Is16Bit = elem.GetAttribute("sixteenbit").Equals("true");
                    if (elem.HasAttribute("stereo"))
                        IsStereo = elem.GetAttribute("stereo").Equals("true");
                }
                foreach (XmlElement elem in doc.DocumentElement.GetElementsByTagName("loop")) {
                    if (elem.HasAttribute("enable"))
                        IsLooping = elem.GetAttribute("enable").Equals("true");
                    if (elem.HasAttribute("start"))
                        LoopStart = float.Parse(elem.GetAttribute("start"));
                    if (elem.HasAttribute("end"))
                        LoopEnd = float.Parse(elem.GetAttribute("end"));
                }
            }
        }

        public override void Save() {
            string path = System.IO.Path.ChangeExtension(Name, "xml");
            if (DefaultValue.IsDefault(this)) {
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);
                return;
            }

            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("sound");
            doc.AppendChild(root);

            XmlElement format = doc.CreateElement("format");
            doc.AppendChild(format);
            if (Frequency != 0)
                format.SetAttribute("frequency", Frequency.ToString());
            if (Is16Bit)
                format.SetAttribute("sixteenbit", Is16Bit ? "true":"false");
            format.SetAttribute("stereo", IsStereo ? "true" : "false");

            if (IsLooping) {
                XmlElement loop = doc.CreateElement("loop");
                doc.AppendChild(loop);
                loop.SetAttribute("enable", IsLooping ? "true" : "false");
                if (LoopStart != 0)
                    loop.SetAttribute("start", LoopStart.ToString());
                if (LoopEnd != 0)
                    loop.SetAttribute("end", LoopEnd.ToString());
            }

            XmlWriterSettings xws = new XmlWriterSettings { OmitXmlDeclaration = true };
            using (XmlWriter xw = XmlWriter.Create(path, xws))
                doc.Save(xw);
        }
    }
}
