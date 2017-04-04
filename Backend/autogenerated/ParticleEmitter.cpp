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
#include "ParticleEmitter.h"

#include <Urho3D/Graphics/ParticleEmitter.h>
#include <Urho3D/Graphics/Material.h>
#include "Material.h"
#include <Urho3D/Graphics/BillboardSet.h>
#include "Billboard.h"
#include <Urho3D/Graphics/Zone.h>
#include "Zone.h"
#include <Urho3D/Graphics/ParticleEffect.h>
#include "ParticleEffect.h"
#include <Urho3D/Graphics/GraphicsDefs.h>

namespace UrhoBackend {

ParticleEmitter::ParticleEmitter(Urho3D::ParticleEmitter* fromUrho) : Drawable(fromUrho) { particleemitter_ = fromUrho; }
ParticleEmitter::ParticleEmitter(System::IntPtr^ ptr) : ParticleEmitter((Urho3D::ParticleEmitter*)ptr->ToPointer()) { }

Material^ ParticleEmitter::material::get() { return gcnew UrhoBackend::Material(particleemitter_->GetMaterial()); }
void ParticleEmitter::material::set(Material^ value) { particleemitter_->SetMaterial(value->material_); }

unsigned ParticleEmitter::numBillboards::get() { return particleemitter_->GetNumBillboards(); }
void ParticleEmitter::numBillboards::set(unsigned value) { particleemitter_->SetNumBillboards(value); }

bool ParticleEmitter::relative::get() { return particleemitter_->IsRelative(); }
void ParticleEmitter::relative::set(bool value) { particleemitter_->SetRelative(value); }

bool ParticleEmitter::sorted::get() { return particleemitter_->IsSorted(); }
void ParticleEmitter::sorted::set(bool value) { particleemitter_->SetSorted(value); }

bool ParticleEmitter::scaled::get() { return particleemitter_->IsScaled(); }
void ParticleEmitter::scaled::set(bool value) { particleemitter_->SetScaled(value); }

FaceCameraMode ParticleEmitter::faceCameraMode::get() { return (UrhoBackend::FaceCameraMode)particleemitter_->GetFaceCameraMode(); }
void ParticleEmitter::faceCameraMode::set(FaceCameraMode value) { particleemitter_->SetFaceCameraMode((Urho3D::FaceCameraMode)value); }

float ParticleEmitter::animationLodBias::get() { return particleemitter_->GetAnimationLodBias(); }
void ParticleEmitter::animationLodBias::set(float value) { particleemitter_->SetAnimationLodBias(value); }

Billboard^ ParticleEmitter::billboards::get(unsigned A) { return gcnew UrhoBackend::Billboard(particleemitter_->GetBillboard(A)); }

Zone^ ParticleEmitter::zone::get() { return gcnew UrhoBackend::Zone(particleemitter_->GetZone()); }
ParticleEffect^ ParticleEmitter::effect::get() { return gcnew UrhoBackend::ParticleEffect(particleemitter_->GetEffect()); }
void ParticleEmitter::effect::set(ParticleEffect^ value) { particleemitter_->SetEffect(value->particleeffect_); }

unsigned ParticleEmitter::numParticles::get() { return particleemitter_->GetNumParticles(); }
void ParticleEmitter::numParticles::set(unsigned value) { particleemitter_->SetNumParticles(value); }

bool ParticleEmitter::emitting::get() { return particleemitter_->IsEmitting(); }
void ParticleEmitter::emitting::set(bool value) { particleemitter_->SetEmitting(value); }

bool ParticleEmitter::serializeParticles::get() { return particleemitter_->GetSerializeParticles(); }
void ParticleEmitter::serializeParticles::set(bool value) { particleemitter_->SetSerializeParticles(value); }

void ParticleEmitter::Commit()  {  particleemitter_->Commit(); }

void ParticleEmitter::ResetEmissionTimer()  {  particleemitter_->ResetEmissionTimer(); }

void ParticleEmitter::RemoveAllParticles()  {  particleemitter_->RemoveAllParticles(); }

void ParticleEmitter::Reset()  {  particleemitter_->Reset(); }

void ParticleEmitter::ApplyEffect()  {  particleemitter_->ApplyEffect(); }

}