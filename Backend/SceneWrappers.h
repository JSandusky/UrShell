#pragma once

#include "StringHash.h"

namespace Urho3D
{
    struct AttributeInfo;
    class Object;
    class RefCounted;
    class Component;
    class Scene;
    class Node;
    class Serializable;
    class UIElement;
}

namespace UrhoBackend
{

ref class Scene;
ref class Component;
ref class Variant;

public ref class AttributeInfo
{
public:
    AttributeInfo(const Urho3D::AttributeInfo* attrInfo);

    System::String^ Name;
    Variant^ DefaultValue;
    int VarType;
    unsigned Mode;
    System::Collections::Generic::List<System::String^>^ EnumNames;
};

public ref class RefCounted : System::ComponentModel::INotifyPropertyChanged 
{
public:
    RefCounted(Urho3D::RefCounted* comp);
    RefCounted(System::IntPtr^ ptr);
    ~RefCounted();

    // Properties
    [System::ComponentModel::Browsable(false)]
    property int refs { int get(); }
    [System::ComponentModel::Browsable(false)]
    property int weakRefs { int get(); }
    // Methods
    void AddRef();
    void ReleaseRef();
    // Fields

    virtual event System::ComponentModel::PropertyChangedEventHandler^ PropertyChanged;

    virtual void PropertyChange(System::String^);

    Urho3D::RefCounted* refcounted_;

private:
    bool addedRef_;
};

public ref class UObject : public RefCounted
{
public:
    UObject(Urho3D::Object* comp);
    UObject(System::IntPtr^ ptr);

    // Properties
    [System::ComponentModel::BrowsableAttribute(false)]
    property UrhoBackend::StringHash^ type { UrhoBackend::StringHash^ get(); }
    [System::ComponentModel::Browsable(false)]
    property UrhoBackend::StringHash^ baseType { UrhoBackend::StringHash^ get(); }
    [System::ComponentModel::Browsable(false)]
    property System::String^ typeName { System::String^ get(); }
    [System::ComponentModel::Browsable(false)]
    property System::String^ category { System::String^ get(); }
    // Methods
    // Fields

    Urho3D::Object* object_;
};

public ref class Serializable : public UObject
{
public:
    Serializable(Urho3D::Serializable* serial);
    Serializable(System::IntPtr^ ptr);

    System::String^ GetTypeName();

    bool EqualsOther(Serializable^ other) { return serializable_ == other->serializable_; }

    System::Collections::Generic::List<System::String^>^ GetAttributes();
    AttributeInfo^ GetAttributeInfo(System::String^ attr);
    System::Collections::Generic::List<AttributeInfo^>^ GetAttributeInfo();
    System::String^ GetAttributeType(System::String^ attr);
    System::Collections::Generic::List<System::String^>^ GetAttributeTypes();

    Variant^ GetAttributeDefault(System::String^ name);
    Variant^ GetAttribute(System::String^ name);
    void SetAttribute(System::String^ name, Variant^ var);

    Urho3D::Serializable* GetSerial() { return serializable_; }

    bool Equals(UrhoBackend::Serializable^ rhs) { return serializable_ == rhs->serializable_; }

    bool IsNull() { return serializable_ != 0; }

    Urho3D::Serializable* serializable_;
};

ref class VariantMap;

public ref class Node : public Serializable
{
public:
    Node(Urho3D::Node* serial);

    unsigned GetID();
    int ComponentCount();
    int ChildCount();

    System::String^ GetName();
    void SetName(System::String^ name);
    VariantMap^ GetVars();
    void SetVar(System::String^ name, Variant^ var);
    bool IsNetworked();

    // General functions
    Scene^ GetScene();
    Component^ GetComponent(int index);  
    Component^ GetComponentByID(int id);
    Component^ GetComponent(System::String^ typeName);
    Node^ GetChild(System::String^ name, bool recurse);
    Node^ GetChild(int index);
    Node^ GetChildByID(int id);
    System::Collections::Generic::List<Node^>^ GetChildren();
    System::Collections::Generic::List<Component^>^ GetComponents();
    Node^ GetParent();
    void AddChild(Node^ node);

    // Creation functions
    Node^ CreateChild(System::String^ name, bool replicated);
    Component^ CreateComponent(System::String^ type, bool replicated);
    Component^ GetOrCreateComponent(System::String^ type, bool replicated);

    // Remove functions
    void Remove();
    void RemoveChild(Node^ node);
    void RemoveComponent(Component^ component);
    void RemoveAllChildren();
    void RemoveAllComponents();

    Urho3D::Node* node_;
};

public ref class Scene : public Node
{
public:
    Scene(Urho3D::Scene* serial);

    Urho3D::Scene* scene_;
};

public ref class Component : public Serializable
{
public:
    Component(Urho3D::Component* serial);

    unsigned GetID();
    Node^ GetNode();
    Scene^ GetScene();
    void Remove();

    Urho3D::Component* component_;
};

public ref class UIElement : public Serializable
{
    Urho3D::UIElement* element_;
public:
    UIElement(Urho3D::UIElement* element);

    System::String^ GetName();
    UIElement^ CreateChild(System::String^ typeName, System::String^ name);
    UIElement^ GetChild(System::String^ name, bool recurse);
    System::Collections::Generic::List<UIElement^>^ GetChildren();
    UIElement^ GetParent();

    Variant^ GetVar(System::String^ name);
    void SetVar(System::String^ name, Variant^ value);

    void Remove();
    void RemoveChild(UIElement^ elem);
    void RemoveAllChildren();
};

}