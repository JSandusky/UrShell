#include "stdafx.h"


#include "UrControl.h"
#include "GraphicsResources.h"


#include <Urho3D/Graphics/Geometry.h>
#include <Urho3D/Graphics/IndexBuffer.h>
#include <Urho3D/Graphics/Material.h>
#include <Urho3D/Graphics/VertexBuffer.h>

namespace UrhoBackend
{

IndexBuffer::IndexBuffer()
{
    IndexData = gcnew System::Collections::Generic::List<unsigned>();
}

IndexBuffer::IndexBuffer(const Urho3D::IndexBuffer* buffer) :
    IndexBuffer()
{
    unsigned ct = buffer->GetIndexCount();
    unsigned size = buffer->GetIndexSize();

    const unsigned char* data = buffer->GetShadowData();
    if (size == sizeof(unsigned))
    {
        for (unsigned i = 0; i < ct; ++i)
            IndexData->Add(((unsigned*)data)[i]);
    }
    else
    {
        for (unsigned i = 0; i < ct; ++i)
            IndexData->Add(((short*)data)[i]);
    }
}

VertexBuffer::VertexBuffer()
{
    VertexData = gcnew System::Collections::Generic::List<float>();
}

VertexBuffer::VertexBuffer(const Urho3D::VertexBuffer* vertexBuffer) :
    VertexBuffer()
{
    VertexCount = vertexBuffer->GetVertexSize();
    VertexSize = Urho3D::VertexBuffer::GetVertexSize(vertexBuffer->GetElementMask());
    ElementMask = vertexBuffer->GetElementMask();

    for (unsigned i = 0; i < VertexCount * VertexSize; ++i)
        VertexData->Add((float)((vertexBuffer->GetShadowData())[i]));
}

Geometry::Geometry()
{
    LevelsOfDetail = gcnew System::Collections::Generic::List<Geometry^>();
    Vertices = gcnew System::Collections::Generic::List<VertexBuffer^>();
}

Bone::Bone(Bone^ parent)
{
    Parent = parent;
    Children = gcnew System::Collections::Generic::List<Bone^>();
}

Bone::Bone(Bone^ parent, const Urho3D::Bone* src) :
    Bone(parent)
{
    InitialPosition = gcnew Vector3(src->initialPosition_);
    InitialRotation = gcnew Quaternion(src->initialRotation_);
    InitialScale = gcnew Vector3(src->initialScale_);
    CollisionMask = src->collisionMask_;
    Name = gcnew System::String(src->name_.CString());
    OffsetMatrix = gcnew Matrix3x4(src->offsetMatrix_);
    Radius = src->radius_;
    ParentIdx = src->parentIndex_;
}

Skeleton::Skeleton()
{
    Bones = gcnew System::Collections::Generic::List<Bone^>();
}

Skeleton::Skeleton(Urho3D::Skeleton* skel)
{
    Bones = gcnew System::Collections::Generic::List<Bone^>();

    const Urho3D::Vector<Urho3D::Bone>& bones = skel->GetBones();
    for (unsigned i = 0; i < bones.Size(); ++i)
    {
        const Urho3D::Bone* curBone = &bones[i];
        Bones->Add(gcnew Bone(nullptr, curBone));
    }
    for (unsigned i = 0; i < Bones->Count; ++i)
    {
        if (Bones[i]->ParentIdx = -1)
            RootBone = Bones[i];
        else
        {
            Bones[i]->Parent = Bones[Bones[i]->ParentIdx];
            Bones[Bones[i]->ParentIdx]->Children->Add(Bones[i]);
        }
    }
}

Model::Model()
{
    Geometries = gcnew System::Collections::Generic::List<Geometry^>();
}

Model::Model(Urho3D::Model* srcModel) :
    Model()
{
    // Load geometries
    for (unsigned i = 0; i < srcModel->GetNumGeometries(); ++i)
    {
        Geometry^ geometry = gcnew Geometry();
        int lodCount = srcModel->GetNumGeometryLodLevels(i);
        for (unsigned l = 0; l < lodCount; ++l)
        {
            Geometry^ lodGeo = nullptr;
            if (l > 0)
                lodGeo = gcnew Geometry();

            Urho3D::Geometry* geom = srcModel->GetGeometry(i, l);
            
            IndexBuffer^ indexBuffer = gcnew IndexBuffer(geom->GetIndexBuffer());
            if (lodGeo != nullptr)
                lodGeo->Indices = indexBuffer;
            else
                geometry->Indices = indexBuffer;

            int vertBuffct = geom->GetNumVertexBuffers();

            for (unsigned v = 0; v < vertBuffct; ++v)
            {
                VertexBuffer^ vb = gcnew VertexBuffer(geom->GetVertexBuffer(v));
                if (lodGeo != nullptr)
                    lodGeo->Vertices->Add(vb);
            }

            if (lodGeo != nullptr)
                geometry->LevelsOfDetail->Add(lodGeo);
        }
        Geometries->Add(geometry);
    }

    for (unsigned i = 0; i < srcModel->GetNumMorphs(); ++i)
    {
        
    }

    Urho3D::Skeleton& skeleton = srcModel->GetSkeleton();
    Skeleton = gcnew UrhoBackend::Skeleton(&skeleton);
}

AnimKeyframe::AnimKeyframe() :
    Time(0.0f)
{

}

AnimKeyframe::AnimKeyframe(const Urho3D::AnimationKeyFrame* key) :
    Time(key->time_),
    Position(key->position_),
    Rotation(key->rotation_),
    Scale(key->scale_)
{
}

AnimTrack::AnimTrack()
{
    Keys = gcnew System::Collections::Generic::List<AnimKeyframe^>();
}

AnimTrack::AnimTrack(const Urho3D::AnimationTrack* track) :
    AnimTrack()
{
    Name = gcnew System::String(track->name_.CString());
    Mask = track->channelMask_;
    for (unsigned i = 0; track->keyFrames_.Size(); ++i)
    {
        Keys->Add(gcnew AnimKeyframe(&track->keyFrames_[i]));
    }
}

Animation::Animation()
{
    Tracks = gcnew System::Collections::Generic::List<AnimTrack^>();
}

Animation::Animation(Urho3D::Animation* anim) :
    Animation()
{
    const Urho3D::Vector<Urho3D::AnimationTrack>& tracks = anim->GetTracks();
    for (unsigned i = 0; i < tracks.Size(); ++i)
    {
        Tracks->Add(gcnew AnimTrack(&tracks[i]));
    }
}

}