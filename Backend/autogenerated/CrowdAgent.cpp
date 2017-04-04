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
#include "CrowdAgent.h"

#include <Urho3D/Navigation/CrowdAgent.h>
#include <Urho3D/Navigation/DetourCrowdManager.h>
#include <Urho3D/Navigation/CrowdAgent.h>
#include <Urho3D/Navigation/CrowdAgent.h>

namespace UrhoBackend {

CrowdAgent::CrowdAgent(Urho3D::CrowdAgent* fromUrho) : Component(fromUrho) { crowdagent_ = fromUrho; }
CrowdAgent::CrowdAgent(System::IntPtr^ ptr) : CrowdAgent((Urho3D::CrowdAgent*)ptr->ToPointer()) { }

unsigned CrowdAgent::navigationFilterType::get() { return crowdagent_->GetNavigationFilterType(); }
void CrowdAgent::navigationFilterType::set(unsigned value) { crowdagent_->SetNavigationFilterType(value); }

bool CrowdAgent::updateNodePosition::get() { return crowdagent_->GetUpdateNodePosition(); }
void CrowdAgent::updateNodePosition::set(bool value) { crowdagent_->SetUpdateNodePosition(value); }

float CrowdAgent::maxAccel::get() { return crowdagent_->GetMaxAccel(); }
void CrowdAgent::maxAccel::set(float value) { crowdagent_->SetMaxAccel(value); }

float CrowdAgent::maxSpeed::get() { return crowdagent_->GetMaxSpeed(); }
void CrowdAgent::maxSpeed::set(float value) { crowdagent_->SetMaxSpeed(value); }

NavigationAvoidanceQuality CrowdAgent::navigationQuality::get() { return (UrhoBackend::NavigationAvoidanceQuality)crowdagent_->GetNavigationQuality(); }
void CrowdAgent::navigationQuality::set(NavigationAvoidanceQuality value) { crowdagent_->SetNavigationQuality((Urho3D::NavigationAvoidanceQuality)value); }

NavigationPushiness CrowdAgent::navigationPushiness::get() { return (UrhoBackend::NavigationPushiness)crowdagent_->GetNavigationPushiness(); }
void CrowdAgent::navigationPushiness::set(NavigationPushiness value) { crowdagent_->SetNavigationPushiness((Urho3D::NavigationPushiness)value); }

UrhoBackend::Vector3^ CrowdAgent::desiredVelocity::get() { return gcnew UrhoBackend::Vector3(crowdagent_->GetDesiredVelocity()); }
UrhoBackend::Vector3^ CrowdAgent::actualVelocity::get() { return gcnew UrhoBackend::Vector3(crowdagent_->GetActualVelocity()); }
UrhoBackend::Vector3^ CrowdAgent::targetPosition::get() { return gcnew UrhoBackend::Vector3(crowdagent_->GetTargetPosition()); }
CrowdAgentState CrowdAgent::agentState::get() { return (UrhoBackend::CrowdAgentState)crowdagent_->GetAgentState(); }
CrowdTargetState CrowdAgent::targetState::get() { return (UrhoBackend::CrowdTargetState)crowdagent_->GetTargetState(); }
UrhoBackend::Vector3^ CrowdAgent::position::get() { return gcnew UrhoBackend::Vector3(crowdagent_->GetPosition()); }
float CrowdAgent::radius::get() { return crowdagent_->GetRadius(); }
void CrowdAgent::radius::set(float value) { crowdagent_->SetRadius(value); }

float CrowdAgent::height::get() { return crowdagent_->GetHeight(); }
void CrowdAgent::height::set(float value) { crowdagent_->SetHeight(value); }

bool CrowdAgent::SetMoveTarget(UrhoBackend::Vector3^ A)  { return  crowdagent_->SetMoveTarget(A->ToVector3()); }

bool CrowdAgent::SetMoveVelocity(UrhoBackend::Vector3^ A)  { return  crowdagent_->SetMoveVelocity(A->ToVector3()); }

}