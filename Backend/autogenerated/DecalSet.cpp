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
#include "DecalSet.h"

#include <Urho3D/Graphics/DecalSet.h>
#include <Urho3D/Graphics/Drawable.h>
#include "Drawable.h"
#include <Urho3D/Graphics/Material.h>
#include "Material.h"
#include <Urho3D/Graphics/Zone.h>
#include "Zone.h"

namespace UrhoBackend {

DecalSet::DecalSet(Urho3D::DecalSet* fromUrho) : Drawable(fromUrho) { decalset_ = fromUrho; }
DecalSet::DecalSet(System::IntPtr^ ptr) : DecalSet((Urho3D::DecalSet*)ptr->ToPointer()) { }

Material^ DecalSet::material::get() { return gcnew UrhoBackend::Material(decalset_->GetMaterial()); }
void DecalSet::material::set(Material^ value) { decalset_->SetMaterial(value->material_); }

unsigned DecalSet::numDecals::get() { return decalset_->GetNumDecals(); }
unsigned DecalSet::numVertices::get() { return decalset_->GetNumVertices(); }
unsigned DecalSet::numIndices::get() { return decalset_->GetNumVertices(); }
unsigned DecalSet::maxVertices::get() { return decalset_->GetMaxVertices(); }
void DecalSet::maxVertices::set(unsigned value) { decalset_->SetMaxVertices(value); }

unsigned DecalSet::maxIndices::get() { return decalset_->GetMaxIndices(); }
void DecalSet::maxIndices::set(unsigned value) { decalset_->SetMaxIndices(value); }

Zone^ DecalSet::zone::get() { return gcnew UrhoBackend::Zone(decalset_->GetZone()); }
bool DecalSet::AddDecal(Drawable^ A, UrhoBackend::Vector3^ B, UrhoBackend::Quaternion^ C, float D, float E, float F, UrhoBackend::Vector2^ G, UrhoBackend::Vector2^ H, float timeToLive, float normalCutoff, unsigned subGeometry)  { return  decalset_->AddDecal(A->drawable_, B->ToVector3(), C->ToQuaternion(), D, E, F, G->ToVector2(), H->ToVector2(), timeToLive, normalCutoff, subGeometry); }

void DecalSet::RemoveDecals(unsigned A)  {  decalset_->RemoveDecals(A); }

void DecalSet::RemoveAllDecals()  {  decalset_->RemoveAllDecals(); }

}
