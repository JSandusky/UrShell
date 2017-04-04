#include "stdafx.h"

#include "MathBind.h"

namespace UrhoBackend
{

Vector3::Vector3() : MathVector(3)
{
    x = 0; y = 0; z = 0.0f;
}
    
Vector3::Vector3(Urho3D::Vector3 vec) : MathVector(3)
{
    x = vec.x_;
    y = vec.y_;
    z = vec.z_;
}

Urho3D::Vector3 Vector3::ToVector3()
{
    return Urho3D::Vector3(x, y, z);
}



}