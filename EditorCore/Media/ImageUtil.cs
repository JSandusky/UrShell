using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditorCore.Media
{
    public static class ImageUtil
    {
        // From user: Alex Aza
        // http://stackoverflow.com/questions/6501797/resize-image-proportionally-with-maxheight-and-maxwidth-constraints
        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            return newImage;
        }

        public static Image FromResourceFolder(string name)
        {
            string path = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            path = System.IO.Path.Combine(path, "Resources/images");
            path = System.IO.Path.Combine(path, name);
            return System.Drawing.Image.FromFile(path);
        }
    }
}
