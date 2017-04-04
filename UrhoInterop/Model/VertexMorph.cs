using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;

namespace UrhoInterop.Model {

    public class MorphVertex
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector3 Tangent;
    }

    public class MorphAffectedBuffer
    {
        public uint VertCt;
        public uint Mask;
        public uint Idx;
        public VertexBuffer Buffer;
        public List<MorphVertex> Vertices { get; private set; }

        public MorphAffectedBuffer()
        {
            Vertices = new List<MorphVertex>();
        }
    }

    public class VertexMorph {
        public string Name { get; set; }
        public List<MorphAffectedBuffer> Buffers { get; private set; }

        public VertexMorph()
        {
            Buffers = new List<MorphAffectedBuffer>();
        }
    }
}
