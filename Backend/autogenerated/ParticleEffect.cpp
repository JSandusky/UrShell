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
#include "../XML.h"
#include "../UrControl.h"
#include "../XML.h"
#include "ParticleEffect.h"

#include <Urho3D/Graphics/ParticleEffect.h>
#include <Urho3D/Graphics/ParticleEffect.h>
#include "ColorFrame.h"
#include <Urho3D/Graphics/ParticleEffect.h>
#include "TextureFrame.h"
#include <Urho3D/Graphics/Material.h>
#include <Urho3D/Graphics/ParticleEffect.h>
#include <Urho3D/Resource/ResourceCache.h>
#include <Urho3D/Resource/XMLFile.h>

namespace UrhoBackend {

ParticleEffect::ParticleEffect(Urho3D::ParticleEffect* fromUrho) : UObject(fromUrho) { particleeffect_ = fromUrho; }
ParticleEffect::ParticleEffect(System::IntPtr ptr) : ParticleEffect((Urho3D::ParticleEffect*)ptr.ToPointer()) 
{ 
    colorFrames_ =   gcnew UrhoBackend::ParentedList<UrhoBackend::ColorFrame^>(this);
    textureFrames_ = gcnew UrhoBackend::ParentedList<UrhoBackend::TextureFrame^>(this);

    FillFrames();
}

System::String^ ParticleEffect::Material::get() { 
    Urho3D::Material* mat = particleeffect_->GetMaterial();
    if (mat != 0)
    {
        return gcnew System::String(mat->GetName().CString());
    }
    return gcnew System::String(""); 
}

void ParticleEffect::Material::set(System::String^ value) { 
    Urho3D::Material* mat = particleeffect_->GetSubsystem<Urho3D::ResourceCache>()->GetResource<Urho3D::Material>(ToCString(value));
    if (mat != 0x0)
        particleeffect_->SetMaterial(mat);
    PropertyChange("Material");
}

unsigned ParticleEffect::NumParticles::get() { return particleeffect_->GetNumParticles(); }
void ParticleEffect::NumParticles::set(unsigned value) { particleeffect_->SetNumParticles(value); PropertyChange("NumParticles"); }

bool ParticleEffect::UpdateInvisible::get() { return particleeffect_->GetUpdateInvisible(); }
void ParticleEffect::UpdateInvisible::set(bool value) { particleeffect_->SetUpdateInvisible(value); PropertyChange("UpdateInvisible"); }

bool ParticleEffect::Relative::get() { return particleeffect_->IsRelative(); }
void ParticleEffect::Relative::set(bool value) {
    particleeffect_->SetRelative(value); PropertyChange("Relative");
}

bool ParticleEffect::Sorted::get() { return particleeffect_->IsSorted(); }
void ParticleEffect::Sorted::set(bool value) {
    particleeffect_->SetSorted(value); PropertyChange("Sorted");
}

bool ParticleEffect::Scaled::get() { return particleeffect_->IsScaled(); }
void ParticleEffect::Scaled::set(bool value) {
    particleeffect_->SetScaled(value); PropertyChange("Scaled");
}

float ParticleEffect::AnimationLodBias::get() { return particleeffect_->GetAnimationLodBias(); }
void ParticleEffect::AnimationLodBias::set(float value) {
    particleeffect_->SetAnimationLodBias(value); PropertyChange("AnimationLodBias");
}

EmitterType ParticleEffect::EmissionType::get() { return (UrhoBackend::EmitterType)particleeffect_->GetEmitterType(); }
void ParticleEffect::EmissionType::set(EmitterType value) {
    particleeffect_->SetEmitterType((Urho3D::EmitterType)value); PropertyChange("EmissionType");
}

UrhoBackend::Vector3^ ParticleEffect::EmitterSize::get() { return gcnew UrhoBackend::Vector3(particleeffect_->GetEmitterSize()); }
void ParticleEffect::EmitterSize::set(UrhoBackend::Vector3^ value) {
    particleeffect_->SetEmitterSize(value->ToVector3()); PropertyChange("EmitterSize");
}

UrhoBackend::Vector3^ ParticleEffect::MinDirection::get() { return gcnew UrhoBackend::Vector3(particleeffect_->GetMinDirection()); }
void ParticleEffect::MinDirection::set(UrhoBackend::Vector3^ value) {
    particleeffect_->SetMinDirection(value->ToVector3()); PropertyChange("MinDirection");
}

UrhoBackend::Vector3^ ParticleEffect::MaxDirection::get() { return gcnew UrhoBackend::Vector3(particleeffect_->GetMaxDirection()); }
void ParticleEffect::MaxDirection::set(UrhoBackend::Vector3^ value) {
    particleeffect_->SetMaxDirection(value->ToVector3()); PropertyChange("MaxDirection");
}

UrhoBackend::Vector3^ ParticleEffect::ConstantForce::get() { return gcnew UrhoBackend::Vector3(particleeffect_->GetConstantForce()); }
void ParticleEffect::ConstantForce::set(UrhoBackend::Vector3^ value) {
    particleeffect_->SetConstantForce(value->ToVector3()); PropertyChange("ConstantForce");
}

float ParticleEffect::DampingForce::get() { return particleeffect_->GetDampingForce(); }
void ParticleEffect::DampingForce::set(float value) {
    particleeffect_->SetDampingForce(value); PropertyChange("DampingForce");
}

float ParticleEffect::ActiveTime::get() { return particleeffect_->GetActiveTime(); }
void ParticleEffect::ActiveTime::set(float value) {
    particleeffect_->SetActiveTime(value); PropertyChange("ActiveTime");
}

float ParticleEffect::InactiveTime::get() { return particleeffect_->GetInactiveTime(); }
void ParticleEffect::InactiveTime::set(float value) {
    particleeffect_->SetInactiveTime(value); PropertyChange("InactiveTime");
}

float ParticleEffect::MinEmissionRate::get() { return particleeffect_->GetMinEmissionRate(); }
void ParticleEffect::MinEmissionRate::set(float value) {
    particleeffect_->SetMinEmissionRate(value); PropertyChange("MinEmissionRate");
}

float ParticleEffect::MaxEmissionRate::get() { return particleeffect_->GetMaxEmissionRate(); }
void ParticleEffect::MaxEmissionRate::set(float value) {
    particleeffect_->SetMaxEmissionRate(value); PropertyChange("MaxEmissionRate");
}

UrhoBackend::Vector2^ ParticleEffect::MinParticleSize::get() { return gcnew UrhoBackend::Vector2(particleeffect_->GetMinParticleSize()); }
void ParticleEffect::MinParticleSize::set(UrhoBackend::Vector2^ value) {
    particleeffect_->SetMinParticleSize(value->ToVector2()); PropertyChange("MinParticleSize");
}

UrhoBackend::Vector2^ ParticleEffect::MaxParticleSize::get() { return gcnew UrhoBackend::Vector2(particleeffect_->GetMaxParticleSize()); }
void ParticleEffect::MaxParticleSize::set(UrhoBackend::Vector2^ value) {
    particleeffect_->SetMaxParticleSize(value->ToVector2()); PropertyChange("MaxParticleSize");
}

float ParticleEffect::MinTimeToLive::get() { return particleeffect_->GetMinTimeToLive(); }
void ParticleEffect::MinTimeToLive::set(float value) {
    particleeffect_->SetMinTimeToLive(value); PropertyChange("MinTimeToLive");
}

float ParticleEffect::MaxTimeToLive::get() { return particleeffect_->GetMaxTimeToLive(); }
void ParticleEffect::MaxTimeToLive::set(float value) {
    particleeffect_->SetMaxTimeToLive(value); PropertyChange("MaxTimeToLive");
}

float ParticleEffect::MinVelocity::get() { return particleeffect_->GetMinVelocity(); }
void ParticleEffect::MinVelocity::set(float value) {
    particleeffect_->SetMinVelocity(value); PropertyChange("MinVelocity");
}

float ParticleEffect::MaxVelocity::get() { return particleeffect_->GetMaxVelocity(); }
void ParticleEffect::MaxVelocity::set(float value) {
    particleeffect_->SetMaxVelocity(value); PropertyChange("MaxVelocity");
}

float ParticleEffect::MinRotation::get() { return particleeffect_->GetMinRotation(); }
void ParticleEffect::MinRotation::set(float value) {
    particleeffect_->SetMinRotation(value); PropertyChange("MinRotation");
}

float ParticleEffect::MaxRotation::get() { return particleeffect_->GetMaxRotation(); }
void ParticleEffect::MaxRotation::set(float value) {
    particleeffect_->SetMaxRotation(value); PropertyChange("MaxRotation");
}

float ParticleEffect::MinRotationSpeed::get() { return particleeffect_->GetMinRotationSpeed(); }
void ParticleEffect::MinRotationSpeed::set(float value) {
    particleeffect_->SetMinRotationSpeed(value); PropertyChange("MinRotationSpeed");
}

float ParticleEffect::MaxRotationSpeed::get() { return particleeffect_->GetMaxRotationSpeed(); }
void ParticleEffect::MaxRotationSpeed::set(float value) {
    particleeffect_->SetMaxRotationSpeed(value); PropertyChange("MaxRotationSpeed");
}

float ParticleEffect::SizeAdd::get() { return particleeffect_->GetSizeAdd(); }
void ParticleEffect::SizeAdd::set(float value) {
    particleeffect_->SetSizeAdd(value); PropertyChange("SizeAdd");
}

float ParticleEffect::SizeMul::get() { return particleeffect_->GetSizeMul(); }
void ParticleEffect::SizeMul::set(float value) {
    particleeffect_->SetSizeMul(value); PropertyChange("SizeMul");
}

unsigned ParticleEffect::NumColorFrames::get() { return particleeffect_->GetNumColorFrames(); }
void ParticleEffect::NumColorFrames::set(unsigned value) {
    particleeffect_->SetNumColorFrames(value); PropertyChange("NumColorFrames");
}

unsigned ParticleEffect::NumTextureFrames::get() { return particleeffect_->GetNumTextureFrames(); }
void ParticleEffect::NumTextureFrames::set(unsigned value) { particleeffect_->SetNumTextureFrames(value); }

void ParticleEffect::AddColorTime(UrhoBackend::Color^ A, float B)  {  
    particleeffect_->AddColorTime(A->ToColor(), B); 
    FillFrames();
}

void ParticleEffect::AddColorFrame(ColorFrame^ A)  { particleeffect_->AddColorFrame(*A->colorframe_); }

void ParticleEffect::SortColorFrames()  { particleeffect_->SortColorFrames(); FillFrames(); }

void ParticleEffect::RemoveColorFrame(unsigned A)  { particleeffect_->RemoveColorFrame(A); if (A < ColorFrames->Count) ColorFrames->RemoveAt(A); }

void ParticleEffect::SetColorFrame(unsigned A, ColorFrame^ B)  { particleeffect_->SetColorFrame(A, *B->colorframe_); }

ColorFrame^ ParticleEffect::GetColorFrame(unsigned A)  { return  gcnew UrhoBackend::ColorFrame(*particleeffect_->GetColorFrame(A), this); }

void ParticleEffect::AddTextureTime(Rect^ A, float B)  {  particleeffect_->AddTextureTime(A->ToRect(), B); }

void ParticleEffect::AddTextureFrame(TextureFrame^ A)  { particleeffect_->AddTextureFrame(*A->textureframe_); }

void ParticleEffect::SortTextureFrames()  {  particleeffect_->SortTextureFrames(); }

void ParticleEffect::RemoveTextureFrame(unsigned A)  { particleeffect_->RemoveTextureFrame(A); TextureFrames->RemoveAt(A); }

void ParticleEffect::SetTextureFrame(unsigned A, TextureFrame^ B)  { particleeffect_->SetTextureFrame(A, *B->textureframe_); }

TextureFrame^ ParticleEffect::GetTextureFrame(unsigned A)  { return  gcnew UrhoBackend::TextureFrame(*particleeffect_->GetTextureFrame(A), this); }

void ParticleEffect::FillFrames()
{
    colorFrames_->Clear();
    textureFrames_->Clear();
    for (int i = 0; i < this->NumColorFrames; ++i)
        colorFrames_->Add(this->GetColorFrame(i));

    for (int i = 0; i < NumTextureFrames; ++i)
        textureFrames_->Add(this->GetTextureFrame(i));
}

void ParticleEffect::UpdateFrames()
{
    for (int i = 0; i < NumColorFrames; ++i)
        SetColorFrame(i, ColorFrames[i]);
    for (int i = 0; i < NumTextureFrames; ++i)
        SetTextureFrame(i, TextureFrames[i]);
}

ColorFrame^ ParticleEffect::InsertColorFrame()
{
    colorFrames_->Add(gcnew ColorFrame(Urho3D::ColorFrame(Urho3D::Color::WHITE, 0.0f), this));
    AddColorFrame(colorFrames_[colorFrames_->Count - 1]);
    return colorFrames_[colorFrames_->Count - 1];
}

TextureFrame^ ParticleEffect::InsertTextureFrame()
{
    textureFrames_->Add(gcnew TextureFrame(Urho3D::TextureFrame(), this));
    AddTextureFrame(textureFrames_[textureFrames_->Count - 1]);
    return textureFrames_[textureFrames_->Count - 1];
}

UrhoBackend::XMLFile^ ParticleEffect::Save()
{
    Urho3D::XMLFile* file = new Urho3D::XMLFile(particleeffect_->GetContext());
    Urho3D::XMLElement root = file->CreateRoot("particleeffect");
    particleeffect_->Save(root);
    return gcnew XMLFile(file);
}

}