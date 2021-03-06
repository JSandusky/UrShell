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
#include "UI.h"

#include <Urho3D/UI/UI.h>
#include <Urho3D/UI/UIElement.h>
#include "UIElement.h"
#include <Urho3D/UI/Cursor.h>
#include "Cursor.h"

namespace UrhoBackend {

UI::UI(Urho3D::UI* fromUrho) : Object(fromUrho) { ui_ = fromUrho; }
UI::UI(System::IntPtr^ ptr) : UI((Urho3D::UI*)ptr->ToPointer()) { }

Cursor^ UI::cursor::get() { return gcnew UrhoBackend::Cursor(ui_->GetCursor()); }
void UI::cursor::set(Cursor^ value) { ui_->SetCursor(value->cursor_); }

UrhoBackend::IntVector2^ UI::cursorPosition::get() { return gcnew UrhoBackend::IntVector2(ui_->GetCursorPosition()); }
UIElement^ UI::focusElement::get() { return gcnew UrhoBackend::UIElement(ui_->GetFocusElement()); }
UIElement^ UI::frontElement::get() { return gcnew UrhoBackend::UIElement(ui_->GetFrontElement()); }
UIElement^ UI::root::get() { return gcnew UrhoBackend::UIElement(ui_->GetRoot()); }
UIElement^ UI::modalRoot::get() { return gcnew UrhoBackend::UIElement(ui_->GetRootModalElement()); }
System::String^ UI::clipBoardText::get() { return gcnew System::String(ui_->GetClipboardText().CString()); }
void UI::clipBoardText::set(System::String^ value) { ui_->SetClipboardText(Urho3D::String(ToCString(value))); }

float UI::doubleClickInterval::get() { return ui_->GetDoubleClickInterval(); }
void UI::doubleClickInterval::set(float value) { ui_->SetDoubleClickInterval(value); }

float UI::dragBeginInterval::get() { return ui_->GetDragBeginInterval(); }
void UI::dragBeginInterval::set(float value) { ui_->SetDragBeginInterval(value); }

int UI::dragBeginDistance::get() { return ui_->GetDragBeginDistance(); }
void UI::dragBeginDistance::set(int value) { ui_->SetDragBeginDistance(value); }

float UI::defaultToolTipDelay::get() { return ui_->GetDefaultToolTipDelay(); }
void UI::defaultToolTipDelay::set(float value) { ui_->SetDefaultToolTipDelay(value); }

int UI::maxFontTextureSize::get() { return ui_->GetMaxFontTextureSize(); }
void UI::maxFontTextureSize::set(int value) { ui_->SetMaxFontTextureSize(value); }

bool UI::nonFocusedMouseWheel::get() { return ui_->IsNonFocusedMouseWheel(); }
void UI::nonFocusedMouseWheel::set(bool value) { ui_->SetNonFocusedMouseWheel(value); }

bool UI::useSystemClipboard::get() { return ui_->GetUseSystemClipboard(); }
void UI::useSystemClipboard::set(bool value) { ui_->SetUseSystemClipboard(value); }

bool UI::useScreenKeyboard::get() { return ui_->GetUseScreenKeyboard(); }
void UI::useScreenKeyboard::set(bool value) { ui_->SetUseScreenKeyboard(value); }

bool UI::useMutableGlyphs::get() { return ui_->GetUseMutableGlyphs(); }
void UI::useMutableGlyphs::set(bool value) { ui_->SetUseMutableGlyphs(value); }

bool UI::forceAutoHint::get() { return ui_->GetForceAutoHint(); }
void UI::forceAutoHint::set(bool value) { ui_->SetForceAutoHint(value); }

void UI::Clear()  {  ui_->Clear(); }

void UI::DebugDraw(UIElement^ A)  {  ui_->DebugDraw(A->uielement_); }

void UI::SetFocusElement(UIElement^ A, bool byKey)  {  ui_->SetFocusElement(A->uielement_, byKey); }

UIElement^ UI::GetElementAt(UrhoBackend::IntVector2^ A, bool activeOnly)  { return  gcnew UrhoBackend::UIElement(ui_->GetElementAt(A->ToIntVector2(), activeOnly)); }

UIElement^ UI::GetElementAt(int A, int B, bool activeOnly)  { return  gcnew UrhoBackend::UIElement(ui_->GetElementAt(A, B, activeOnly)); }

bool UI::HasModalElement()  { return  ui_->HasModalElement(); }

bool UI::IsDragging()  { return  ui_->IsDragging(); }

}
