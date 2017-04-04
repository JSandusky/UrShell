using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrhoInterop.Model {
    public class VertexBuffer {
        public int VertexSize { get; set; }
        public float[] VertexData { get; set; }

        public VertexBuffer(float[] buffer, int vertexSize)
        {
            VertexData = buffer;
            VertexSize = vertexSize;
        }

        public VertexBuffer(byte[] bytes, int vertexSize)
        {
            VertexData = bytes.Select(b => (float)Convert.ToSingle(b)).ToArray();
            VertexSize = vertexSize;
        }
    }
}
