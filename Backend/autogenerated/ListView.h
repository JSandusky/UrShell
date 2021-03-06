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
    class ListView;
    class UIElement;
    class IntVector2;
    class ScrollBar;
    class BorderImage;
}



namespace UrhoBackend {

    ref class UIElement;
    ref class ScrollBar;
    ref class BorderImage;

    public ref class ListView : public UrhoBackend::UIElement {
    public:
        ListView(Urho3D::ListView* comp);
        ListView(System::IntPtr^ ptr);

// Properties
        property UrhoBackend::IntVector2^ viewPosition { UrhoBackend::IntVector2^ get(); void set(UrhoBackend::IntVector2^); }
        property UIElement^ contentElement { UIElement^ get(); }
        property ScrollBar^ horizontalScrollBar { ScrollBar^ get(); }
        property ScrollBar^ verticalScrollBar { ScrollBar^ get(); }
        property BorderImage^ scrollPanel { BorderImage^ get(); }
        property bool scrollBarsAutoVisible { bool get(); void set(bool); }
        property float scrollStep { float get(); void set(float); }
        property float pageStep { float get(); void set(float); }
        property float scrollDeceleration { float get(); void set(float); }
        property float scrollSnapEpsilon { float get(); void set(float); }
        property bool autoDisableChildren { bool get(); void set(bool); }
        property float autoDisableThreshold { float get(); void set(float); }
        property unsigned numItems { unsigned get(); }
        property UIElement^ items[unsigned] { UIElement^ get(unsigned); }
        property unsigned selection { unsigned get(); void set(unsigned); }
        property UIElement^ selectedItem { UIElement^ get(); }
        property HighlightMode highlightMode { HighlightMode get(); void set(HighlightMode); }
        property bool multiselect { bool get(); void set(bool); }
        property bool hierarchyMode { bool get(); void set(bool); }
        property int baseIndent { int get(); void set(int); }
        property bool clearSelectionOnDefocus { bool get(); void set(bool); }
        property bool selectOnClickEnd { bool get(); void set(bool); }
// Methods
        void SetViewPosition(int, int);
        void SetScrollBarsVisible(bool, bool);
        void AddItem(UIElement^);
        void InsertItem(unsigned, UIElement^, UIElement^);
        void RemoveItem(UIElement^, unsigned);
        void RemoveItem(unsigned);
        void RemoveAllItems();
        void AddSelection(unsigned);
        void RemoveSelection(unsigned);
        void ToggleSelection(unsigned);
        void ChangeSelection(int, bool);
        void ClearSelection();
        void CopySelectedItemsToClipboard();
        void Expand(unsigned, bool, bool);
        void ToggleExpand(unsigned, bool);
        bool IsSelected(unsigned);
        bool IsExpanded(unsigned);
        unsigned FindItem(UIElement^);
// Fields

        Urho3D::ListView* listview_;
    };
}
