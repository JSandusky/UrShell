#include "Data/Scripts/OrbitZoomEditor.as"

Node@ previewModelNode;
StaticModel@ previewModel;
Material@ material;

// Light
Node@ previewLightNode;
Light@ previewLight;

String loadFile;

void Start()
{
    StartOrbitZoomEditor();
}

void FirstFrame()
{
    // Create root scene node
    CreateScene();
    
    CreateGrid();
    
    SetupViewport();
    
    UnsubscribeFromEvent("Update");
    SubscribeToEvent("Update", "HandleSceneUpdate");
    SubscribeToEvent("KeyDown", "HandleKeyDown");
    
    previewModelNode = editorScene.CreateChild("PreviewModel");
    previewModelNode.rotation = Quaternion(0, 0, 0);
    previewModel = previewModelNode.CreateComponent("StaticModel");
    previewModel.model = cache.GetResource("Model", "Models/Sphere.mdl");
    
    previewLightNode = editorScene.CreateChild("PreviewLight");
    previewLightNode.direction = Vector3(0.5, -0.5, 0.5);
    previewLight = previewLightNode.CreateComponent("Light");
    previewLight.lightType = LIGHT_DIRECTIONAL;
    previewLight.specularIntensity = 0.5;
    
    material = Material();
    
    if (loadFile.length > 0)
    {
        material.Load(cache.GetFile(loadFile));
    }
    
    previewModel.material = material;
    
    VariantMap eventData = VariantMap();
    eventData["Material"] = Variant(material);
    SendEvent("MaterialLoaded", eventData);
}

void SetMaterialShape(int shapeIndex)
{
    if (shapeIndex == 0) // sphere
    {
    }
    if (shapeIndex == 1) // box
    {
    }
    else if (shapeIndex == 2) // plane
    {
    }
    else
        log.Error("Unknown shape index specified");
}

void StartForFile(String fileName)
{
    loadFile = fileName;
    Start();
}