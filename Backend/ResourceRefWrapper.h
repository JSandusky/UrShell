#pragma once

#using <mscorlib.dll>
#using <System.dll>
#using <System.Drawing.dll>
#using <System.Windows.Forms.dll>

#include <Urho3D/Urho3D.h>
#include <Urho3D/Math/StringHash.h>

using namespace System::ComponentModel;

namespace Urho3D
{
    class ResourceRef;
    class ResourceRefList;
}

namespace UrhoBackend
{
    
    public ref class ResourceRef
    {
    public:
        ResourceRef();
        explicit ResourceRef(System::String^ typeName, System::String^ resName);
        explicit ResourceRef(Urho3D::StringHash hash, System::String^ resName);
        ResourceRef(const Urho3D::ResourceRef*);
        ResourceRef(const Urho3D::ResourceRef);
        ~ResourceRef();

        property System::String^ Name { System::String^ get() { return GetName(); } }
        property System::String^ ResType { System::String^ get() { return GetResourceType(); } }

        unsigned GetResourceTypeInt();
        System::String^ GetResourceType();
        System::String^ GetName();

        void SetResourceType(System::String^ typeName);
        void SetResourceName(System::String^ name);

        System::String^ ToString() override;
        
        Urho3D::ResourceRef* ref_;
    };

    
    public ref class ResourceRefList
    {
    public:
        ResourceRefList();
        ResourceRefList(const Urho3D::ResourceRefList*);
        ResourceRefList(const Urho3D::ResourceRefList);
        ~ResourceRefList();

 
        unsigned GetResourceType();
        unsigned Size();
        ResourceRef^ Get(int index);
        void Add(ResourceRef^ wrapper);
        void Remove(ResourceRef^ wrapper);
        void Erase(int index);
        void Clear();

        System::Collections::Generic::List<System::String^>^ ToList();
        void FromList(System::Collections::Generic::List<System::String^>^ list);

        Urho3D::ResourceRefList* refList_;
    };
}