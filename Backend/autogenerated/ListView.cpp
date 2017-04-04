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
#include "ListView.h"

#include <Urho3D/UI/ListView.h>
#include <Urho3D/UI/UIElement.h>
#include "UIElement.h"
#include <Urho3D/UI/ScrollBar.h>
#include "ScrollBar.h"
#include <Urho3D/UI/BorderImage.h>
#include "BorderImage.h"
#include <Urho3D/UI/ListView.h>

namespace UrhoBackend {

ListView::ListView(Urho3D::ListView* fromUrho) : UIElement(fromUrho) { listview_ = fromUrho; }
ListView::ListView(System::IntPtr^ ptr) : ListView((Urho3D::ListView*)ptr->ToPointer()) { }

UrhoBackend::IntVector2^ ListView::viewPosition::get() { return gcnew UrhoBackend::IntVector2(listview_->GetViewPosition()); }
void ListView::viewPosition::set(UrhoBackend::IntVector2^ value) { listview_->SetViewPosition(value->ToIntVector2()); }

UIElement^ ListView::contentElement::get() { return gcnew UrhoBackend::UIElement(listview_->GetContentElement()); }
ScrollBar^ ListView::horizontalScrollBar::get() { return gcnew UrhoBackend::ScrollBar(listview_->GetHorizontalScrollBar()); }
ScrollBar^ ListView::verticalScrollBar::get() { return gcnew UrhoBackend::ScrollBar(listview_->GetVerticalScrollBar()); }
BorderImage^ ListView::scrollPanel::get() { return gcnew UrhoBackend::BorderImage(listview_->GetScrollPanel()); }
bool ListView::scrollBarsAutoVisible::get() { return listview_->GetScrollBarsAutoVisible(); }
void ListView::scrollBarsAutoVisible::set(bool value) { listview_->SetScrollBarsAutoVisible(value); }

float ListView::scrollStep::get() { return listview_->GetScrollStep(); }
void ListView::scrollStep::set(float value) { listview_->SetScrollStep(value); }

float ListView::pageStep::get() { return listview_->GetPageStep(); }
void ListView::pageStep::set(float value) { listview_->SetPageStep(value); }

float ListView::scrollDeceleration::get() { return listview_->GetScrollDeceleration(); }
void ListView::scrollDeceleration::set(float value) { listview_->SetScrollDeceleration(value); }

float ListView::scrollSnapEpsilon::get() { return listview_->GetScrollSnapEpsilon(); }
void ListView::scrollSnapEpsilon::set(float value) { listview_->SetScrollSnapEpsilon(value); }

bool ListView::autoDisableChildren::get() { return listview_->GetAutoDisableChildren(); }
void ListView::autoDisableChildren::set(bool value) { listview_->SetAutoDisableChildren(value); }

float ListView::autoDisableThreshold::get() { return listview_->GetAutoDisableThreshold(); }
void ListView::autoDisableThreshold::set(float value) { listview_->SetAutoDisableThreshold(value); }

unsigned ListView::numItems::get() { return listview_->GetNumItems(); }
UIElement^ ListView::items::get(unsigned A) { return gcnew UrhoBackend::UIElement(listview_->GetItem(A)); }

unsigned ListView::selection::get() { return listview_->GetSelection(); }
void ListView::selection::set(unsigned value) { listview_->SetSelection(value); }

UIElement^ ListView::selectedItem::get() { return gcnew UrhoBackend::UIElement(listview_->GetSelectedItem()); }
HighlightMode ListView::highlightMode::get() { return (UrhoBackend::HighlightMode)listview_->GetHighlightMode(); }
void ListView::highlightMode::set(HighlightMode value) { listview_->SetHighlightMode((Urho3D::HighlightMode)value); }

bool ListView::multiselect::get() { return listview_->GetMultiselect(); }
void ListView::multiselect::set(bool value) { listview_->SetMultiselect(value); }

bool ListView::hierarchyMode::get() { return listview_->GetHierarchyMode(); }
void ListView::hierarchyMode::set(bool value) { listview_->SetHierarchyMode(value); }

int ListView::baseIndent::get() { return listview_->GetBaseIndent(); }
void ListView::baseIndent::set(int value) { listview_->SetBaseIndent(value); }

bool ListView::clearSelectionOnDefocus::get() { return listview_->GetClearSelectionOnDefocus(); }
void ListView::clearSelectionOnDefocus::set(bool value) { listview_->SetClearSelectionOnDefocus(value); }

bool ListView::selectOnClickEnd::get() { return listview_->GetSelectOnClickEnd(); }
void ListView::selectOnClickEnd::set(bool value) { listview_->SetSelectOnClickEnd(value); }

void ListView::SetViewPosition(int A, int B)  {  listview_->SetViewPosition(A, B); }

void ListView::SetScrollBarsVisible(bool A, bool B)  {  listview_->SetScrollBarsVisible(A, B); }

void ListView::AddItem(UIElement^ A)  {  listview_->AddItem(A->uielement_); }

void ListView::InsertItem(unsigned A, UIElement^ B, UIElement^ arg2)  {  listview_->InsertItem(A, B->uielement_, arg2->uielement_); }

void ListView::RemoveItem(UIElement^ A, unsigned index)  {  listview_->RemoveItem(A->uielement_, index); }

void ListView::RemoveItem(unsigned A)  {  listview_->RemoveItem(A); }

void ListView::RemoveAllItems()  {  listview_->RemoveAllItems(); }

void ListView::AddSelection(unsigned A)  {  listview_->AddSelection(A); }

void ListView::RemoveSelection(unsigned A)  {  listview_->RemoveSelection(A); }

void ListView::ToggleSelection(unsigned A)  {  listview_->ToggleSelection(A); }

void ListView::ChangeSelection(int A, bool B)  {  listview_->ChangeSelection(A, B); }

void ListView::ClearSelection()  {  listview_->ClearSelection(); }

void ListView::CopySelectedItemsToClipboard()  {  listview_->CopySelectedItemsToClipboard(); }

void ListView::Expand(unsigned A, bool B, bool arg2)  {  listview_->Expand(A, B, arg2); }

void ListView::ToggleExpand(unsigned A, bool arg1)  {  listview_->ToggleExpand(A, arg1); }

bool ListView::IsSelected(unsigned A)  { return  listview_->IsSelected(A); }

bool ListView::IsExpanded(unsigned A)  { return  listview_->IsExpanded(A); }

unsigned ListView::FindItem(UIElement^ A)  { return  listview_->FindItem(A->uielement_); }

}