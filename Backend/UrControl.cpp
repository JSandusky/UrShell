#include "stdafx.h"
#include "UrControl.h"

#include "CallbackInterfaces.h"
#include "Attributes.h"
#include "autogenerated/Graphics.h"

#include <Urho3D/Urho3D.h>

#include <Urho3D/Graphics/Camera.h>
#include <Urho3D/Core/Context.h>
#include <Urho3D/IO/Log.h>
#include <Urho3D/Engine/Engine.h>
#include <Urho3D/Input/Input.h>
#include <Urho3D/Graphics/Graphics.h>
#include <Urho3D/Graphics/GraphicsImpl.h>
#include <Urho3D/Graphics/Renderer.h>
#include <Urho3D/Resource/ResourceCache.h>
#include <Urho3D/Script/Script.h>
#include <Urho3D/Script/ScriptFile.h>

#include <Urho3D/ThirdParty/SDL/SDL.h>
#include <Urho3D/ThirdParty/SDL/SDL_video.h>

using namespace Urho3D;

const char* ToCString(System::String^ str)
{
    return (const char*)System::Runtime::InteropServices::Marshal::StringToCoTaskMemAnsi(str).ToPointer();
}

namespace UrhoBackend
{

class EventProxy : public Urho3D::Object
{
    OBJECT(EventProxy);
public:
    EventProxy(Context* context) : Object(context) 
    {
    }

    void SubscribeToEvent(Urho3D::StringHash eventType)
    {
        if (!Object::HasSubscribedToEvent(eventType))
        {
            Object::SubscribeToEvent(eventType, HANDLER(EventProxy, CheckEvent));
            subscribtionCount[eventType] = 1;
        }
        else
            subscribtionCount[eventType] = subscribtionCount[eventType] + 1;
    }

    void UnsubscribeFromEvent(Urho3D::StringHash eventType)
    {
        if (!subscribtionCount.Contains(eventType))
            return;
        if (subscribtionCount[eventType] == 0)
            Object::UnsubscribeFromEvent(eventType);
        else
            subscribtionCount[eventType] = subscribtionCount[eventType] - 1;
    }

    void CheckEvent(Urho3D::StringHash eventType, Urho3D::VariantMap& eventData)
    {
        System::Collections::Generic::List<System::Object^>^ callbacks = UrControl::GetInst()->GetEventHandlers();
        VariantMap^ wrapper = gcnew VariantMap(eventData);

        array<System::Object^>^ params = gcnew array<System::Object^>(2);
        params[0] = eventType.Value();
        params[1] = wrapper;
        for (int i = 0; i < callbacks->Count; ++i)
        {
            array<System::Reflection::MethodInfo^>^ mi = callbacks[i]->GetType()->GetMethods();
            for (int midx = 0; midx < mi->GetLength(0); ++midx)
            {
                System::Reflection::MethodInfo^ method = mi[midx];
                array<System::Object^>^ attributes = method->GetCustomAttributes(HandleEvent::typeid, false);
                for (int aidx = 0; aidx < attributes->GetLength(0); ++aidx)
                {
                    HandleEvent^ handles = (HandleEvent^)attributes[aidx];
                    if (Urho3D::StringHash(ToCString(handles->EventName)) == eventType)
                        method->Invoke(callbacks[i], params);
                }
            }
        }
    }

    Urho3D::HashMap<Urho3D::StringHash, int> subscribtionCount;
};

UrControl::UrControl(System::String^ scriptFileName) : scriptFileName_(scriptFileName)
{
    callbacks_ = gcnew System::Collections::Generic::List<System::Object^>();
    proxy_ = 0;
    inst_ = this;
    Timing = gcnew System::Timers::Timer(1000 / 30.0); //we'll run at 30fps
    Timing->Elapsed += gcnew System::Timers::ElapsedEventHandler(this, &UrhoBackend::UrControl::OnElapsed);
    Timing->Start();
    this->UseWaitCursor = false;
    this->Cursor = System::Windows::Forms::Cursors::Default;
}

UrControl::~UrControl()
{
    engine_->Exit();
}

void UrControl::Render()
{
    this->Cursor = System::Windows::Forms::Cursors::Default;
    this->UseWaitCursor = false;
    engine_->RunFrame();
    this->UseWaitCursor = false;
    this->Cursor = System::Windows::Forms::Cursors::Default;
}

void UrControl::SendEvent(System::String^ eventName, VariantMap^ varMap)
{
    const char* str = (const char*)System::Runtime::InteropServices::Marshal::StringToCoTaskMemAnsi(eventName).ToPointer();
    engine_->SendEvent(Urho3D::StringHash(str), *varMap->map_);
}

void UrControl::Focus()
{
    Input* input = engine_->GetSubsystem<Input>();
    //input->GainFocus();
}

void UrControl::SetForceFocus(bool value)
{
    Input* input = engine_->GetSubsystem<Input>();
    //input->forceFocus_ = value;
    //input->ResetState();
}

void UrControl::Unfocus()
{
    Input* input = engine_->GetSubsystem<Input>();
    //input->LoseFocus();
}

void UrControl::OnHandleCreated(System::EventArgs^ e)
{
    context_ = new Context();
    engine_ = new Engine(context_);
    Urho3D::VariantMap params;
    params["ExternalWindow"] = Urho3D::Variant(Handle.ToPointer());
    if (engine_->Initialize(params))
    {
        engine_->SetMaxFps(30);
        valid_ = true;

        context_->RegisterSubsystem(new Script(context_));
        scriptFile_ = context_->GetSubsystem<ResourceCache>()->GetResource<ScriptFile>(ToCString(scriptFileName_));
        bool success = false;
        if (scriptFile_)
        {
            if (ExecuteMethod != nullptr)
                success = scriptFile_->Execute(ToCString(ExecuteMethod), ExecuteParams != nullptr ? ExecuteParams->ToVariantVector() : Urho3D::Variant::emptyVariantVector);
            else
                success = scriptFile_->Execute("void Start()");
        }

        if (!success)
        {
            valid_ = false;
            Log* log = engine_->GetSubsystem<Log>();
            lastError_ = gcnew System::String(log->GetLastMessage().CString());
            System::Windows::Forms::MessageBox::Show(lastError_);
        }
        Input* input = engine_->GetSubsystem<Input>();
        input->SetMouseVisible(true, true);

        // Attach any pre-existing event handlers now
        if (proxy_ == 0)
        {
            this->proxy_ = new EventProxy(context_);
            for (int i = 0; i < callbacks_->Count; ++i)
            {
                SubscribeCallback(callbacks_[i]);
            }
        }
    }
    else
    {
        valid_ = false;
        Log* log = engine_->GetSubsystem<Log>();
        lastError_ = gcnew System::String(log->GetLastMessage().CString());
        System::Windows::Forms::MessageBox::Show(lastError_);
    }
    Control::OnHandleCreated(e);

    //Log* log = engine_->GetSubsystem<Log>();
    //lastError_ = gcnew System::String(log->GetLastMessage().CString());
    //valid_ = true;
    
}

void UrControl::OnPaint(System::Windows::Forms::PaintEventArgs^ e)
{
    if (this->UseWaitCursor)
    {
        this->UseWaitCursor = false;
        this->Cursor = System::Windows::Forms::Cursors::Default;
    }
    engine_->RunFrame();
    if (this->UseWaitCursor)
    {
        this->UseWaitCursor = false;
        this->Cursor = System::Windows::Forms::Cursors::Default;
    }
}

void UrControl::SubscribeToEvent(System::String^ name, System::Object^ callback)
{
    if (proxy_ != 0)
        proxy_->SubscribeToEvent(Urho3D::StringHash(ToCString(name)));
}

void UrControl::CheckEvent(Urho3D::StringHash eventType, Urho3D::VariantMap& eventData)
{

}

Graphics^ UrControl::GetGraphics()
{
    return gcnew Graphics(context_->GetSubsystem<Urho3D::Graphics>());
}

void UrControl::UnsubscribeFromEvent(System::String^ name)
{
    proxy_->UnsubscribeFromEvent(Urho3D::StringHash(ToCString(name)));
}

void UrControl::ExecuteRaw(System::String^ code)
{
    Script* scripting = context_->GetSubsystem<Script>();
    if (scripting)
        scripting->Execute(ToCString(code));
}

void UrControl::Execute(System::String^ scriptFunction)
{
    if (scriptFile_)
        scriptFile_->Execute(ToCString(scriptFunction));
}

void UrControl::Execute(System::String^ scriptFunction, VariantVector^ parameters)
{
    if (scriptFile_)
        scriptFile_->Execute(ToCString(scriptFunction), parameters->ToVariantVector());
}

void UrControl::SubscribeCallback(System::Object^ callback)
{
    if (proxy_ == 0)
    {
        callbacks_->Add(callback);
        return;
    }
    else if (!callbacks_->Contains(callback))
        callbacks_->Add(callback);
    array<System::Reflection::MethodInfo^>^ mi = callback->GetType()->GetMethods();
    for (int midx = 0; midx < mi->GetLength(0); ++midx)
    {
        System::Reflection::MethodInfo^ method = mi[midx];
        array<System::Object^>^ attributes = method->GetCustomAttributes(HandleEvent::typeid, false);
        for (int aidx = 0; aidx < attributes->GetLength(0); ++aidx)
        {
            HandleEvent^ handles = (HandleEvent^)attributes[aidx];
            if (handles != nullptr)
                SubscribeToEvent(handles->EventName, callback);
        }
    }
}

void UrControl::UnsubscribeCallback(System::Object^ callback)
{
    array<System::Reflection::MethodInfo^>^ mi = callback->GetType()->GetMethods();
    for (int midx = 0; midx < mi->GetLength(0); ++midx)
    {
        System::Reflection::MethodInfo^ method = mi[midx];
        array<System::Object^>^ attributes = method->GetCustomAttributes(HandleEvent::typeid, false);
        for (int aidx = 0; aidx < attributes->GetLength(0); ++aidx)
        {
            HandleEvent^ handles = (HandleEvent^)attributes[aidx];
            if (handles != nullptr)
                UnsubscribeFromEvent(handles->EventName);
        }
    }
}

void UrControl::Exit()
{
    engine_->Exit();
    delete engine_;
    delete context_;
}

UrhoBackend::Scene^ UrControl::GetScene()
{
    return nullptr;
}

System::Collections::Generic::Dictionary<System::String^, System::Collections::Generic::List<System::String^>^>^ UrControl::GetObjectTypes()
{
    System::Collections::Generic::Dictionary<System::String^, System::Collections::Generic::List<System::String^>^>^ ret = gcnew System::Collections::Generic::Dictionary<System::String^, System::Collections::Generic::List<System::String^>^>();
    const Urho3D::HashMap<Urho3D::String, Urho3D::Vector<Urho3D::StringHash> >& categories = context_->GetObjectCategories();
    for (Urho3D::HashMap<Urho3D::String, Urho3D::Vector<Urho3D::StringHash> >::ConstIterator cit = categories.Begin(); cit != categories.End(); ++cit)
    {
        System::Collections::Generic::List<System::String^>^ members = gcnew System::Collections::Generic::List<System::String^>();
        for (Urho3D::Vector<Urho3D::StringHash>::ConstIterator mit = cit->second_.Begin(); mit != cit->second_.End(); ++mit)
            members->Add(GetObjectTypeName(mit->Value()));
        ret[gcnew System::String(cit->first_.CString())] = members;
    }

    return ret;
}

System::String^ UrControl::GetObjectTypeName(unsigned hashCode)
{
    return gcnew System::String(context_->GetTypeName(Urho3D::StringHash(hashCode)).CString());
}

}

void UrhoBackend::UrControl::OnElapsed(System::Object ^sender, System::Timers::ElapsedEventArgs ^e)
{
    Invalidate();
}


