#pragma once

#include "MathBind.h"
#include "Variant.h"
#include <Urho3D/Urho3D.h>
#include <Urho3D/Resource/XMLElement.h>

namespace Urho3D
{
    class XMLElement;
    class XMLFile;
}

namespace UrhoBackend
{
    ref class UrControl;

    public ref class XMLFile
    {
    public:
        XMLFile(System::Xml::XmlDocument^ document, UrControl^ urho);
        XMLFile(Urho3D::XMLFile* comp);
        XMLFile(System::IntPtr ptr);
        ~XMLFile();

        System::IntPtr ToIntPtr() { return System::IntPtr((void*)data_); }
        Urho3D::XMLFile* GetPtr() { return data_; }

        System::Xml::XmlDocument^ ToDocument();

        bool FromString(System::String^);
        System::String^ ToString(System::String^);
        void Patch(XMLFile^);

    private:
        void FillFromElement(System::Xml::XmlElement^ elem, Urho3D::XMLElement& urhoElem);
        void FillIntoElement(System::Xml::XmlDocument^ document, System::Xml::XmlElement^ elem, Urho3D::XMLElement& urhoElem);

        Urho3D::XMLFile* data_;
    };
}