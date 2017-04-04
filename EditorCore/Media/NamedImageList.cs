using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace EditorCore
{
    /// <summary>
    /// Manages an application master image list, which contains all of the images that are intended
    /// to be used in controls
    /// </summary>
    public sealed class NamedImageList
    {
        ImageList imageList_;
        List<string> names_ = new List<string>();

        public NamedImageList()
        {
            imageList_ = new ImageList();
        }

        public void Load(string xmlFile)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFile);
            Media.ImageListXML.Fill(this, doc.DocumentElement);
        }

        public void AddImage(string name, System.Drawing.Image img)
        {
            if (names_.Contains(name))
                throw new Exception(String.Format("Illegal argument: ", name));
            names_.Add(name);
            imageList_.Images.Add(img);
        }

        public int IndexOf(string name)
        {
            return names_.IndexOf(name);
        }

        public System.Drawing.Image GetImage(string name)
        {
            if (!names_.Contains(name))
                return null;
            return imageList_.Images[IndexOf(name)];
        }

        public ImageList GetImageList() { return imageList_; }
    }
}
