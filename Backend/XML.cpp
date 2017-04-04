#include "stdafx.h"

#include "XML.h"
#include "UrControl.h"

#include <Urho3D/Urho3D.h>

#include <Urho3D/Resource/XMLFile.h>
#include <Urho3D/Resource/XMLElement.h>
#include <Urho3D/Container/Str.h>

#include "ResourceRefWrapper.h"

namespace UrhoBackend
{

XMLFile::XMLFile(System::Xml::XmlDocument^ document, UrControl^ urho)
{
    data_ = new Urho3D::XMLFile(urho->GetContext()); // Need to get the context?
    for (int i = 0; i < document->ChildNodes->Count; ++i)
    {
        System::Xml::XmlNode^ nd = document->ChildNodes->Item(i);
        if (nd->GetType() == System::Xml::XmlElement::typeid)
        {
            System::Xml::XmlElement^ childElem = (System::Xml::XmlElement^)nd;
            Urho3D::XMLElement elem = data_->CreateRoot(ToCString(nd->Name));
            FillFromElement(childElem, elem);
        }
    }
}

void XMLFile::FillFromElement(System::Xml::XmlElement^ elem, Urho3D::XMLElement& urhoElem)
{
    for (int i = 0; i < elem->Attributes->Count; ++i)
    {
        System::Xml::XmlAttribute^ attr = (System::Xml::XmlAttribute^)elem->Attributes->Item(i);
        urhoElem.SetAttribute(ToCString(attr->Name), ToCString(attr->Value));
    }

    for (int i = 0; i < elem->ChildNodes->Count; ++i)
    {
        System::Xml::XmlNode^ nd = elem->ChildNodes->Item(i);
        if (nd->GetType() == System::Xml::XmlElement::typeid)
        {
            System::Xml::XmlElement^ childElem = (System::Xml::XmlElement^)nd;
            Urho3D::XMLElement uelem = urhoElem.CreateChild(ToCString(nd->Name));

            if (elem->HasChildNodes)
                FillFromElement(childElem, uelem);
        }
    }
}

XMLFile::XMLFile(System::IntPtr data)
{
    data_ = static_cast<Urho3D::XMLFile*>(data.ToPointer());
}

XMLFile::XMLFile(Urho3D::XMLFile* fromUrho) { data_ = fromUrho; }

XMLFile::~XMLFile()
{
    
}

System::Xml::XmlDocument^ XMLFile::ToDocument()
{
    System::Xml::XmlDocument^ ret = gcnew System::Xml::XmlDocument();

    Urho3D::XMLElement elem = data_->GetRoot();
    System::Xml::XmlElement^ rootElem = ret->CreateElement(gcnew System::String(elem.GetName().CString()));
    FillIntoElement(ret, rootElem, elem);
    ret->AppendChild(rootElem);

    return ret;
}

void XMLFile::FillIntoElement(System::Xml::XmlDocument^ document, System::Xml::XmlElement^ elem, Urho3D::XMLElement& urhoElem)
{
    Urho3D::Vector<Urho3D::String> attrNames = urhoElem.GetAttributeNames();
    for (unsigned i = 0; i < attrNames.Size(); ++i)
    {
        Urho3D::String value = urhoElem.GetAttribute(attrNames[i]);
        elem->SetAttribute(gcnew System::String(attrNames[i].CString()), gcnew System::String(value.CString()));
    }

    Urho3D::XMLElement childElem = urhoElem.GetChild();
    while (!childElem.IsNull())
    {
        System::Xml::XmlElement^ childXML = document->CreateElement(gcnew System::String(childElem.GetName().CString()));
        elem->AppendChild(childXML);
        FillIntoElement(document, childXML, childElem);
        childElem = childElem.GetNext();
    }
}

bool XMLFile::FromString(System::String^ A)  { return  data_->FromString(Urho3D::String(ToCString(A))); }

System::String^ XMLFile::ToString(System::String^ A)  { return  gcnew System::String(data_->ToString(Urho3D::String(ToCString(A))).CString()); }

void XMLFile::Patch(XMLFile^ A)  { data_->Patch(A->data_); }

}