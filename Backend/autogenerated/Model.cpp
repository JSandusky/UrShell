//THIS FILE IS AUTOGENERATED - DO NOT MODIFY
#include "stdafx.h"
#include <Urho3D/Urho3D.h>
#include <Urho3D/Scene/Component.h>
#include <Urho3D/Container/Str.h>
#include "../MathBind.h"
#include "../ResourceRefWrapper.h"
#include "../SceneWrappers.h"
#include "../Variant.h"
#include "../StringHash.h"
#include "../UrControl.h"
#include "Model.h"

#include <Urho3D/Graphics/Model.h>
#include <Urho3D/Graphics/Geometry.h>
#include "Geometry.h"
#include <Urho3D/Math/BoundingBox.h>
#include "BoundingBox.h"
#include <Urho3D/Graphics/Skeleton.h>
#include "Skeleton.h"

namespace UrhoBackend {

Model::Model(Urho3D::Model* fromUrho) : Resource(fromUrho) { model_ = fromUrho; }
Model::Model(System::IntPtr^ ptr) : Model((Urho3D::Model*)ptr->ToPointer()) { }

BoundingBox^ Model::boundingBox::get() { return gcnew UrhoBackend::BoundingBox(model_->GetBoundingBox()); }
void Model::boundingBox::set(BoundingBox^ value) { model_->SetBoundingBox(*value->boundingbox_); }

Skeleton^ Model::skeleton::get() { return gcnew UrhoBackend::Skeleton(model_->GetSkeleton()); }
unsigned Model::numGeometries::get() { return model_->GetNumGeometries(); }
void Model::numGeometries::set(unsigned value) { model_->SetNumGeometries(value); }

unsigned Model::numGeometryLodLevels::get(unsigned A) { return model_->GetNumGeometryLodLevels(A); }

void Model::numGeometryLodLevels::set(unsigned A, unsigned B) { model_->SetNumGeometryLodLevels(A, B); }

UrhoBackend::Vector3^ Model::geometryCenters::get(unsigned A) { return gcnew UrhoBackend::Vector3(model_->GetGeometryCenter(A)); }

void Model::geometryCenters::set(unsigned A, UrhoBackend::Vector3^ B) { model_->SetGeometryCenter(A, B->ToVector3()); }

unsigned Model::numMorphs::get() { return model_->GetNumMorphs(); }
bool Model::SetGeometry(unsigned A, unsigned B, Geometry^ C)  { return  model_->SetGeometry(A, B, C->geometry_); }

Geometry^ Model::GetGeometry(unsigned A, unsigned B)  { return  gcnew UrhoBackend::Geometry(model_->GetGeometry(A, B)); }

}
