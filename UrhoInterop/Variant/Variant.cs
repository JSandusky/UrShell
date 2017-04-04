using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrhoInterop.Variant {

    public enum VariantType {
        VAR_NONE,
        VAR_INT,
        VAR_BOOL,
        VAR_FLOAT,
        VAR_VECTOR2,
        VAR_VECTOR3,
        VAR_VECTOR4,
        VAR_QUATERNION,
        VAR_COLOR,
        VAR_STRING,
        VAR_BUFFER,
        VAR_VOIDPTR,
        VAR_RESOURCEREF,
        VAR_RESOURCEREFLIST,
        VAR_VARIANTVECTOR,
        VAR_VARIANTMAP,
        VAR_INTRECT,
        VAR_INTVECTOR2,
        VAR_PTR,
        VAR_MATRIX3,
        VAR_MATRIX3X4,
        VAR_MATRIX4,
        MAX_VAR_TYPES
    }

    
    public class Variant {
        public VariantType Type { get; set; }

    }
}
