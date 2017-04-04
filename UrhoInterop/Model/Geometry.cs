using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;

namespace UrhoInterop.Model {
    public class LOD {
        public VertexBuffer VertexBuffer { get; set; }
        public IndexBuffer IndexBuffer { get; set; }
        public uint IndexStart;
        public uint IndexEnd;
        public float Distance;
        public uint Primitive;
    }

    public class Geometry
    {
        public List<LOD> LODs { get; private set; }
        public uint[] BoneMapping { get; set; }

        public Vector3 Center;

        public Geometry()
        {
            LODs = new List<LOD>();
        }
    }
}
