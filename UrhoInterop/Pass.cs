using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Urho {

    public enum PassType {
        Base,
        LitBase,
        Light,
        Alpha,
        LitAlpha,
        PostOpaque,
        Refract,
        PostAlpha,
        Prepass,
        Material,
        Deferred,
        Depth,
        Shadow
    }

    public enum LightModes {
        Unlit,
        PerVertex,
        PerPixel
    }

    public enum BlendModes {
        Replace,
        Add,
        Multiply,
        Alpha,
        AddAlpha,
        PremulAlpha,
        InvDestAlpha,
        Subtract,
        SubtractAlpha
    }

    public enum DepthTest {
        Always,
        Equal,
        Less,
        LessEqual,
        Greater,
        GreaterEqual
    }

    

    public class Pass : NamedBaseClass {
        PassType passType_;
        public PassType PassType {
            get { return passType_; }
            set { passType_ = value; OnPropertyChanged("PassType"); }
        }

        LightModes lightMode_;
        public LightModes LightMode {
            get { return lightMode_; }
            set { lightMode_ = value; OnPropertyChanged("LightMode"); }
        }
        
        BlendModes blendMode_;
        public BlendModes BlendMode {
            get { return blendMode_; }
            set { blendMode_ = value; OnPropertyChanged("BlendMode"); }
        }

        DepthTest depthTest_;
        public DepthTest DepthTest {
            get { return depthTest_; }
            set { depthTest_ = value; OnPropertyChanged("DepthTest"); }
        }
        bool depthWrite_;
        public bool DepthWrite {
            get { return depthWrite_; }
            set { depthWrite_ = value; OnPropertyChanged("DepthWrite"); }
        }

        bool alphaMask_;
        public bool AlphaMask {
            get { return alphaMask_; }
            set { alphaMask_ = value; OnPropertyChanged("AlphaMask"); }
        }

        string vs_;
        string ps_;
        [Resource(typeof(Shader))]
        public string VS { get { return vs_; } set { vs_ = value; OnPropertyChanged("VS"); } }
        [Resource(typeof(Shader))]
        public string PS { get { return ps_; } set { ps_ = value; OnPropertyChanged("PS"); } }

        Shader vertexShader_;
        public Shader VertexShader {
            get { return vertexShader_; }
            set { vertexShader_ = value; OnPropertyChanged("VertexShader"); }
        }

        Shader pixelShader_;
        public Shader PixelShader {
            get { return pixelShader_; }
            set { pixelShader_ = value; OnPropertyChanged("PixelShader"); }
        }

        bool? sm3_;
        bool? desktop_;
        ObservableCollection<string> vsdefines_;
        ObservableCollection<string> psdefines_;

        public bool? IsDesktop { get { return desktop_; } set { desktop_ = value; OnPropertyChanged("IsDesktop"); } }
        public bool? IsSM3 { get { return sm3_; } set { sm3_ = value; OnPropertyChanged("IsSM3"); } }
        public ObservableCollection<string> VSDefines { get { return vsdefines_; } }
        public ObservableCollection<string> PSDefines { get { return psdefines_; } }

        public Pass() {
            vsdefines_ = new ObservableCollection<string>();
            psdefines_ = new ObservableCollection<string>();
        }

        public void Parse(XmlElement elem) {
            Name = elem.GetAttribute("name");
            if (elem.HasAttribute("ps"))
                PS = elem.GetAttribute("ps");
            if (elem.HasAttribute("vs"))
                VS = elem.GetAttribute("vs");
            if (elem.HasAttribute("vsdefines")) {
                string[] defs = elem.GetAttribute("vsdefines").Split(' ');
                foreach (string str in defs)
                    VSDefines.Add(str);
            }
            if (elem.HasAttribute("psdefines")) {
                string[] defs = elem.GetAttribute("psdefines").Split(' ');
                foreach (string str in defs)
                    PSDefines.Add(str);
            }
            if (elem.HasAttribute("sm3"))
                IsSM3 = elem.GetAttribute("sm3").Equals("true");
            if (elem.HasAttribute("desktop"))
                IsDesktop = elem.GetAttribute("desktop").Equals("true");

            if (elem.HasAttribute("lighting")) {
                string s = elem.GetAttribute("depthtest");
                foreach (LightModes dt in Enum.GetValues(typeof(LightModes))) {
                    if (dt.ToString().ToLower().Equals(s)) {
                        LightMode = dt;
                        break;
                    }
                }
            }
            if (elem.HasAttribute("blend")) {
                string s = elem.GetAttribute("depthtest");
                foreach (BlendModes dt in Enum.GetValues(typeof(BlendModes))) {
                    if (dt.ToString().ToLower().Equals(s)) {
                        BlendMode = dt;
                        break;
                    }
                }
            }
            if (elem.HasAttribute("depthtest")) {
                string s = elem.GetAttribute("depthtest");
                foreach (DepthTest dt in Enum.GetValues(typeof(DepthTest))) {
                    if (dt.ToString().ToLower().Equals(s)) {
                        DepthTest = dt;
                        break;
                    }
                }
            }
            if (elem.HasAttribute("depthwrite"))
                DepthWrite = elem.GetAttribute("depthwrite").Equals("true");
            if (elem.HasAttribute("alphamask"))
                AlphaMask = elem.GetAttribute("alphamask").Equals("true");
        }

        public XmlElement Save(XmlDocument doc) {
            XmlElement me = doc.CreateElement("pass");
            me.SetAttribute("name", Name);
            if (IsSM3.HasValue)
                me.SetAttribute("sm3", IsSM3.Value ? "true" : "false");
            if (IsDesktop.HasValue)
                me.SetAttribute("desktop", IsDesktop.Value ? "true" : "false");
            if (VS != null && VS.Length > 0)
                me.SetAttribute("vs", VS);
            if (PS != null && PS.Length > 0)
                me.SetAttribute("ps", PS);
            if (VSDefines.Count > 0)
                me.SetAttribute("vsdefines", string.Join(" ", VSDefines.ToArray()));
            if (PSDefines.Count > 0)
                me.SetAttribute("psdefines", string.Join(" ", PSDefines.ToArray()));

            me.SetAttribute("lighting", LightMode.ToString().ToLower());
            me.SetAttribute("blend", BlendMode.ToString().ToLower());
            me.SetAttribute("depthtest", DepthTest.ToString().ToLower());
            me.SetAttribute("depthwrite", DepthWrite ? "true" : "false");
            me.SetAttribute("alphamask", AlphaMask ? "true" : "false");

            return me;
        }
    }
}
