#include "stdafx.h"

#include "StringHash.h"

#include "UrControl.h"

#include <Urho3D/Urho3D.h>
#include <Urho3D/Container/Str.h>
#include <Urho3D/Math/StringHash.h>

namespace UrhoBackend
{

StringHash::StringHash(System::String^ str)
{
    Urho3D::StringHash hash = Urho3D::StringHash(ToCString(str));
    Value = hash.Value();
}

StringHash::StringHash(unsigned value)
{
    Value = value;
}

StringHash::StringHash(const Urho3D::StringHash& hash)
{
    Value = hash.Value();
}

Urho3D::StringHash StringHash::ToStringHash()
{
    return Urho3D::StringHash(Value);
}

}