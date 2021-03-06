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
#include "Time.h"

#include <Urho3D/Core/Timer.h>

namespace UrhoBackend {

Time::Time(Urho3D::Time* fromUrho) : Object(fromUrho) { time_ = fromUrho; }
Time::Time(System::IntPtr^ ptr) : Time((Urho3D::Time*)ptr->ToPointer()) { }

unsigned Time::frameNumber::get() { return time_->GetFrameNumber(); }
float Time::timeStep::get() { return time_->GetTimeStep(); }
float Time::elapsedTime::get() { return time_->GetElapsedTime(); }
}
