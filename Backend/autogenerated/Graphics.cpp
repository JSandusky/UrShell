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
#include "Graphics.h"

#include <Urho3D/Graphics/Graphics.h>
#include <Urho3D/Resource/Image.h>
//#include "Image.h"

namespace UrhoBackend {

Graphics::Graphics(Urho3D::Graphics* fromUrho) { graphics_ = fromUrho; }
Graphics::Graphics(System::IntPtr^ ptr) : Graphics((Urho3D::Graphics*)ptr->ToPointer()) { }

System::String^ Graphics::windowTitle::get() { return gcnew System::String(graphics_->GetWindowTitle().CString()); }
void Graphics::windowTitle::set(System::String^ value) { graphics_->SetWindowTitle(Urho3D::String(ToCString(value))); }

System::String^ Graphics::apiName::get() { return gcnew System::String(graphics_->GetApiName().CString()); }
//void Graphics::windowIcon::set(Image^ value) { graphics_->SetWindowIcon(value->image_); }

UrhoBackend::IntVector2^ Graphics::windowPosition::get() { return gcnew UrhoBackend::IntVector2(graphics_->GetWindowPosition()); }
void Graphics::windowPosition::set(UrhoBackend::IntVector2^ value) { graphics_->SetWindowPosition(value->ToIntVector2()); }

bool Graphics::sRGB::get() { return graphics_->GetSRGB(); }
void Graphics::sRGB::set(bool value) { graphics_->SetSRGB(value); }

bool Graphics::flushGPU::get() { return graphics_->GetFlushGPU(); }
void Graphics::flushGPU::set(bool value) { graphics_->SetFlushGPU(value); }

System::String^ Graphics::orientations::get() { return gcnew System::String(graphics_->GetOrientations().CString()); }
void Graphics::orientations::set(System::String^ value) { graphics_->SetOrientations(Urho3D::String(ToCString(value))); }

int Graphics::width::get() { return graphics_->GetWidth(); }
int Graphics::height::get() { return graphics_->GetHeight(); }
int Graphics::multiSample::get() { return graphics_->GetMultiSample(); }
bool Graphics::fullscreen::get() { return graphics_->GetFullscreen(); }
bool Graphics::resizable::get() { return graphics_->GetResizable(); }
bool Graphics::borderless::get() { return graphics_->GetBorderless(); }
bool Graphics::vsync::get() { return graphics_->GetVSync(); }
bool Graphics::tripleBuffer::get() { return graphics_->GetTripleBuffer(); }
bool Graphics::initialized::get() { return graphics_->IsInitialized(); }
bool Graphics::deviceLost::get() { return graphics_->IsDeviceLost(); }
unsigned Graphics::numPrimitives::get() { return graphics_->GetNumPrimitives(); }
unsigned Graphics::numBatches::get() { return graphics_->GetNumBatches(); }
bool Graphics::instancingSupport::get() { return graphics_->GetInstancingSupport(); }
bool Graphics::lightPrepassSupport::get() { return graphics_->GetLightPrepassSupport(); }
bool Graphics::deferredSupport::get() { return graphics_->GetDeferredSupport(); }
bool Graphics::hardwareShadowSupport::get() { return graphics_->GetHardwareShadowSupport(); }
bool Graphics::readableDepthSupport::get() { return graphics_->GetReadableDepthSupport(); }
bool Graphics::sRGBSupport::get() { return graphics_->GetSRGBSupport(); }
bool Graphics::sRGBWriteSupport::get() { return graphics_->GetSRGBWriteSupport(); }
UrhoBackend::IntVector2^ Graphics::desktopResolution::get() { return gcnew UrhoBackend::IntVector2(graphics_->GetDesktopResolution()); }
bool Graphics::SetMode(int A, int B, bool C, bool D, bool E, bool F, bool G, int H)  { return  graphics_->SetMode(A, B, C, D, E, F, G, H); }

bool Graphics::SetMode(int A, int B)  { return  graphics_->SetMode(A, B); }

void Graphics::SetWindowPosition(int A, int B)  {  graphics_->SetWindowPosition(A, B); }

bool Graphics::ToggleFullscreen()  { return  graphics_->ToggleFullscreen(); }

void Graphics::Maximize()  {  graphics_->Maximize(); }

void Graphics::Minimize()  {  graphics_->Minimize(); }

void Graphics::Close()  {  graphics_->Close(); }

//bool Graphics::TakeScreenShot(Image^ A)  { return  graphics_->TakeScreenShot(A->image_); }

void Graphics::BeginDumpShaders(System::String^ A)  {  graphics_->BeginDumpShaders(Urho3D::String(ToCString(A))); }

void Graphics::EndDumpShaders()  {  graphics_->EndDumpShaders(); }

}