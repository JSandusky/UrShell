#include "stdafx.h"

#include "MaterialResource.h"
#include <Urho3D/Urho3D.h>
#include <Urho3D/Graphics/Material.h>

namespace UrhoBackend
{
    MaterialResource::MaterialResource(System::IntPtr pointer)
    {
        material_ = (Urho3D::Material*)pointer.ToPointer();
    }
}