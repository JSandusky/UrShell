using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;

namespace UrhoInterop.Model {
    public class Model {
        public List<VertexBuffer> VertexBuffers { get; private set; }
        public List<IndexBuffer> IndexBuffers { get; private set; }
        public Skeleton Skeleton { get; set; }
        public List<Geometry> Geometries { get; private set; }
        public Vector3 BoundsMin { get; set; }
        public Vector3 BoundsMax { get; set; }

        public Model()
        {
            VertexBuffers = new List<VertexBuffer>();
            IndexBuffers = new List<IndexBuffer>();
            Geometries = new List<Geometry>();
        }
    }
}
