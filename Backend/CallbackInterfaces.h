#pragma once

namespace UrhoBackend
{

/// C# implements this and provides when wanting a thumbnail rendered.
public interface class ImageDataCallback
{
public:
    void OnImageDataCreated(System::IntPtr data, System::Int32 dataSize);
};

}