using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginLibrary
{
    /// <summary>
    /// Interface for implementing an external audio player
    /// </summary>
    public interface IAudioPlayer
    {
        void SetFile(string file);
        void Play();
        void Pause();
        float Length { get; }
        float Time { get; set; }
    }
}
