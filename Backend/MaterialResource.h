#pragma once

namespace Urho3D
{
    class Material;
}

namespace UrhoBackend
{

    public ref class MaterialResource
    {
    public:
        MaterialResource(System::IntPtr pointer);

        

        Urho3D::Material* material_;
    };

}