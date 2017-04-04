#include "stdafx.h"

#include "ContextManager.h"

#include <Urho3D/Urho3D.h>
#include <Urho3D/Resource/ResourceCache.h>

namespace UrhoBackend
{

ContextRecord::ContextRecord()
{
    context_ = new Urho3D::Context();
}

ContextRecord::~ContextRecord()
{
    delete context_;
}

System::Collections::Generic::Dictionary<System::String^, System::Collections::Generic::List<System::String^>^>^ ContextRecord::GetObjectTypes()
{
    System::Collections::Generic::Dictionary<System::String^, System::Collections::Generic::List<System::String^>^>^ ret = gcnew System::Collections::Generic::Dictionary<System::String^, System::Collections::Generic::List<System::String^>^>();
    const Urho3D::HashMap<Urho3D::String, Urho3D::Vector<Urho3D::StringHash> >& categories = context_->GetObjectCategories();
    for (Urho3D::HashMap<Urho3D::String, Urho3D::Vector<Urho3D::StringHash> >::ConstIterator cit = categories.Begin(); cit != categories.End(); ++cit)
    {
        System::Collections::Generic::List<System::String^>^ members = gcnew System::Collections::Generic::List<System::String^>();
        for (Urho3D::Vector<Urho3D::StringHash>::ConstIterator mit = cit->second_.Begin(); mit != cit->second_.End(); ++mit)
            members->Add(GetObjectTypeName(mit->Value()));
        ret[gcnew System::String(cit->first_.CString())] = members;
    }

    return ret;
}
System::String^ ContextRecord::GetObjectTypeName(unsigned hashCode)
{
    return gcnew System::String(context_->GetTypeName(Urho3D::StringHash(hashCode)).CString());
}

ContextManager::ContextManager()
{
    inst_ = this;
    contexts_ = gcnew System::Collections::Generic::Dictionary<System::String^, ContextRecord^>();
}

ContextManager::~ContextManager()
{
    contexts_->Clear();
}

void ContextManager::Destroy()
{
    inst_ = nullptr;
}

ContextRecord^ ContextManager::CreateContext(System::String^ name)
{
    ContextRecord^ rec = gcnew ContextRecord();
    contexts_[name] = rec;
    return rec;
}

ContextRecord^ ContextManager::GetContext(System::String^ name)
{
    if (contexts_->ContainsKey(name))
        return contexts_[name];
    return nullptr;
}

bool ContextManager::Contains(System::String^ name)
{
    return contexts_->ContainsKey(name);
}

void ContextManager::RemoveContext(System::String^ name)
{
    if (contexts_->ContainsKey(name))
        contexts_->Remove(name);
}

}