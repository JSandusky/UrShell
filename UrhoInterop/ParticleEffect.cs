using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Urho {

    public enum EmitterType {
        Sphere,
        Box
    }

    public class ParticleColorFade : BaseClass {
        UColor color_ = new UColor();
        public UColor Color { get { return color_; } set { color_ = value; OnPropertyChanged("Color"); } }
        float time_ = 0f;
        public float Time { get { return time_; } set { time_ = value; OnPropertyChanged("Time"); } }

        public ParticleColorFade() {
            Color = new UColor { R = 1, G = 1, B = 1, A = 1 };
        }

        public ParticleColorFade(XmlElement e)
            : this() 
        {
            color_ = UColor.FromString(e.GetAttribute("color"));
            time_ = float.Parse(e.GetAttribute("time"));
        }
    }

    public class ParticleTexAnim : BaseClass {
        Vector4 vec_ = new Vector4();
        public Vector4 Animation { get { return vec_; } set { vec_ = value; OnPropertyChanged("Animation"); } }
        float time_ = 0f;
        public float Time { get { return time_; } set { time_ = value; OnPropertyChanged("Time"); } }

        public ParticleTexAnim() {
            vec_ = new Vector4();
        }

        public ParticleTexAnim(XmlElement elem) {
            vec_ = Vector4.FromString(elem.GetAttribute("uv"));
            time_ = float.Parse(elem.GetAttribute("time"));
        }
    }

    public class ParticleEffect : NamedBaseClass {
        string material_;

        [Resource(typeof(Material))]
        public string Material {
            get { return material_; }
            set { material_ = value; OnPropertyChanged("Material"); }
        }

        int numParticles_ = 10;
        [DefaultValue(10)]
        public int NumParticles
        {
            get { return numParticles_; }
            set { numParticles_ = value; OnPropertyChanged("NumParticles"); }
        }

        bool updateInvisible_ = false;
        [DefaultValue(false)]
        public bool UpdateInvisible {
            get { return updateInvisible_; }
            set { updateInvisible_ = value; OnPropertyChanged("UpdateInvisible"); }
        }

        bool relative_ = true;
        [DefaultValue(true)]
        public bool Relative {
            get { return relative_; }
            set { relative_ = value; OnPropertyChanged("Relative"); }
        }

        bool scaled_ = true;
        [DefaultValue(true)]
        public bool Scaled {
            get { return scaled_; }
            set { scaled_ = value; OnPropertyChanged("Scaled"); }
        }

        bool sorted_ = false;
        [DefaultValue(false)]
        public bool Sorted {
            get { return sorted_; }
            set { sorted_ = value; OnPropertyChanged("Sorted"); }
        }


        float animLodBias_ = 0.0f;
        [DefaultValue(0.0f)]
        public float AnimLodBias {
            get { return animLodBias_; }
            set { animLodBias_ = value; OnPropertyChanged("AnimLodBias"); }
        }

        EmitterType type_;
        [DefaultValue(EmitterType.Sphere)]
        public EmitterType Type {
            get { return type_; }
            set { type_ = value; OnPropertyChanged("Type"); }
        }

        Vector3 emitterSize_ = new Vector3();
        public Vector3 EmitterSize {
            get { return emitterSize_; }
            set { emitterSize_ = value; OnPropertyChanged("EmitterSize"); }
        }

        float emitterRadius_ = 0f;
        [DefaultValue(0f)]
        public float EmitterRadius {
            get { return emitterRadius_; }
            set { emitterRadius_ = value; OnPropertyChanged("EmitterRadius"); }
        }

        Vector3 directionMin_ = new Vector3 { X = -1, Y = -1, Z = -1 };
        public Vector3 DirectionMin {
            get { return directionMin_; }
            set { directionMin_ = value; OnPropertyChanged("DirectionMin"); }
        }

        Vector3 directionMax_ = new Vector3 { X = 1, Y = 1, Z = 1 };
        public Vector3 DirectionMax {
            get { return directionMax_; }
            set { directionMax_ = value; OnPropertyChanged("DirectionMax"); }
        }

        Vector3 constantForce_ = new Vector3();
        public Vector3 ConstantForce {
            get {
                if (constantForce_ == null)
                    constantForce_ = new Vector3();
                return constantForce_; 
            }
            set { constantForce_ = value; OnPropertyChanged("ConstantForce"); }
        }

        float dampingForce_ = 0;
        public float DampingForce {
            get { return dampingForce_; }
            set { dampingForce_ = value; OnPropertyChanged("DampingForce"); }
        }

        float activeTime_ = 0;
        public float ActiveTime {
            get { return activeTime_; }
            set { activeTime_ = value; OnPropertyChanged("ActiveTime"); }
        }

        float inactiveTime_ = 0;
        public float InActiveTime {
            get { return inactiveTime_; }
            set { inactiveTime_ = value; OnPropertyChanged("InActiveTime"); }
        }

        MinMax interval_ = new MinMax { Min = 10, Max = 10 };
        public MinMax Interval {
            get { return interval_; }
            set { interval_ = value; OnPropertyChanged("Interval"); }
        }

        MinMax emissionRate_ = new MinMax(10,10);
        public MinMax EmissionRate {
            get {
                if (emissionRate_ == null)
                    emissionRate_ = new MinMax(10, 10);
                return emissionRate_; 
            }
            set { emissionRate_ = value; OnPropertyChanged("EmissionRate"); }
        }

        Vector2 minSize_ = new Vector2(0.1f,0.1f);
        public Vector2 MinSize {
            get { return minSize_; }
            set { minSize_ = value; OnPropertyChanged("MinSize"); }
        }

        Vector2 maxSize_ = new Vector2(0.1f,0.1f);
        public Vector2 MaxSize {
            get { return maxSize_; }
            set { maxSize_ = value; OnPropertyChanged("MaxSize"); }
        }

        MinMax timeToLive_ = new MinMax(1,1);
        public MinMax TimeToLive {
            get { return timeToLive_; }
            set { timeToLive_ = value; OnPropertyChanged("TimeToLive"); }
        }

        MinMax velocity_ = new MinMax(1,1);
        public MinMax Velocity {
            get { return velocity_; }
            set { velocity_ = value; OnPropertyChanged("Velocity"); }
        }

        MinMax rotation_ = new MinMax(0,0);
        public MinMax Rotation {
            get { return rotation_; }
            set { rotation_ = value; OnPropertyChanged("Rotation"); }
        }

        MinMax rotationSpeed_ = new MinMax(0,0);
        public MinMax RotationSpeed {
            get { return rotationSpeed_; }
            set { rotationSpeed_ = value; OnPropertyChanged("RotationSpeed"); }
        }

        Vector2 sizeDelta_ = new Vector2(0,1);
        public Vector2 SizeDelta {
            get { return sizeDelta_; }
            set { sizeDelta_ = value; OnPropertyChanged("SizeDelta"); }
        }

        ObservableCollection<ParticleColorFade> colorFade_;
        public ObservableCollection<ParticleColorFade> ColorFade {
            get { return colorFade_; }
        }
        ObservableCollection<ParticleTexAnim> texAnim_;
        public ObservableCollection<ParticleTexAnim> TextureAnim {
            get { return texAnim_; }
        }

        public ParticleEffect(string fileName) {
            Name = fileName;
            colorFade_ = new ObservableCollection<ParticleColorFade>();
            texAnim_ = new ObservableCollection<ParticleTexAnim>();
            type_ = EmitterType.Box;

            string path = System.IO.Path.ChangeExtension(fileName, "xml");
            if (System.IO.File.Exists(path)) {
                    XmlDocument doc = new XmlDocument();
                doc.Load(path);

                foreach (XmlElement mat in doc.DocumentElement.GetElementsByTagName("material"))
                    Material = mat.GetAttribute("name");
                foreach (XmlElement num in doc.DocumentElement.GetElementsByTagName("numparticles"))

                foreach (XmlElement u in doc.DocumentElement.GetElementsByTagName("updateinvisible"))
                    UpdateInvisible = u.GetAttribute("enable").Equals("true");
                foreach (XmlElement rel in doc.DocumentElement.GetElementsByTagName("relative"))
                    Relative = rel.GetAttribute("enable").Equals("true");
                foreach (XmlElement s in doc.DocumentElement.GetElementsByTagName("scaled"))
                    Scaled = s.GetAttribute("enable").Equals("true");
                foreach (XmlElement s in doc.DocumentElement.GetElementsByTagName("sorted"))
                    Sorted = s.GetAttribute("enable").Equals("true");
                foreach (XmlElement s in doc.DocumentElement.GetElementsByTagName("animlodbias"))
                    AnimLodBias = float.Parse(s.GetAttribute("value"));
                foreach (XmlElement t in doc.DocumentElement.GetElementsByTagName("emittertype")) {
                    foreach (EmitterType type in Enum.GetValues(typeof(EmitterType))) {
                        if (type.ToString().ToLower().Equals(t.GetAttribute("value"))) {
                            Type = type;
                            break;
                        }
                    }
                }
                foreach (XmlElement s in doc.DocumentElement.GetElementsByTagName("emittersize"))
                    EmitterSize = Vector3.FromString(s.GetAttribute("value"));
                foreach (XmlElement s in doc.DocumentElement.GetElementsByTagName("emitterradius"))
                    EmitterRadius = float.Parse(s.GetAttribute("value"));
                foreach (XmlElement d in doc.DocumentElement.GetElementsByTagName("direction")) {
                    DirectionMax = Vector3.FromString(d.GetAttribute("max"));
                    DirectionMin = Vector3.FromString(d.GetAttribute("min"));
                }
                foreach (XmlElement s in doc.DocumentElement.GetElementsByTagName("constantforce"))
                    ConstantForce = Vector3.FromString(s.GetAttribute("value"));
                foreach (XmlElement s in doc.DocumentElement.GetElementsByTagName("dampingforce"))
                    DampingForce = float.Parse(s.GetAttribute("value"));
                foreach (XmlElement s in doc.DocumentElement.GetElementsByTagName("activetime"))
                    ActiveTime = float.Parse(s.GetAttribute("value"));
                foreach (XmlElement s in doc.DocumentElement.GetElementsByTagName("inactivetime"))
                    InActiveTime = float.Parse(s.GetAttribute("value"));
                foreach (XmlElement s in doc.DocumentElement.GetElementsByTagName("interval"))
                    Interval = MinMax.FromElement(s);
                foreach (XmlElement s in doc.DocumentElement.GetElementsByTagName("emissionrate"))
                    EmissionRate = MinMax.FromElement(s);
                foreach (XmlElement s in doc.DocumentElement.GetElementsByTagName("particlesize")) {
                    if (s.HasAttribute("min"))
                        MinSize = Vector2.FromString(s.GetAttribute("min"));
                    if (s.HasAttribute("max"))
                        MaxSize = Vector2.FromString(s.GetAttribute("max"));
                    if (s.HasAttribute("value")) {
                        MinSize = Vector2.FromString(s.GetAttribute("value"));
                        MaxSize = Vector2.FromString(s.GetAttribute("value"));
                    }
                }
                foreach (XmlElement s in doc.DocumentElement.GetElementsByTagName("timetolive"))
                    TimeToLive = MinMax.FromElement(s);
                foreach (XmlElement s in doc.DocumentElement.GetElementsByTagName("velocity"))
                    Velocity = MinMax.FromElement(s);
                foreach (XmlElement s in doc.DocumentElement.GetElementsByTagName("rotation"))
                    Rotation = MinMax.FromElement(s);
                foreach (XmlElement s in doc.DocumentElement.GetElementsByTagName("rotationspeed"))
                    RotationSpeed = MinMax.FromElement(s);
                foreach (XmlElement s in doc.DocumentElement.GetElementsByTagName("sizedelta")) {
                    Vector2 sd = new Vector2();
                    if (s.HasAttribute("add"))
                        sd.X = float.Parse(s.GetAttribute("add"));
                    if (s.HasAttribute("mul"))
                        sd.Y = float.Parse(s.GetAttribute("mul"));
                    SizeDelta = sd;
                }

                foreach (XmlElement s in doc.DocumentElement.GetElementsByTagName("colorfade"))
                    ColorFade.Add(new ParticleColorFade(s));
                foreach (XmlElement s in doc.DocumentElement.GetElementsByTagName("texanim"))
                    TextureAnim.Add(new ParticleTexAnim(s));
            }
            else
            {
                // Add the initial color fade
                // ColorFade.Add(new ParticleColorFade() { Color = new UColor(1, 1, 1, 1), Time = 0 });
            }
        }

        public override void Save() {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("particleemitter");
            doc.AppendChild(root);

            root.AppendChild(doc.CreateElement("material").SimpleTag("name", Material));
            root.AppendChild(doc.CreateElement("numparticles").SimpleTag("value", NumParticles.ToString()));
            root.AppendChild(doc.CreateElement("updateinvisible").SimpleTag("enable", UpdateInvisible ? "true" : "false"));
            root.AppendChild(doc.CreateElement("relative").SimpleTag("enable", Relative ? "true" : "false"));
            root.AppendChild(doc.CreateElement("scaled").SimpleTag("enable", Scaled ? "true" : "false"));
            root.AppendChild(doc.CreateElement("sorted").SimpleTag("enable", Sorted ? "true" : "false"));
            root.AppendChild(doc.CreateElement("animlodbias").SimpleTag("value", AnimLodBias.ToString()));
            root.AppendChild(doc.CreateElement("emittertype").ValueTag(Type.ToString().ToLower()));
            if (EmitterSize != null)
                root.AppendChild(doc.CreateElement("emittersize").ValueTag(EmitterSize.ToString()));
            root.AppendChild(doc.CreateElement("emitterradius").ValueTag(EmitterRadius.ToString()));
            
            XmlElement dir = doc.CreateElement("direction");
            dir.SetAttribute("min", DirectionMin.ToString());
            dir.SetAttribute("max", DirectionMax.ToString());
            root.AppendChild(dir);

            root.AppendChild(doc.CreateElement("constantforce").ValueTag(ConstantForce.ToString()));
            root.AppendChild(doc.CreateElement("dampingforce").ValueTag(DampingForce.ToString()));
            root.AppendChild(doc.CreateElement("activetime").ValueTag(ActiveTime.ToString()));
            root.AppendChild(doc.CreateElement("inactivetime").ValueTag(InActiveTime.ToString()));
            root.AppendChild(Interval.ToElement(doc.CreateElement("interval")));
            root.AppendChild(EmissionRate.ToElement(doc.CreateElement("emissionrate")));

            XmlElement psize = doc.CreateElement("particlesize");
            psize.SetAttribute("min", MinSize.ToString());
            psize.SetAttribute("max", MaxSize.ToString());
            root.AppendChild(psize);
            
            root.AppendChild(TimeToLive.ToElement(doc.CreateElement("timetolive")));
            root.AppendChild(Velocity.ToElement(doc.CreateElement("velocity")));
            root.AppendChild(Rotation.ToElement(doc.CreateElement("rotation")));
            root.AppendChild(RotationSpeed.ToElement(doc.CreateElement("rotationspeed")));
            //SIZE DELTA
            XmlElement sized = doc.CreateElement("sizedelta");
            sized.SetAttribute("add", SizeDelta.X.ToString());
            sized.SetAttribute("mul", SizeDelta.Y.ToString());
            root.AppendChild(sized);

            foreach (ParticleColorFade f in ColorFade) {
                XmlElement e = doc.CreateElement("colorfade");
                e.SetAttribute("color", f.Color.ToString());
                e.SetAttribute("time", f.Time.ToString());
                root.AppendChild(e);
            }
            foreach (ParticleTexAnim a in TextureAnim) {
                XmlElement e = doc.CreateElement("texanim");
                e.SetAttribute("uv", a.Animation.ToString());
                e.SetAttribute("time", a.Time.ToString());
                root.AppendChild(e);
            }

            XmlWriterSettings xws = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true };
            using (XmlWriter xw = XmlWriter.Create(System.IO.Path.ChangeExtension(Name, "xml"), xws))
                doc.Save(xw);
        }
    }
}
