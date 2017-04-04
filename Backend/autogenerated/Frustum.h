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
    class Frustum;
    class Matrix3x4;
    class Vector3;
    class BoundingBox;
    class Matrix3;
    class Sphere;
}

#include <Urho3D/Math/Frustum.h>

namespace UrhoBackend {

    ref class BoundingBox;
    ref class Sphere;

    public ref class Frustum {
    public:
        Frustum(Urho3D::Frustum comp);
        Frustum(System::IntPtr^ ptr);
        ~Frustum();

// Properties
// Methods
        Frustum^ opAssign(Frustum^);
        void Define(float, float, float, float, float, UrhoBackend::Matrix3x4^);
        void Define(UrhoBackend::Vector3^, UrhoBackend::Vector3^, UrhoBackend::Matrix3x4^);
        void Define(BoundingBox^, UrhoBackend::Matrix3x4^);
        void DefineOrtho(float, float, float, float, float, UrhoBackend::Matrix3x4^);
        void Transform(UrhoBackend::Matrix3^);
        void Transform(UrhoBackend::Matrix3x4^);
        Intersection IsInside(UrhoBackend::Vector3^);
        Intersection IsInside(BoundingBox^);
        Intersection IsInside(Sphere^);
        float Distance(UrhoBackend::Vector3^);
        Frustum^ Transformed(UrhoBackend::Matrix3^);
        Frustum^ Transformed(UrhoBackend::Matrix3x4^);
// Fields

        Urho3D::Frustum* frustum_;
    };
}