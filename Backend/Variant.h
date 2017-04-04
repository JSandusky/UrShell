#pragma once

#include <Urho3D/Urho3D.h>
#include <Urho3D/Core/Variant.h>
#include <Urho3D/Container/HashMap.h>

namespace UrhoBackend
{
    ref class StringHash;
    ref class Vector2;
    ref class Vector3;
    ref class Vector4;
    ref class Quaternion;
    ref class IntVector2;
    ref class IntRect;
    ref class Color;
    ref class Matrix3;
    ref class Matrix3x4;
    ref class Matrix4;
    ref class VariantVector;
    ref class VariantMap;
    ref class ResourceRef;
    ref class ResourceRefList;
    ref class Node;
    ref class Component;
    ref class Scene;
    ref class Buffer;

    public ref class Variant
    {
    public:
        Variant();
        Variant(int);
        Variant(unsigned);
        Variant(float);
        Variant(bool);
        Variant(System::String^);
        Variant(Color^);
        Variant(Vector2^);
        Variant(Vector3^);
        Variant(Vector4^);
        Variant(IntVector2^);
        Variant(Quaternion^);
        Variant(IntRect^);
        Variant(Matrix3^);
        Variant(Matrix3x4^);
        Variant(Matrix4^);
        Variant(VariantMap^);
        Variant(VariantVector^);
        Variant(ResourceRef^);
        Variant(ResourceRefList^);
        Variant(Buffer^);
        Variant(System::IntPtr);

        Variant(Urho3D::Variant var);
        ~Variant();

        int GetVarTypeID() { return (int)variant_->GetType(); }
        System::String^ GetVarType() { return gcnew System::String(variant_->GetTypeName(variant_->GetType()).CString()); }
        void SetInt(int value) { *variant_ = Urho3D::Variant(value); }
        void SetUInt(unsigned value) { *variant_ = Urho3D::Variant(value); }
        void SetFloat(float value) { *variant_ = Urho3D::Variant(value); }
        void SetBool(bool value) { *variant_ = Urho3D::Variant(value); }
        void SetColor(Color^ color);
        void SetString(System::String^ value);
        void SetVector2(Vector2^ value);
        void SetVector3(Vector3^ value);
        void SetVector4(Vector4^ value);
        void SetQuaternion(Quaternion^ value);
        void SetIntVector2(IntVector2^ value);
        void SetIntRect(IntRect^ value);
        void SetMatrix3(Matrix3^ value);
        void SetMatrix3x4(Matrix3x4^ value);
        void SetMatrix4(Matrix4^ value);
        void SetVariantMap(VariantMap^ map);
        void SetVariantVector(VariantVector^ vec);
        void SetResourceRef(ResourceRef^ ref);
        void SetResourceRefList(ResourceRefList^ ref);
        void SetBuffer(Buffer^ buff);
        void SetPtr(System::IntPtr ptr);

        void FromString(System::String^ type, System::String^ data);
        virtual System::String^ ToString() override;

        int GetInt();
        unsigned GetUInt();
        float GetFloat();
        bool GetBool();
        System::String^ GetString();
        Vector2^ GetVector2();
        Vector3^ GetVector3();
        Vector4^ GetVector4();
        Quaternion^ GetQuaternion();
        IntVector2^ GetIntVector2();
        Color^ GetColor();
        IntRect^ GetIntRect();
        Matrix3^ GetMatrix3();
        Matrix3x4^ GetMatrix3x4();
        Matrix4^ GetMatrix4();
        VariantMap^ GetVariantMap();
        VariantVector^ GetVariantVector();
        ResourceRef^ GetResourceRef();
        ResourceRefList^ GetResourceRefList();
        Node^ GetNode();
        Component^ GetComponent();
        Scene^ GetScene();
        Buffer^ GetBuffer();
        System::IntPtr GetPtr();

        Urho3D::Variant* variant_;
    };

    public ref class VariantMap
    {
    public:
        VariantMap();
        VariantMap(Urho3D::VariantMap map);
        ~VariantMap();
        
        bool Contains(System::String^ value);
        Variant^ Get(UrhoBackend::StringHash^ hash);
        Variant^ Get(System::String^ value);
        void Set(System::String^ name, Variant^ value);
        void Set(UrhoBackend::StringHash^ hash, Variant^ value);
        void Remove(System::String^ name);
        void Clear();

        System::Collections::Generic::List <UrhoBackend::StringHash^>^ Keys();

        bool owned_;
        Urho3D::VariantMap* map_;
    };

    public ref class VariantVector : public System::Collections::Generic::List<Variant^>
    {
    public:
        VariantVector();
        VariantVector(Urho3D::VariantVector vector);
        ~VariantVector();

        Urho3D::VariantVector ToVariantVector();
    };

}