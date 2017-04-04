using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Urho;

namespace UrhoInterop {


    public enum InterpolationMethod
    {
        Linear,
        Spline
    }

    public enum AnimWrapMode
    {
        Loop,
        Once,
        Clamp
    }

    public class Keyframe : BaseClass
    {
        string type_;
        string value_;
        float time_;

        public string Type { get { return type_; } set { type_ = value; OnPropertyChanged("Type"); } }
        public string Value { get { return value_; } set { value_ = value; OnPropertyChanged("Value"); } }
        public float Time { get { return time_; } set { time_ = value; OnPropertyChanged("Time"); } }

        public Keyframe() { }
        public Keyframe(XmlElement elem)
        {
            type_ = elem.GetAttribute("type");
            value_ = elem.GetAttribute("value");
            time_ = float.Parse(elem.GetAttribute("time"));
        }
    }

    public class AttributeAnimation : BaseClass
    {
        ObservableCollection<Keyframe> frames_;
        float splineTension_;
        float speed_;
        InterpolationMethod interp_;
        AnimWrapMode wrap_;
        string name_;

        public string Name { get { return name_; } set { name_ = value; OnPropertyChanged("Name"); } }
        public float SplineTension { get { return splineTension_; } set { splineTension_ = value; } }
        public InterpolationMethod Interpolation { get { return interp_; } set { interp_ = value; } }
        public AnimWrapMode WrapMode { get { return wrap_; } set { wrap_ = value; } }
        public ObservableCollection<Keyframe> Keyframes { get { return frames_; } }
        public float Speed { get { return speed_; } set { speed_ = value; } }

        public AttributeAnimation()
        {
            frames_ = new ObservableCollection<Keyframe>();
        }

        public AttributeAnimation(XmlElement elem)
        {
            name_ = elem.GetAttribute("name");
            speed_ = float.Parse(elem.GetAttribute("speed"));
            splineTension_ = float.Parse(elem.GetAttribute("splinetension"));
            string interp = elem.GetAttribute("interpolation").ToLowerInvariant();
            string wrapmode = elem.GetAttribute("wrapmode").ToLowerInvariant();
            if (interp.Equals("linear"))
                interp_ = InterpolationMethod.Linear;
            else if (interp.Equals("linear"))
                interp_ = InterpolationMethod.Spline;
            if (wrapmode.Equals("clamp"))
                wrap_ = AnimWrapMode.Clamp;
            else if (wrapmode.Equals("loop"))
                wrap_ = AnimWrapMode.Loop;
            else if (wrapmode.Equals("once"))
                wrap_ = AnimWrapMode.Once;

            foreach (XmlElement keyelem in elem.GetElementsByTagName("keyframe"))
                Keyframes.Add(new Keyframe(keyelem));
        }
    }

    public class ObjectAnimation : BaseClass {
        ObservableCollection<AttributeAnimation> attrAnims_;

        public ObservableCollection<AttributeAnimation> AttributeAnimation { get { return attrAnims_; } }

        public ObjectAnimation()
        {
            attrAnims_ = new ObservableCollection<AttributeAnimation>();
        }

        public ObjectAnimation(string fileName) {
            attrAnims_ = new ObservableCollection<AttributeAnimation>();

            string path = System.IO.Path.ChangeExtension(fileName, "xml");
            if (System.IO.File.Exists(path))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);

                foreach (XmlElement attrAnim in doc.DocumentElement.GetElementsByTagName("attributeanimation"))
                    attrAnims_.Add(new AttributeAnimation(attrAnim));
            }
        }
    }
}
