using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginLibrary
{
    /// <summary>
    /// Interface for implementing an external image loader
    /// </summary>
    public interface IImageLoader
    {
        System.Drawing.Image LoadImage(string file);
    }
}
