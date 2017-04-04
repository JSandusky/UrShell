using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrhoInterop.Model {
    public class IndexBuffer {
        public uint[] IndexData { get; set; }

        public IndexBuffer(uint[] buffer)
        {
            IndexData = buffer;
        }

        public IndexBuffer(byte[] bytes)
        {
            IndexData = bytes.Select(b => (uint)Convert.ToUInt32(b)).ToArray();
        }
    }
}
