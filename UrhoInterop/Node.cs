using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Urho {
    public class Node : BaseClass {
        int id_;
        ObservableCollection<Attribute> attributes_;
        ObservableCollection<Node> childNodes_;
        ObservableCollection<Component> components_;

        public int ID { get { return id_; } set { id_ = value; OnPropertyChanged("ID"); } }
        public ObservableCollection<Attribute> Attributes { get { return attributes_; } }
        public ObservableCollection<Node> Children { get { return childNodes_; } }
        public ObservableCollection<Component> Components { get { return components_; } }


        public Node() {
            attributes_ = new ObservableCollection<Attribute>();
            childNodes_ = new ObservableCollection<Node>();
            components_ = new ObservableCollection<Component>();
        }

        public static Node FromXml(XmlElement elem) {
            Node ret = new Node();
            ret.LoadXML(elem);
            return ret;
        }

        public void LoadXML(XmlElement elem) {
            ID = int.Parse(elem.GetAttribute("id"));
            foreach (XmlElement e in elem.ChildNodes) {
                if (e.Name.Equals("attribute")) {
                    Attributes.Add(Attribute.FromXml(e));
                } else if (e.Name.Equals("component")) {
                    Components.Add(Component.FromXml(e));
                } else if (e.Name.Equals("node")) {
                    Children.Add(Node.FromXml(e));
                }
            }
        }

        public virtual XmlElement SaveTo(XmlDocument aDoc) {
            XmlElement r = aDoc.CreateElement("node");
            WriteInto(r, aDoc);
            return r;
        }

        protected void WriteInto(XmlElement aElem, XmlDocument aDoc) {
            aElem.SetAttribute("id", ID.ToString());
            foreach (Attribute attr in Attributes)
                aElem.AppendChild(attr.SaveTo(aDoc));
            foreach (Component comp in Components)
                aElem.AppendChild(comp.SaveTo(aDoc));
            foreach (Node child in Children)
                aElem.AppendChild(child.SaveTo(aDoc));
        }
    }
}
