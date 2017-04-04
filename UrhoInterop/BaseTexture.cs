using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Urho {

    [ResourceType(new String[] { "Texture2D", "Texture3D", "TextureCube" })]
    public abstract class BaseTexture : NamedBaseClass {

        public abstract bool IsCube { get; }
        public bool IsHardFile { get; set; }
    }
}
