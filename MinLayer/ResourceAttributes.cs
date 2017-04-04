using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UrhoBackend
{
    /// <summary>
    /// Attribute used to indicate the type of resource that is acceptable for a ResourceRef
    /// used for UI purposes to limit selection options
    /// 
    /// May use an array to specify multiple resource types (ie. Texture2D/TextureCube/etc)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method)]
    public class UrhoResource : Attribute
    {
        public UrhoResource(string resType) { ResourceTypes = new string[] { resType }; }
        public UrhoResource(params string[] resTypes) { ResourceTypes = resTypes; }
        public string[] ResourceTypes { get; private set; }
    }
}
