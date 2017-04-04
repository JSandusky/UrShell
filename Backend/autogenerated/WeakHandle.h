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
    class WeakHandle;
    class RefCounted;
}

namespace UrhoBackend {

    ref class RefCounted;

    public ref class WeakHandle {
    public:
        WeakHandle(Urho3D::WeakHandle comp);
        WeakHandle(System::IntPtr^ ptr);
        ~WeakHandle();

// Properties
        property int refs { int get(); }
        property int weakRefs { int get(); }
        property bool expired { bool get(); }
// Methods
        WeakPtr<RefCounted> opAssign(WeakHandle^);
        WeakPtr<RefCounted> opAssign(RefCounted^);
        RefCounted^ Get();
// Fields

        Urho3D::WeakHandle* weakhandle_;
    };
}