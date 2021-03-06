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
    class ConstraintRevolute2D;
    class Vector2;
}

#include "Constraint2D.h"

namespace UrhoBackend {


    public ref class ConstraintRevolute2D : public UrhoBackend::Constraint2D {
    public:
        ConstraintRevolute2D(Urho3D::ConstraintRevolute2D* comp);
        ConstraintRevolute2D(System::IntPtr^ ptr);

// Properties
        property UrhoBackend::Vector2^ anchor { UrhoBackend::Vector2^ get(); void set(UrhoBackend::Vector2^); }
        property bool enableLimit { bool get(); void set(bool); }
        property float lowerAngle { float get(); void set(float); }
        property float upperAngle { float get(); void set(float); }
        property bool enableMotor { bool get(); void set(bool); }
        property float motorSpeed { float get(); void set(float); }
        property float maxMotorTorque { float get(); void set(float); }
// Methods
// Fields

        Urho3D::ConstraintRevolute2D* constraintrevolute2d_;
    };
}
