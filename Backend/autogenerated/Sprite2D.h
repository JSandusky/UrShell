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
    class Sprite2D;
    class Texture2D;
    class IntRect;
    class Vector2;
    class IntVector2;
}

#include "Resource.h"

namespace UrhoBackend {

    ref class Texture2D;

    public ref class Sprite2D : public UrhoBackend::Resource {
    public:
        Sprite2D(Urho3D::Sprite2D* comp);
        Sprite2D(System::IntPtr^ ptr);

// Properties
        property Texture2D^ texture { Texture2D^ get(); void set(Texture2D^); }
        property UrhoBackend::IntRect^ rectangle { UrhoBackend::IntRect^ get(); void set(UrhoBackend::IntRect^); }
        property UrhoBackend::Vector2^ hotSpot { UrhoBackend::Vector2^ get(); void set(UrhoBackend::Vector2^); }
        property UrhoBackend::IntVector2^ offset { UrhoBackend::IntVector2^ get(); void set(UrhoBackend::IntVector2^); }
// Methods
// Fields

        Urho3D::Sprite2D* sprite2d_;
    };
}