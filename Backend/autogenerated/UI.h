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
    class UI;
    class UIElement;
    class IntVector2;
    class Cursor;
    class String;
}



namespace UrhoBackend {

    ref class UIElement;
    ref class Cursor;

    public ref class UI : public UrhoBackend::Object {
    public:
        UI(Urho3D::UI* comp);
        UI(System::IntPtr^ ptr);

// Properties
        property Cursor^ cursor { Cursor^ get(); void set(Cursor^); }
        property UrhoBackend::IntVector2^ cursorPosition { UrhoBackend::IntVector2^ get(); }
        property UIElement^ focusElement { UIElement^ get(); }
        property UIElement^ frontElement { UIElement^ get(); }
        property UIElement^ root { UIElement^ get(); }
        property UIElement^ modalRoot { UIElement^ get(); }
        property System::String^ clipBoardText { System::String^ get(); void set(System::String^); }
        property float doubleClickInterval { float get(); void set(float); }
        property float dragBeginInterval { float get(); void set(float); }
        property int dragBeginDistance { int get(); void set(int); }
        property float defaultToolTipDelay { float get(); void set(float); }
        property int maxFontTextureSize { int get(); void set(int); }
        property bool nonFocusedMouseWheel { bool get(); void set(bool); }
        property bool useSystemClipboard { bool get(); void set(bool); }
        property bool useScreenKeyboard { bool get(); void set(bool); }
        property bool useMutableGlyphs { bool get(); void set(bool); }
        property bool forceAutoHint { bool get(); void set(bool); }
// Methods
        void Clear();
        void DebugDraw(UIElement^);
        void SetFocusElement(UIElement^, bool);
        UIElement^ GetElementAt(UrhoBackend::IntVector2^, bool);
        UIElement^ GetElementAt(int, int, bool);
        bool HasModalElement();
        bool IsDragging();
// Fields

        Urho3D::UI* ui_;
    };
}
