using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Urho {

    [ResourceType(new string[] { "Technique" })]
    public class Technique : NamedBaseClass {
        float distance_;
        public float Distance {
            get { return distance_; }
            set { distance_ = value; OnPropertyChanged("Distance"); }
        }

        float quality_;
        public float Quality {
            get { return quality_; }
            set { quality_ = value; OnPropertyChanged("Quality"); }
        }

        bool? sm3_;
        bool? desktop_;
        ObservableCollection<string> vsdefines_;
        ObservableCollection<string> psdefines_;
        string vs_;
        string ps_;

        [Resource(typeof(Shader))]
        public string VS { get { return vs_; } set { vs_ = value; OnPropertyChanged("VS"); } }
        [Resource(typeof(Shader))]
        public string PS { get { return ps_; } set { ps_ = value; OnPropertyChanged("PS"); } }

        public bool? IsDesktop { get { return desktop_; } set { desktop_ = value; OnPropertyChanged("IsDesktop"); } }
        public bool? IsSM3 { get { return sm3_; } set { sm3_ = value; OnPropertyChanged("IsSM3"); } }
        public ObservableCollection<string> VSDefines { get { return vsdefines_; } }
        public ObservableCollection<string> PSDefines { get { return psdefines_; } }

        ObservableCollection<Pass> passes_;

        public ObservableCollection<Pass> Passes {
            get { return passes_; }
        }

        public Technique()
        {
            Name = "";
            vsdefines_ = new ObservableCollection<string>();
            psdefines_ = new ObservableCollection<string>();
            passes_ = new ObservableCollection<Pass>();
        }

        public Technique(string file) {
            Name = file;
            vsdefines_ = new ObservableCollection<string>();
            psdefines_ = new ObservableCollection<string>();
            passes_ = new ObservableCollection<Pass>();

            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            XmlElement root = doc.DocumentElement;
            if (root.HasAttribute("vs"))
                VS = root.GetAttribute("vs");
            if (root.HasAttribute("ps"))
                PS = root.GetAttribute("ps");
            if (root.HasAttribute("vsdefines")) {
                string[] defs = root.GetAttribute("vsdefines").Split(' ');
                foreach (string str in defs)
                    VSDefines.Add(str);
            }
            if (root.HasAttribute("psdefines")) {
                string[] defs = root.GetAttribute("psdefines").Split(' ');
                foreach (string str in defs)
                    PSDefines.Add(str);
            }
            if (root.HasAttribute("sm3"))
                IsSM3 = root.GetAttribute("sm3").Equals("true");
            if (root.HasAttribute("desktop"))
                IsDesktop = root.GetAttribute("desktop").Equals("true");

            foreach (XmlElement e in root.ChildNodes) {
                if (e.Name.Equals("pass")) {
                    Pass p = new Pass();
                    p.Parse(e);
                    Passes.Add(p);
                }
            }
        }

        public override void Save() {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("technique");
            doc.AppendChild(root);

            if (VS != null && VS.Length > 0)
                root.SetAttribute("vs", VS);
            if (PS != null && PS.Length > 0)
                root.SetAttribute("ps", PS);
            if (VSDefines.Count > 0)
                root.SetAttribute("vsdefines", string.Join(" ", VSDefines.ToArray()));
            if (PSDefines.Count > 0)
                root.SetAttribute("psdefines", string.Join(" ", PSDefines.ToArray()));
            if (IsSM3.HasValue)
                root.SetAttribute("sm3", IsSM3.Value ? "true" : "false");
            if (IsDesktop.HasValue)
                root.SetAttribute("desktop", IsDesktop.Value ? "true" : "false");

            foreach (Pass pass in Passes)
                root.AppendChild(pass.Save(doc));

            XmlWriterSettings xws = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true };
            using (XmlWriter xw = XmlWriter.Create(System.IO.Path.ChangeExtension(Name, "xml"), xws))
                doc.Save(xw);
        }
    }
}
