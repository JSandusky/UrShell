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
    class SoundListener;
}



namespace UrhoBackend {


    public ref class SoundListener : public UrhoBackend::Component {
    public:
        SoundListener(Urho3D::SoundListener* comp);
        SoundListener(System::IntPtr^ ptr);

// Properties
// Methods
// Fields

        Urho3D::SoundListener* soundlistener_;
    };
}