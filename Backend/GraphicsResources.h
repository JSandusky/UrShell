#pragma once

#include <Urho3D/Urho3D.h>
#include <Urho3D/Graphics/Animation.h>
#include <Urho3D/Graphics/Model.h>

#include "MathBind.h"

namespace Urho3D
{
    class Material;
}

namespace UrhoBackend
{

    public ref class VertexBuffer
    {
    public:
        VertexBuffer();
        VertexBuffer(const Urho3D::VertexBuffer*);

        property int VertexCount;
        property int VertexSize;
        property unsigned ElementMask;

        System::Collections::Generic::List<float>^ VertexData;
    };

    public ref class IndexBuffer
    {
    public:
        IndexBuffer();
        IndexBuffer(const Urho3D::IndexBuffer*);

        System::Collections::Generic::List<unsigned>^ IndexData;
    };

    public ref class Geometry
    {
    public:
        Geometry();

        IndexBuffer^ Indices;
        System::Collections::Generic::List<VertexBuffer^>^ Vertices;
        System::Collections::Generic::List<Geometry^>^ LevelsOfDetail;
    };

    public ref class Bone
    {
    public:
        Bone(Bone^ parent);
        Bone(Bone^ parent, const Urho3D::Bone* src);

        int ParentIdx;
        Bone^ Parent;
        System::Collections::Generic::List<Bone^>^ Children;

        System::String^ Name;
        Matrix3x4^ OffsetMatrix;
        Vector3^ InitialPosition;
        Quaternion^ InitialRotation;
        Vector3^ InitialScale;
        char CollisionMask;
        float Radius;
    };

    public ref class Skeleton
    {
    public:
        Skeleton();
        Skeleton(Urho3D::Skeleton*);

        Bone^ RootBone;
        System::Collections::Generic::List<Bone^>^ Bones;
    };

    public ref class Model
    {
    public:
        Model();
        Model(Urho3D::Model* srcModel);

        Skeleton^ Skeleton;
        System::Collections::Generic::List<Geometry^>^ Geometries;
    };

    public ref class AnimKeyframe
    {
    public:
        AnimKeyframe();
        AnimKeyframe(const Urho3D::AnimationKeyFrame* key);

        UrhoBackend::Vector3 Position;
        UrhoBackend::Quaternion Rotation;
        UrhoBackend::Vector3 Scale;
        float Time;
    };

    public ref class AnimTrack
    {
    public:
        AnimTrack();
        AnimTrack(const Urho3D::AnimationTrack* track);

        property System::String^ Name;
        property char Mask;

        System::Collections::Generic::List<AnimKeyframe^>^ Keys;
    };

    public ref class Animation
    {
    public:
        Animation();
        Animation(Urho3D::Animation* anim);

        System::Collections::Generic::List<AnimTrack^>^ Tracks;
    };

}