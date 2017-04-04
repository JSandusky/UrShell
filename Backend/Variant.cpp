#include "stdafx.h"

#include "MathBind.h"
#include "Variant.h"
#include "UrControl.h"
#include "ResourceRefWrapper.h"
#include "SceneWrappers.h"
#include "StringHash.h"
#include "BufferWrapper.h"

#include <Urho3D/Urho3D.h>
#include <Urho3D/Scene/Component.h>
#include <Urho3D/Scene/Node.h>
#include <Urho3D/Scene/Scene.h>

using namespace Urho3D;

namespace UrhoBackend
{
    Variant::Variant()
    {
        variant_ = new Urho3D::Variant();
    }

    Variant::Variant(int value) : Variant()
    {
        SetInt(value);
    }
    Variant::Variant(unsigned value) : Variant()
    {
        SetUInt(value);
    }
    Variant::Variant(float value) : Variant()
    {
        SetFloat(value);
    }
    Variant::Variant(bool value) : Variant()
    {
        SetBool(value);
    }
    Variant::Variant(System::String^ value) : Variant()
    {
        SetString(value);
    }
    Variant::Variant(Color^ value) : Variant()
    {
        SetColor(value);
    }
    Variant::Variant(Vector2^ value) : Variant()
    {
        SetVector2(value);
    }
    Variant::Variant(Vector3^ value) : Variant()
    {
        SetVector3(value);
    }
    Variant::Variant(Vector4^ value) : Variant()
    {
        SetVector4(value);
    }
    Variant::Variant(IntVector2^ value) : Variant()
    {
        SetIntVector2(value);
    }
    Variant::Variant(Quaternion^ value) : Variant()
    {
        SetQuaternion(value);
    }
    Variant::Variant(IntRect^ value) : Variant()
    {
        SetIntRect(value);
    }
    Variant::Variant(Matrix3^ value)
    {
        SetMatrix3(value);
    }
    Variant::Variant(Matrix3x4^ value)
    {
        SetMatrix3x4(value);
    }
    Variant::Variant(Matrix4^ value)
    {
        SetMatrix4(value);
    }
    Variant::Variant(VariantMap^ value) : Variant()
    {
        SetVariantMap(value);
    }
    Variant::Variant(VariantVector^ value) : Variant()
    {
        SetVariantVector(value);
    }
    Variant::Variant(ResourceRef^ value) : Variant()
    {
        SetResourceRef(value);
    }
    Variant::Variant(ResourceRefList^ value) : Variant()
    {
        SetResourceRefList(value);
    }

    Variant::Variant(Urho3D::Variant var)
    {
        variant_ = new Urho3D::Variant(var);
    }

    Variant::Variant(Buffer^ buff)
    {
        SetBuffer(buff);
    }

    Variant::Variant(System::IntPtr ptr)
    {
        SetPtr(ptr);
    }

    Variant::~Variant()
    {
        delete variant_;
    }

    void Variant::SetColor(Color^ color)
    {
        *variant_ = Urho3D::Variant(color->ToColor());
    }

    void Variant::SetString(System::String^ value)
    {
        *variant_ = Urho3D::Variant(ToCString(value));
    }

    void Variant::SetVector2(Vector2^ value)
    {
        *variant_ = Urho3D::Variant(value->ToVector2());
    }
    void Variant::SetVector3(Vector3^ value)
    {
        *variant_ = Urho3D::Variant(value->ToVector3());
    }
    void Variant::SetVector4(Vector4^ value)
    {
        *variant_ = Urho3D::Variant(value->ToVector4());
    }
    void Variant::SetQuaternion(Quaternion^ value)
    {
        *variant_ = Urho3D::Variant(value->ToQuaternion());
    }
    void Variant::SetIntVector2(IntVector2^ value)
    {
        *variant_ = Urho3D::Variant(value->ToIntVector2());
    }
    void Variant::SetIntRect(IntRect^ value)
    {
        *variant_ = Urho3D::Variant(value->ToIntRect());
    }
    void Variant::SetMatrix3(Matrix3^ value)
    {
        *variant_ = Urho3D::Variant(value->ToMatrix3());
    }
    void Variant::SetMatrix3x4(Matrix3x4^ value)
    {
        *variant_ = Urho3D::Variant(value->ToMatrix3x4());
    }
    void Variant::SetMatrix4(Matrix4^ value)
    {
        *variant_ = Urho3D::Variant(value->ToMatrix4());
    }
    void Variant::SetVariantMap(VariantMap^ map)
    {
        *variant_ = Urho3D::Variant(*map->map_);
    }
    void Variant::SetVariantVector(VariantVector^ vec)
    {
        *variant_ = Urho3D::Variant(vec->ToVariantVector());
    }

    void Variant::SetResourceRef(ResourceRef^ ref)
    {
        *variant_ = Urho3D::Variant(*ref->ref_);
    }

    void Variant::SetResourceRefList(ResourceRefList^ ref)
    {
        *variant_ = Urho3D::Variant(*ref->refList_);
    }

    void Variant::SetBuffer(Buffer^ buff)
    {

    }

    void Variant::SetPtr(System::IntPtr ptr)
    {
        *variant_ = Urho3D::Variant(ptr.ToPointer());
    }

    void Variant::FromString(System::String^ type, System::String^ data)
    {
        variant_->FromString(ToCString(type), ToCString(data));
    }

    System::String^ Variant::ToString()
    {
        return gcnew System::String(variant_->ToString().CString());
    }

    int Variant::GetInt() { return variant_->GetInt(); }
    unsigned Variant::GetUInt() { return variant_->GetUInt(); }
    float Variant::GetFloat() { return variant_->GetFloat(); }
    bool Variant::GetBool() { return variant_->GetBool(); }
    System::String^ Variant::GetString() { return gcnew System::String(variant_->GetString().CString()); }
    Vector2^ Variant::GetVector2() { return gcnew Vector2(variant_->GetVector2()); }
    Vector3^ Variant::GetVector3() { return gcnew Vector3(variant_->GetVector3()); }
    Vector4^ Variant::GetVector4() { return gcnew Vector4(variant_->GetVector4()); }
    Quaternion^ Variant::GetQuaternion() { return gcnew Quaternion(variant_->GetQuaternion()); }
    IntVector2^ Variant::GetIntVector2() { return gcnew IntVector2(variant_->GetIntVector2()); }
    Color^ Variant::GetColor() { return gcnew Color(variant_->GetColor()); }
    IntRect^ Variant::GetIntRect() { return gcnew IntRect(variant_->GetIntRect()); }
    Matrix3^ Variant::GetMatrix3() { return gcnew Matrix3(variant_->GetMatrix3()); }
    Matrix3x4^ Variant::GetMatrix3x4() { return gcnew Matrix3x4(variant_->GetMatrix3x4()); }
    Matrix4^ Variant::GetMatrix4() { return gcnew Matrix4(variant_->GetMatrix4()); }
    VariantMap^ Variant::GetVariantMap() { return gcnew VariantMap(variant_->GetVariantMap()); }
    VariantVector^ Variant::GetVariantVector() { return gcnew VariantVector(variant_->GetVariantVector()); }
    Buffer^ Variant::GetBuffer() { return gcnew Buffer(); }
    System::IntPtr Variant::GetPtr() { return System::IntPtr(variant_->GetPtr()); }

    Node^ Variant::GetNode()
    {
        if (Urho3D::Node* node = dynamic_cast<Urho3D::Node*>(variant_->GetPtr()))
        {
            return gcnew Node(node);
        }
        return nullptr;
    }
    Component^ Variant::GetComponent()
    {
        if (Urho3D::Component* node = dynamic_cast<Urho3D::Component*>(variant_->GetPtr()))
        {
            return gcnew Component(node);
        }
        return nullptr;
    }
    Scene^ Variant::GetScene()
    {
        if (Urho3D::Scene* scene = dynamic_cast<Urho3D::Scene*>(variant_->GetPtr()))
        {
            return gcnew Scene(scene);
        }
        return nullptr;
    }

    ResourceRef^ Variant::GetResourceRef()
    {
        const Urho3D::ResourceRef& ref = variant_->GetResourceRef();
        return gcnew ResourceRef(&ref);
    }
    ResourceRefList^ Variant::GetResourceRefList()
    {
        const Urho3D::ResourceRefList& list = variant_->GetResourceRefList();
        return gcnew ResourceRefList(&list);
    }

    VariantMap::VariantMap()
    {
        owned_ = true;
        map_ = new Urho3D::VariantMap();
    }

    VariantMap::VariantMap(Urho3D::VariantMap map)
    {
        owned_ = true;
        map_ = new Urho3D::VariantMap(map);
    }

    VariantMap::~VariantMap()
    {
        if (owned_)
            delete map_;
    }

    bool VariantMap::Contains(System::String^ name)
    {
        return map_->Contains(ToCString(name));
    }

    void VariantMap::Set(System::String^ name, Variant^ value)
    {
        const char* str = ToCString(name);
        (*map_)[Urho3D::StringHash(str)] = *(value->variant_);
    }

    void VariantMap::Set(UrhoBackend::StringHash^ name, Variant^ value)
    {
        (*map_)[Urho3D::StringHash(name->Value)] = *(value->variant_);
    }

    Variant^ VariantMap::Get(UrhoBackend::StringHash^ hash)
    {
        Variant^ ret = gcnew Variant();
        *ret->variant_ = (*map_)[Urho3D::StringHash(hash->Value)];
        return ret;
    }

    Variant^ VariantMap::Get(System::String^ name)
    {
        const char* str = ToCString(name);
        Variant^ ret = gcnew Variant();
        *ret->variant_ = (*map_)[str];
        return ret;
    }

    void VariantMap::Remove(System::String^ name)
    {
        map_->Erase(ToCString(name));
    }

    void VariantMap::Clear()
    {
        map_->Clear();
    }

    System::Collections::Generic::List<UrhoBackend::StringHash^>^ VariantMap::Keys()
    {
        System::Collections::Generic::List<UrhoBackend::StringHash^>^ ret = gcnew System::Collections::Generic::List<UrhoBackend::StringHash^>();

        Urho3D::Vector<Urho3D::StringHash> keys = map_->Keys();
        for (unsigned i = 0; i < keys.Size(); ++i)
            ret->Add(gcnew UrhoBackend::StringHash(keys[i].Value()));

        return ret;
    }

    VariantVector::VariantVector()
    {
    }

    VariantVector::VariantVector(Urho3D::VariantVector vector)
    {
        for (unsigned i = 0; i < vector.Size(); ++i)
            Add(gcnew Variant(vector[i]));
    }

    VariantVector::~VariantVector()
    {
    }

    Urho3D::VariantVector VariantVector::ToVariantVector()
    {
        Urho3D::VariantVector ret;
        for (int i = 0; i < Count; ++i)
            ret.Push(*(this[i]->variant_));
        return ret;
    }
}