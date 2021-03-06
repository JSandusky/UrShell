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
    class ParticleEmitter2D;
    class ParticleEffect2D;
    class Sprite2D;
}

#include "Drawable2D.h"

namespace UrhoBackend {

    ref class ParticleEffect2D;
    ref class Sprite2D;

    public ref class ParticleEmitter2D : public UrhoBackend::Drawable2D {
    public:
        ParticleEmitter2D(Urho3D::ParticleEmitter2D* comp);
        ParticleEmitter2D(System::IntPtr^ ptr);

// Properties
        property ParticleEffect2D^ effect { ParticleEffect2D^ get(); void set(ParticleEffect2D^); }
        property Sprite2D^ sprite { Sprite2D^ get(); void set(Sprite2D^); }
        property BlendMode blendMode { BlendMode get(); void set(BlendMode); }
// Methods
// Fields

        Urho3D::ParticleEmitter2D* particleemitter2d_;
    };
}
