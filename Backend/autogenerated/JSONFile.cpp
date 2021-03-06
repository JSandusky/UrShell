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
#include "JSONFile.h"

#include <Urho3D/Resource/JSONFile.h>
#include <Urho3D/Resource/JSONValue.h>
#include "JSONValue.h"
#include <Urho3D/Resource/JSONValue.h>

namespace UrhoBackend {

JSONFile::JSONFile(Urho3D::JSONFile* fromUrho) : Resource(fromUrho) { jsonfile_ = fromUrho; }
JSONFile::JSONFile(System::IntPtr^ ptr) : JSONFile((Urho3D::JSONFile*)ptr->ToPointer()) { }

JSONValue^ JSONFile::CreateRoot(JSONValueType valueType)  { return  gcnew UrhoBackend::JSONValue(jsonfile_->CreateRoot((Urho3D::JSONValueType)valueType)); }

JSONValue^ JSONFile::GetRoot(JSONValueType valueType)  { return  gcnew UrhoBackend::JSONValue(jsonfile_->GetRoot((Urho3D::JSONValueType)valueType)); }

}
