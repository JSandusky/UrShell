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
    class Resource;
    class String;
}



namespace UrhoBackend {


    public ref class Resource : public UrhoBackend::UObject {
    public:
        Resource(Urho3D::Resource* comp);
        Resource(System::IntPtr^ ptr);

// Properties
        property System::String^ name { System::String^ get(); void set(System::String^); }
        property unsigned memoryUse { unsigned get(); }
        property unsigned useTimer { unsigned get(); }
// Methods
// Fields

        Urho3D::Resource* resource_;
    };
}
