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
#include "Sprite.h"

#include <Urho3D/UI/Sprite.h>
#include <Urho3D/Graphics/Texture.h>
#include "Texture.h"
#include <Urho3D/Graphics/GraphicsDefs.h>

namespace UrhoBackend {

Sprite::Sprite(Urho3D::Sprite* fromUrho) : UIElement(fromUrho) { sprite_ = fromUrho; }
Sprite::Sprite(System::IntPtr^ ptr) : Sprite((Urho3D::Sprite*)ptr->ToPointer()) { }

UrhoBackend::IntVector2^ Sprite::hotSpot::get() { return gcnew UrhoBackend::IntVector2(sprite_->GetHotSpot()); }
void Sprite::hotSpot::set(UrhoBackend::IntVector2^ value) { sprite_->SetHotSpot(value->ToIntVector2()); }

UrhoBackend::Vector2^ Sprite::scale::get() { return gcnew UrhoBackend::Vector2(sprite_->GetScale()); }
void Sprite::scale::set(UrhoBackend::Vector2^ value) { sprite_->SetScale(value->ToVector2()); }

float Sprite::rotation::get() { return sprite_->GetRotation(); }
void Sprite::rotation::set(float value) { sprite_->SetRotation(value); }

Texture^ Sprite::texture::get() { return gcnew UrhoBackend::Texture(sprite_->GetTexture()); }
void Sprite::texture::set(Texture^ value) { sprite_->SetTexture(value->texture_); }

UrhoBackend::IntRect^ Sprite::imageRect::get() { return gcnew UrhoBackend::IntRect(sprite_->GetImageRect()); }
void Sprite::imageRect::set(UrhoBackend::IntRect^ value) { sprite_->SetImageRect(value->ToIntRect()); }

BlendMode Sprite::blendMode::get() { return (UrhoBackend::BlendMode)sprite_->GetBlendMode(); }
void Sprite::blendMode::set(BlendMode value) { sprite_->SetBlendMode((Urho3D::BlendMode)value); }

void Sprite::SetHotSpot(int A, int B)  {  sprite_->SetHotSpot(A, B); }

void Sprite::SetScale(float A, float B)  {  sprite_->SetScale(A, B); }

void Sprite::SetScale(float A)  {  sprite_->SetScale(A); }

void Sprite::SetFullImageRect()  {  sprite_->SetFullImageRect(); }

}
