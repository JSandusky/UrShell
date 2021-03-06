//THIS FILE IS AUTOGENERATED - DO NOT MODIFY
#pragma once

#include <Urho3D/Urho3D.h>
#include <Urho3D/Scene/Component.h>
#include "../MathBind.h"
#include "../ResourceRefWrapper.h"
#include "../SceneWrappers.h"
#include "../Variant.h"
#include "../StringHash.h"
#include "../Attributes.h"

namespace Urho3D {
    class ValueAnimation;
    class Variant;
}

namespace UrhoBackend {


    public ref class ValueAnimation : public UrhoBackend::UObject {
    public:
        ValueAnimation(Urho3D::ValueAnimation* comp);
        ValueAnimation(System::IntPtr^ ptr);

// Properties
        property InterpMethod interpolationMethod { InterpMethod get(); void set(InterpMethod); }
        property float splineTension { float get(); void set(float); }
        property VariantType valueType { VariantType get(); void set(VariantType); }
// Methods
        void SetKeyFrame(float, Variant^);
// Fields

        Urho3D::ValueAnimation* valueanimation_;
    };
}
