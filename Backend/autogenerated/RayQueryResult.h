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
    class RayQueryResult;
    class Vector3;
}

#include <Urho3D/Graphics/OctreeQuery.h>

namespace UrhoBackend {


    public ref class RayQueryResult {
    public:
        RayQueryResult(Urho3D::RayQueryResult comp);
        RayQueryResult(System::IntPtr^ ptr);
        ~RayQueryResult();

// Properties
// Methods
// Fields
        property UrhoBackend::Vector3^ position { UrhoBackend::Vector3^ get(); void set(UrhoBackend::Vector3^ value); }
        property UrhoBackend::Vector3^ normal { UrhoBackend::Vector3^ get(); void set(UrhoBackend::Vector3^ value); }
        property float distance { float get(); void set(float value); }
        property unsigned subObject { unsigned get(); void set(unsigned value); }

        Urho3D::RayQueryResult* rayqueryresult_;
    };
}
