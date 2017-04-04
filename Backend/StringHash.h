#pragma once

#include <Urho3D/Urho3D.h>
#include <Urho3D/Math/StringHash.h>

namespace UrhoBackend
{

public ref class StringHash
{
public:
    StringHash(System::String^ str);
    StringHash(unsigned value);
    StringHash(const Urho3D::StringHash& hash);

    Urho3D::StringHash ToStringHash();

    property unsigned Value;
};

}