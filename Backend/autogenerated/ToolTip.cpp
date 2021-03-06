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
#include "ToolTip.h"

#include <Urho3D/UI/ToolTip.h>

namespace UrhoBackend {

ToolTip::ToolTip(Urho3D::ToolTip* fromUrho) : UIElement(fromUrho) { tooltip_ = fromUrho; }
ToolTip::ToolTip(System::IntPtr^ ptr) : ToolTip((Urho3D::ToolTip*)ptr->ToPointer()) { }

float ToolTip::delay::get() { return tooltip_->GetDelay(); }
void ToolTip::delay::set(float value) { tooltip_->SetDelay(value); }

}
