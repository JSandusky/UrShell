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
#include "Log.h"

#include <Urho3D/IO/Log.h>

namespace UrhoBackend {

Log::Log(Urho3D::Log* fromUrho) : Object(fromUrho) { log_ = fromUrho; }
Log::Log(System::IntPtr^ ptr) : Log((Urho3D::Log*)ptr->ToPointer()) { }

int Log::level::get() { return log_->GetLevel(); }
void Log::level::set(int value) { log_->SetLevel(value); }

bool Log::timeStamp::get() { return log_->GetTimeStamp(); }
void Log::timeStamp::set(bool value) { log_->SetTimeStamp(value); }

System::String^ Log::lastMessage::get() { return gcnew System::String(log_->GetLastMessage().CString()); }
bool Log::quiet::get() { return log_->IsQuiet(); }
void Log::quiet::set(bool value) { log_->SetQuiet(value); }

void Log::Open(System::String^ A)  {  log_->Open(Urho3D::String(ToCString(A))); }

void Log::Close()  {  log_->Close(); }

}