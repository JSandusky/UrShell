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
    class Technique;
    class Pass;
    class String;
}

#include "Resource.h"

namespace UrhoBackend {

    ref class Pass;

    public ref class Technique : public UrhoBackend::Resource {
    public:
        Technique(Urho3D::Technique* comp);
        Technique(System::IntPtr^ ptr);

// Properties
        property bool supported { bool get(); }
        property bool desktop { bool get(); void set(bool); }
        property unsigned numPasses { unsigned get(); }
// Methods
        Pass^ CreatePass(System::String^);
        void RemovePass(System::String^);
        bool HasPass(System::String^);
        Pass^ GetPass(System::String^);
        Pass^ GetSupportedPass(System::String^);
// Fields

        Urho3D::Technique* technique_;
    };
}
