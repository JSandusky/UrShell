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
#include "Technique.h"

#include <Urho3D/Graphics/Technique.h>
#include <Urho3D/Graphics/Technique.h>
#include "Pass.h"

namespace UrhoBackend {

Technique::Technique(Urho3D::Technique* fromUrho) : Resource(fromUrho) { technique_ = fromUrho; }
Technique::Technique(System::IntPtr^ ptr) : Technique((Urho3D::Technique*)ptr->ToPointer()) { }

bool Technique::supported::get() { return technique_->IsSupported(); }
bool Technique::desktop::get() { return technique_->IsDesktop(); }
void Technique::desktop::set(bool value) { technique_->SetIsDesktop(value); }

unsigned Technique::numPasses::get() { return technique_->GetNumPasses(); }
Pass^ Technique::CreatePass(System::String^ A)  { return  gcnew UrhoBackend::Pass(technique_->CreatePass(Urho3D::String(ToCString(A)))); }

void Technique::RemovePass(System::String^ A)  {  technique_->RemovePass(Urho3D::String(ToCString(A))); }

bool Technique::HasPass(System::String^ A)  { return  technique_->HasPass(Urho3D::String(ToCString(A))); }

Pass^ Technique::GetPass(System::String^ A)  { return  gcnew UrhoBackend::Pass(technique_->GetPass(Urho3D::String(ToCString(A)))); }

Pass^ Technique::GetSupportedPass(System::String^ A)  { return  gcnew UrhoBackend::Pass(technique_->GetSupportedPass(Urho3D::String(ToCString(A)))); }

}