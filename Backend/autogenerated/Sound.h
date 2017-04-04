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
    class Sound;
}

#include "Resource.h"

namespace UrhoBackend {


    public ref class Sound : public UrhoBackend::Resource {
    public:
        Sound(Urho3D::Sound* comp);
        Sound(System::IntPtr^ ptr);

// Properties
        property float length { float get(); }
        property unsigned sampleSize { unsigned get(); }
        property float frequency { float get(); }
        property bool looped { bool get(); void set(bool); }
        property bool sixteenBit { bool get(); }
        property bool stereo { bool get(); }
        property bool compressed { bool get(); }
// Methods
// Fields

        Urho3D::Sound* sound_;
    };
}