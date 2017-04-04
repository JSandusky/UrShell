#pragma once

#include <Urho3D/Urho3D.h>
#include <Urho3D/Core/Context.h>

#include "StringHash.h"

namespace UrhoBackend
{

public ref class ContextRecord
{
public:
    ContextRecord();
    ~ContextRecord();

    System::Collections::Generic::Dictionary<System::String^, System::Collections::Generic::List<System::String^>^>^ GetObjectTypes();
    System::String^ GetObjectTypeName(unsigned hashCode);

    Urho3D::Context* context_;
};

public ref class ContextManager
{
public:
    ContextManager();
    ~ContextManager();

    void Destroy();

    ContextRecord^ CreateContext(System::String^ name);
    ContextRecord^ GetContext(System::String^ name);
    bool Contains(System::String^ name);
    void RemoveContext(System::String^ name);

    static ContextManager^ inst_;
    static ContextManager^ GetInst() { return inst_; }

private:
    System::Collections::Generic::Dictionary<System::String^, ContextRecord^>^ contexts_;
};

}