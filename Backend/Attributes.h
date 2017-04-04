#pragma once



namespace UrhoBackend
{

    [System::AttributeUsageAttribute(System::AttributeTargets::Method)]
    public ref class HandleEvent : public System::Attribute
    {
    public:
        property System::String^ EventName;
    };

    [System::AttributeUsageAttribute(System::AttributeTargets::Property)]
    public ref class UAttribute : public System::Attribute
    {
    public:
        UAttribute(System::String^ attrName)
        {
            AttributeName = attrName;
        }
        property System::String^ AttributeName;
    };
}