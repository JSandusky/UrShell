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

#include "Enumerations.h"

namespace Urho3D {
    class BiasParameters;
}

#include <Urho3D/Graphics/Light.h>

namespace UrhoBackend {


    public ref class BiasParameters {
    public:
        BiasParameters(Urho3D::BiasParameters comp);
        BiasParameters(System::IntPtr^ ptr);
        ~BiasParameters();

// Properties
// Methods
// Fields
        property float constantBias { float get(); void set(float value); }
        property float slopeScaledBias { float get(); void set(float value); }

        Urho3D::BiasParameters* biasparameters_;
    };
}
