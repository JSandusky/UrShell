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
#include "TextureCube.h"

#include <Urho3D/Graphics/TextureCube.h>
#include <Urho3D/Graphics/RenderSurface.h>
#include "RenderSurface.h"
#include <Urho3D/Graphics/GraphicsDefs.h>
#include <Urho3D/Graphics/GraphicsDefs.h>

namespace UrhoBackend {

TextureCube::TextureCube(Urho3D::TextureCube* fromUrho) : Texture(fromUrho) { texturecube_ = fromUrho; }
TextureCube::TextureCube(System::IntPtr^ ptr) : TextureCube((Urho3D::TextureCube*)ptr->ToPointer()) { }

RenderSurface^ TextureCube::renderSurfaces::get(CubeMapFace A) { return gcnew UrhoBackend::RenderSurface(texturecube_->GetRenderSurface((Urho3D::CubeMapFace)A)); }

bool TextureCube::SetSize(int A, unsigned B, TextureUsage usage)  { return  texturecube_->SetSize(A, B, (Urho3D::TextureUsage)usage); }

}