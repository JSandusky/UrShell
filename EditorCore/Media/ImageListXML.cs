using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace EditorCore.Media
{
    /// <summary>
    /// Utility method for loading an XML description of the images, either loose, or from a composite
    /// via the Urho3D skin style
    /// 
    /// The assumption is made with Urho3D style that all children of an <elements/> tag will use the
    /// same image file, that file must additionally be specified in the <elements/> tag
    /// </summary>
    public static class ImageListXML
    {
        public static void Fill(NamedImageList target, XmlElement xmlRoot)
        {
            // Parse an urho3d style declaration
            string imgFile = xmlRoot.GetAttribute("file");
            ImageCropper cropper = new ImageCropper(new System.Drawing.Bitmap(imgFile));
            foreach (XmlElement elem in xmlRoot.GetElementsByTagName("element"))
            {
                string name = elem.GetAttribute("type");
                Rectangle r = new Rectangle();
                foreach (XmlElement attr in elem.GetElementsByTagName("attribute"))
                {
                    try
                    {
                        if (attr.GetAttribute("name").Equals("Image Rect"))
                        {
                            string[] parts = attr.GetAttribute("value").Split(' ');
                            r.X = int.Parse(parts[0]);
                            r.Y = int.Parse(parts[1]);
                            r.Width = int.Parse(parts[2]) - r.X;
                            r.Height = int.Parse(parts[3]) - r.Y;
                            target.AddImage(name, cropper.Get(r));
                        }
                    }
                    catch (Exception) { }
                }
            }
        }
    }
}
