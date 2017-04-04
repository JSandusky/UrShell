#pragma once

#include "Variant.h"
#using <mscorlib.dll>
#using <System.dll>
#using <System.Drawing.dll>
#using <System.Windows.Forms.dll>

const char* ToCString(System::String^ str);

namespace Urho3D
{
    class Engine;
    class Context;
    class ScriptFile;
}

namespace UrhoBackend
{
ref class UObject;
ref class Graphics;
class EventProxy;

public ref class UrControl : public System::Windows::Forms::Control
{
public:
    UrControl(System::String^ scriptFileName);
    ~UrControl();

    static void DestroyInst() { inst_ = nullptr; }

    bool Valid() { return valid_; }

    Urho3D::Context* GetContext() { return context_; }
    System::String^ GetLastError() { return lastError_; }

    virtual void OnPaint(System::Windows::Forms::PaintEventArgs^ e) override;
    virtual void OnHandleCreated(System::EventArgs^ e) override;

// General functionality
    void Render();
    void Focus();
    void SetForceFocus(bool value);
    void Unfocus();
    void Exit();

// Utility
    System::Timers::Timer^ Timing;
    static UrControl^ GetInst() { return inst_; }
    UrhoBackend::Scene^ GetScene();

// Typing
    System::Collections::Generic::Dictionary<System::String^, System::Collections::Generic::List<System::String^>^>^ GetObjectTypes();
    System::String^ GetObjectTypeName(unsigned hashCode);

// Scripting
    property System::String^ ExecuteMethod;
    property UrhoBackend::VariantVector^ ExecuteParams;
    void ExecuteRaw(System::String^ code);
    void Execute(System::String^ scriptFunction);
    void Execute(System::String^ scriptFunction, VariantVector^ parameters);

// Event subscriptions
    void SubscribeCallback(System::Object^ callback);
    void UnsubscribeCallback(System::Object^ callback);
    System::Collections::Generic::List<System::Object^>^ GetEventHandlers() { return callbacks_; }
    void SendEvent(System::String^ eventName, VariantMap^ varMap);
    // INTERNAL USE ONLY
    void CheckEvent(Urho3D::StringHash eventType, Urho3D::VariantMap& eventData);

// Systems
    Graphics^ GetGraphics();

private:
    void SubscribeToEvent(System::String^ name, System::Object^ callback);
    void UnsubscribeFromEvent(System::String^ name);

    static UrControl^ inst_;
    bool valid_;
    System::String^ scriptFileName_;
    System::String^ lastError_;
    Urho3D::Engine* engine_;
    Urho3D::Context* context_;
    Urho3D::ScriptFile* scriptFile_;
    EventProxy* proxy_;
    void OnElapsed(System::Object^ sender, System::Timers::ElapsedEventArgs^ e);
    System::Collections::Generic::List<System::Object^>^ callbacks_;
};

}