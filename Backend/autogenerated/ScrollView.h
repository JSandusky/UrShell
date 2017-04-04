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
    class ScrollView;
    class UIElement;
    class IntVector2;
    class ScrollBar;
    class BorderImage;
}



namespace UrhoBackend {

    ref class UIElement;
    ref class ScrollBar;
    ref class BorderImage;

    public ref class ScrollView : public UrhoBackend::UIElement {
    public:
        ScrollView(Urho3D::ScrollView* comp);
        ScrollView(System::IntPtr^ ptr);

// Properties
        property UIElement^ contentElement { UIElement^ get(); void set(UIElement^); }
        property UrhoBackend::IntVector2^ viewPosition { UrhoBackend::IntVector2^ get(); void set(UrhoBackend::IntVector2^); }
        property float scrollStep { float get(); void set(float); }
        property float pageStep { float get(); void set(float); }
        property ScrollBar^ horizontalScrollBar { ScrollBar^ get(); }
        property ScrollBar^ verticalScrollBar { ScrollBar^ get(); }
        property BorderImage^ scrollPanel { BorderImage^ get(); }
        property bool scrollBarsAutoVisible { bool get(); void set(bool); }
        property float scrollDeceleration { float get(); void set(float); }
        property float scrollSnapEpsilon { float get(); void set(float); }
        property bool autoDisableChildren { bool get(); void set(bool); }
        property float autoDisableThreshold { float get(); void set(float); }
// Methods
        void SetViewPosition(int, int);
        void SetScrollBarsVisible(bool, bool);
// Fields

        Urho3D::ScrollView* scrollview_;
    };
}