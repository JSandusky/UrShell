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
    class Text;
    class String;
    class Font;
    class Color;
    class IntVector2;
}



namespace UrhoBackend {

    ref class Font;

    public ref class Text : public UrhoBackend::UIElement {
    public:
        Text(Urho3D::Text* comp);
        Text(System::IntPtr^ ptr);

// Properties
        property Font^ font { Font^ get(); }
        property int fontSize { int get(); }
        property System::String^ text { System::String^ get(); void set(System::String^); }
        property HorizontalAlignment textAlignment { HorizontalAlignment get(); void set(HorizontalAlignment); }
        property float rowSpacing { float get(); void set(float); }
        property bool wordwrap { bool get(); void set(bool); }
        property unsigned selectionStart { unsigned get(); }
        property unsigned selectionLength { unsigned get(); }
        property UrhoBackend::Color^ selectionColor { UrhoBackend::Color^ get(); void set(UrhoBackend::Color^); }
        property UrhoBackend::Color^ hoverColor { UrhoBackend::Color^ get(); void set(UrhoBackend::Color^); }
        property TextEffect textEffect { TextEffect get(); void set(TextEffect); }
        property UrhoBackend::Color^ effectColor { UrhoBackend::Color^ get(); void set(UrhoBackend::Color^); }
        property unsigned numRows { unsigned get(); }
        property unsigned numChars { unsigned get(); }
        property int rowWidths[unsigned] { int get(unsigned); }
        property UrhoBackend::IntVector2^ charPositions[unsigned] { UrhoBackend::IntVector2^ get(unsigned); }
        property UrhoBackend::IntVector2^ charSizes[unsigned] { UrhoBackend::IntVector2^ get(unsigned); }
        property int rowHeight { int get(); }
// Methods
        bool SetFont(System::String^, int);
        bool SetFont(Font^, int);
        void SetSelection(unsigned, unsigned);
        void ClearSelection();
// Fields

        Urho3D::Text* text_;
    };
}
