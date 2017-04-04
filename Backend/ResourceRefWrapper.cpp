#include "stdafx.h"

#include "ResourceRefWrapper.h"

#include "UrControl.h"

#include <Urho3D/Urho3D.h>
#include <Urho3D/Resource/Resource.h>

namespace UrhoBackend
{

    ResourceRef::ResourceRef()
    {
        ref_ = new Urho3D::ResourceRef();
    }

    ResourceRef::ResourceRef(System::String^ typeName, System::String^ resName)
    {
        ref_ = new Urho3D::ResourceRef(ToCString(typeName), ToCString(resName));
    }

    ResourceRef::ResourceRef(const Urho3D::ResourceRef* ref)
    {
        ref_ = new Urho3D::ResourceRef(ref->type_, ref->name_);
    }

    ResourceRef::ResourceRef(const Urho3D::ResourceRef ref)
    {
        ref_ = new Urho3D::ResourceRef(ref);
    }

    ResourceRef::ResourceRef(Urho3D::StringHash hash, System::String^ resName)
    {
        ref_ = new Urho3D::ResourceRef(hash, ToCString(resName));
    }

    ResourceRef::~ResourceRef()
    {
        delete ref_;
    }

    unsigned ResourceRef::GetResourceTypeInt()
    {
        if (ref_)
            return ref_->type_.Value();
        return -1;
    }

    System::String^ ResourceRef::GetResourceType()
    {
        return gcnew System::String(ref_->type_.ToString().CString());
    }

    System::String^ ResourceRef::ToString()
    {
        return gcnew System::String(ref_->name_.CString());
    }

    System::String^ ResourceRef::GetName()
    {
        return gcnew System::String(ref_->name_.CString());
    }

    void ResourceRef::SetResourceType(System::String^ typeName)
    {
        ref_->type_ = Urho3D::StringHash(ToCString(typeName));
    }

    void ResourceRef::SetResourceName(System::String^ name)
    {
        ref_->name_ = ToCString(name);
    }

    ResourceRefList::ResourceRefList()
    {
        refList_ = new Urho3D::ResourceRefList();
    }
    ResourceRefList::ResourceRefList(const Urho3D::ResourceRefList* list)
    {
        refList_ = new Urho3D::ResourceRefList();
        refList_->names_ = list->names_;
        refList_->type_ = list->type_;
    }
    ResourceRefList::ResourceRefList(const Urho3D::ResourceRefList ref)
    {
        refList_ = new Urho3D::ResourceRefList(ref);
    }
    ResourceRefList::~ResourceRefList()
    {
        delete refList_;
    }

    unsigned ResourceRefList::GetResourceType()
    {
        if (refList_)
            return refList_->type_.Value();
        return -1;
    }

    unsigned ResourceRefList::Size()
    {
        return refList_->names_.Size();
    }
    ResourceRef^ ResourceRefList::Get(int index)
    {
        if (index >= Size())
            return nullptr;
        Urho3D::ResourceRef r(refList_->type_, refList_->names_[index]);
        return gcnew ResourceRef(&r);
    }

    void ResourceRefList::Add(ResourceRef^ wrapper)
    {
        if (wrapper->ref_->type_ == refList_->type_)
        {
            refList_->names_.Push(wrapper->ref_->name_);
        }
    }
    void ResourceRefList::Remove(ResourceRef^ wrapper)
    {
        if (wrapper->ref_->type_ == refList_->type_)
        {
            if (refList_->names_.Contains(wrapper->ref_->name_))
                refList_->names_.Remove(wrapper->ref_->name_);
        }
    }
    void ResourceRefList::Erase(int index)
    {
        if (index < Size())
        {
            refList_->names_.Erase(index);
        }
    }
    void ResourceRefList::Clear()
    {
        refList_->names_.Clear();
    }

    System::Collections::Generic::List<System::String^>^ ResourceRefList::ToList()
    {
        System::Collections::Generic::List<System::String^>^ ret = gcnew System::Collections::Generic::List<System::String^>();

        if (refList_)
        {
            for (unsigned i = 0; i < refList_->names_.Size(); ++i)
            {
                ret->Add(gcnew System::String(refList_->names_[i].CString()));
            }
        }

        return ret;
    }

    void ResourceRefList::FromList(System::Collections::Generic::List<System::String^>^ list)
    {
        if (refList_)
        {
            refList_->names_.Clear();
            for (int i = 0; i < list->Count; ++i)
                refList_->names_.Push(Urho3D::String(ToCString(list[i])));
        }
    }

}