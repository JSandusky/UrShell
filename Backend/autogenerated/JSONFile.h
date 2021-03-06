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
    class JSONFile;
    class JSONValue;
}

#include "Resource.h"

namespace UrhoBackend {

    ref class JSONValue;

    public ref class JSONFile : public UrhoBackend::Resource {
    public:
        JSONFile(Urho3D::JSONFile* comp);
        JSONFile(System::IntPtr^ ptr);

// Properties
// Methods
        JSONValue^ CreateRoot(JSONValueType);
        JSONValue^ GetRoot(JSONValueType);
// Fields

        Urho3D::JSONFile* jsonfile_;
    };
}
