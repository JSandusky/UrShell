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
    class CheckBox;
    class Texture;
    class IntRect;
    class IntVector2;
}



namespace UrhoBackend {

    ref class Texture;

    public ref class CheckBox : public UrhoBackend::UIElement {
    public:
        CheckBox(Urho3D::CheckBox* comp);
        CheckBox(System::IntPtr^ ptr);

// Properties
        property Texture^ texture { Texture^ get(); void set(Texture^); }
        property UrhoBackend::IntRect^ imageRect { UrhoBackend::IntRect^ get(); void set(UrhoBackend::IntRect^); }
        property UrhoBackend::IntRect^ border { UrhoBackend::IntRect^ get(); void set(UrhoBackend::IntRect^); }
        property UrhoBackend::IntRect^ imageBorder { UrhoBackend::IntRect^ get(); void set(UrhoBackend::IntRect^); }
        property UrhoBackend::IntVector2^ hoverOffset { UrhoBackend::IntVector2^ get(); void set(UrhoBackend::IntVector2^); }
        property BlendMode blendMode { BlendMode get(); void set(BlendMode); }
        property bool tiled { bool get(); void set(bool); }
        property bool checked { bool get(); void set(bool); }
        property UrhoBackend::IntVector2^ checkedOffset { UrhoBackend::IntVector2^ get(); void set(UrhoBackend::IntVector2^); }
// Methods
        void SetFullImageRect();
        void SetHoverOffset(int, int);
        void SetCheckedOffset(int, int);
// Fields

        Urho3D::CheckBox* checkbox_;
    };
}