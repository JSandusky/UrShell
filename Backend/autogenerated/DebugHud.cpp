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
#include "DebugHud.h"

#include <Urho3D/Engine/DebugHud.h>
#include <Urho3D/Resource/XMLFile.h>
#include "XMLFile.h"
#include <Urho3D/Graphics/Texture.h>
#include "Text.h"

namespace UrhoBackend {

DebugHud::DebugHud(Urho3D::DebugHud* fromUrho) : Object(fromUrho) { debughud_ = fromUrho; }
DebugHud::DebugHud(System::IntPtr^ ptr) : DebugHud((Urho3D::DebugHud*)ptr->ToPointer()) { }

XMLFile^ DebugHud::defaultStyle::get() { return gcnew UrhoBackend::XMLFile(debughud_->GetDefaultStyle()); }
void DebugHud::defaultStyle::set(XMLFile^ value) { debughud_->SetDefaultStyle(value->xmlfile_); }

unsigned DebugHud::mode::get() { return debughud_->GetMode(); }
void DebugHud::mode::set(unsigned value) { debughud_->SetMode(value); }

unsigned DebugHud::profilerMaxDepth::get() { return debughud_->GetProfilerMaxDepth(); }
void DebugHud::profilerMaxDepth::set(unsigned value) { debughud_->SetProfilerMaxDepth(value); }

float DebugHud::profilerInterval::get() { return debughud_->GetProfilerInterval(); }
void DebugHud::profilerInterval::set(float value) { debughud_->SetProfilerInterval(value); }

bool DebugHud::useRendererStats::get() { return debughud_->GetUseRendererStats(); }
void DebugHud::useRendererStats::set(bool value) { debughud_->SetUseRendererStats(value); }

Text^ DebugHud::statsText::get() { return gcnew UrhoBackend::Text(debughud_->GetStatsText()); }
Text^ DebugHud::modeText::get() { return gcnew UrhoBackend::Text(debughud_->GetModeText()); }
Text^ DebugHud::profilerText::get() { return gcnew UrhoBackend::Text(debughud_->GetProfilerText()); }
void DebugHud::Update()  {  debughud_->Update(); }

void DebugHud::Toggle(unsigned A)  {  debughud_->Toggle(A); }

void DebugHud::ToggleAll()  {  debughud_->ToggleAll(); }

void DebugHud::SetAppStats(System::String^ A, Variant^ B)  {  debughud_->SetAppStats(Urho3D::String(ToCString(A)), *B->variant_); }

void DebugHud::SetAppStats(System::String^ A, System::String^ B)  {  debughud_->SetAppStats(Urho3D::String(ToCString(A)), Urho3D::String(ToCString(B))); }

void DebugHud::ResetAppStats(System::String^ A)  {  debughud_->ResetAppStats(Urho3D::String(ToCString(A))); }

void DebugHud::ClearAppStats()  {  debughud_->ClearAppStats(); }

}