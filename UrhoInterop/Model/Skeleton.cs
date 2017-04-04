using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;

namespace UrhoInterop.Model {
    public class Bone
    {
        public static int BONECOLLISION_SPHERE = 1;
        public static int BONECOLLISION_BOX = 2;

        public string Name;
        public uint Parent;
        public Vector3 Position;
        public Vector4 Rotation;
        public Vector3 Scale;

        public byte ColMask;
        public float ColRadius;
        public Vector3 BoundsMin;
        public Vector3 BoundsMax;
    }

    public class Skeleton {
        public List<Bone> Bones { get; private set; }

        public Skeleton()
        {
            Bones = new List<Bone>();
        }
    }
}
