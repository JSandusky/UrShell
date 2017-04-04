using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;

namespace UrhoInterop.Model {

    public class Keyframe
    {
        public float Time { get; set; }
        public Vector3 Pos { get; set; }
        public Vector4 Rot { get; set; }
        public Vector3 Scl { get; set; }
    }

    public class Track
    {
        public string Name { get; set; }
        public byte Mask { get; set; }
        public List<Keyframe> Keys { get; private set; }

        public Track()
        {
            Keys = new List<Keyframe>();
        }
    }

    public class Animation {
        public string Name { get; set; }
        public float Length { get; set; }
        public List<Track> Tracks { get; private set; }

        public Animation()
        {
            Tracks = new List<Track>();
        }
    }
}
