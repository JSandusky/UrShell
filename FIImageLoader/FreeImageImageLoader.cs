using FreeImageAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIImageLoader
{
    public class FreeImageImageLoader : PluginLibrary.IImageLoader
    {
        public System.Drawing.Image LoadImage(string file)
        {
            try
            {
                FIBITMAP image = FreeImage.LoadEx(file);
                int width = (int)FreeImage.GetWidth(image);
                int height = (int)FreeImage.GetHeight(image);
                int fileBPP = (int)FreeImage.GetBPP(image);
                IntPtr ptr = FreeImage.GetHbitmap(image, new IntPtr(0), false); //obtain the Hbitmap
                return System.Drawing.Image.FromHbitmap(ptr);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
