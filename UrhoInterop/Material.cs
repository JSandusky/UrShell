using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Urho {

    public enum CullMode {
        CW,
        CCW,
        NONE,
    }

    public enum TexUnit {
        Diffuse,
        Normal,
        Specular,
        Emissive,
        Environment,
        Splat
    }

    public class MatParameter : NamedBaseClass {
        Vector4 backingVec_ = new Vector4();

        public MatParameter() {
        }

        public MatParameter(XmlElement elem) {
            Name = elem.GetAttribute("name");
            backingVec_ = Vector4.FromString(elem.GetAttribute("value"));
        }

        public XmlElement Save(XmlDocument doc) {
            XmlElement me = doc.CreateElement("parameter");
            me.SetAttribute("name", Name);
            me.SetAttribute("value", Value.ToString());
            return me;
        }

        public Vector4 Value {
            get { return backingVec_; }
            set {
                backingVec_ = value;
                OnPropertyChanged("Value");
            }
        }
    }

    [Resource(typeof(BaseTexture))]
    public class MatTexture : NamedBaseClass {
        TexUnit textureUnit_;

        public TexUnit TextureUnit {
            get { return textureUnit_; }
            set { textureUnit_ = value; OnPropertyChanged("TextureUnit"); }
        }

        public XmlElement Save(XmlDocument doc) {
            XmlElement me = doc.CreateElement("texture");
            me.SetAttribute("unit", TextureUnit.ToString().ToLower());
            me.SetAttribute("name", Name);
            return me;
        }
    }

    [Resource(typeof(Technique))]
    public class MatTechnique : NamedBaseClass {
        float quality_;
        float loddistance_;

        public float Quality { get { return quality_; } set { quality_ = value; OnPropertyChanged("Quality"); } }
        public float LODDistance { get { return loddistance_; } set { loddistance_ = value; OnPropertyChanged("LODDistance"); } }

        public MatTechnique()
        {

        }

        public MatTechnique(XmlElement tech) {
            Name = tech.GetAttribute("name");
            if (tech.HasAttribute("quality"))
                Quality = float.Parse(tech.GetAttribute("quality"));
            if (tech.HasAttribute("loddistance"))
                LODDistance = float.Parse(tech.GetAttribute("loddistance"));
        }

        public XmlElement Save(XmlDocument doc) {
            XmlElement me = doc.CreateElement("technique");
            me.SetAttribute("name", Name);
            if (Quality != 0)
                me.SetAttribute("quality", Quality.ToString());
            if (LODDistance != 0)
                me.SetAttribute("loddistance", LODDistance.ToString());
            return me;
        }
    }

    [ResourceType(new string[] {"Material"})]
    public class Material : NamedBaseClass {
        CullMode shadowCull_;
        public CullMode ShadowCull {
            get { return shadowCull_; }
            set { shadowCull_ = value; }
        }

        CullMode culling_;
        public CullMode Culling {
            get { return culling_; }
            set { culling_ = value; }
        }

        float depthBiasConst_;
        public float DepthBiasConst {
            get { return depthBiasConst_; }
            set { depthBiasConst_ = value; }
        }

        float depthBiasSlope_;
        public float DepthBiasSlope {
            get { return depthBiasSlope_; }
            set { depthBiasSlope_ = value; }
        }

        ObservableCollection<MatParameter> params_;
        public ObservableCollection<MatParameter> Params {
            get { return params_; }
        }

        ObservableCollection<MatTexture> textures_;
        public ObservableCollection<MatTexture> Textures {
            get { return textures_; }
        }

        ObservableCollection<MatTechnique> techniques_;
        public ObservableCollection<MatTechnique> Techniques { get { return techniques_; } }

        public Material(string fileName) {
            Name = fileName;
            params_ = new ObservableCollection<MatParameter>();
            textures_ = new ObservableCollection<MatTexture>();
            techniques_ = new ObservableCollection<MatTechnique>();
            //setup other defaults

            string path = System.IO.Path.ChangeExtension(fileName, "xml");
            if (System.IO.File.Exists(path)) {
                XmlDocument doc = new XmlDocument();
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    doc.Load(fileStream);
                //doc.Load(path);

                foreach (XmlElement tech in doc.DocumentElement.GetElementsByTagName("technique"))
                    Techniques.Add(new MatTechnique(tech));
                foreach (XmlElement tex in doc.DocumentElement.GetElementsByTagName("texture")) {
                    string unit = tex.GetAttribute("unit");
                    MatTexture t = new MatTexture();
                    t.Name = tex.GetAttribute("name");
                    foreach (TexUnit u in Enum.GetValues(typeof(TexUnit))) {
                        if (u.ToString().ToLower().Equals(unit)) {
                            t.TextureUnit = u;
                            break;
                        }
                    }
                    Textures.Add(t);
                }
                foreach (XmlElement param in doc.DocumentElement.GetElementsByTagName("parameter"))
                    Params.Add(new MatParameter(param));
                foreach (XmlElement cull in doc.GetElementsByTagName("cull")) {
                    if (cull.HasAttribute("value")) {
                        if (cull.GetAttribute("value").Equals("cw"))
                            Culling = CullMode.CW;
                        else if (cull.GetAttribute("value").Equals("ccw"))
                            Culling = CullMode.CCW;
                        else
                            Culling = CullMode.NONE;
                    }
                }
                foreach (XmlElement cull in doc.GetElementsByTagName("shadowcull")) {
                    if (cull.HasAttribute("value")) {
                        if (cull.GetAttribute("value").Equals("cw"))
                            ShadowCull = CullMode.CW;
                        else if (cull.GetAttribute("value").Equals("ccw"))
                            ShadowCull = CullMode.CCW;
                        else
                            ShadowCull = CullMode.NONE;
                    }
                }
                foreach (XmlElement cull in doc.GetElementsByTagName("depthbias")) {
                    if (cull.HasAttribute("constant"))
                        DepthBiasConst = float.Parse(cull.GetAttribute("constant"));
                    if (cull.HasAttribute("slopescaled"))
                        DepthBiasSlope = float.Parse(cull.GetAttribute("slopescaled"));
                }
            }
        }

        public override void Save() {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("material");
            doc.AppendChild(root);

            //TECHNIQUES
            foreach (MatTechnique tech in Techniques) {
                root.AppendChild(tech.Save(doc));
            }
            foreach (MatTexture tex in Textures)
                root.AppendChild(tex.Save(doc));
            foreach (MatParameter param in Params)
                root.AppendChild(param.Save(doc));
            XmlElement cull = doc.CreateElement("cull");
            cull.SetAttribute("value",Culling.ToString().ToLower());
            root.AppendChild(cull);

            XmlElement scull = doc.CreateElement("shadowcull");
            scull.SetAttribute("value", ShadowCull.ToString().ToLower());
            root.AppendChild(scull);

            XmlElement dbias = doc.CreateElement("depthbias");
            dbias.SetAttribute("constant", DepthBiasConst.ToString());
            dbias.SetAttribute("slopedscaled", DepthBiasSlope.ToString());

            XmlWriterSettings xws = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true };
            using (XmlWriter xw = XmlWriter.Create(System.IO.Path.ChangeExtension(Name, "xml"), xws))
                doc.Save(xw);
        }
    }
}
