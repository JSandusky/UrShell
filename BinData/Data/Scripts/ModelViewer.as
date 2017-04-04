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
    
    previewLightNode = editorScene.CreateChild("PreviewLight");
    previewLightNode.direction = Vector3(0.5, -0.5, 0.5);
    previewLight = previewLightNode.CreateComponent("Light");
    previewLight.lightType = LIGHT_DIRECTIONAL;
    previewLight.specularIntensity = 0.5;
    
    material = Material();
    
    if (loadFile.length > 0)
    {
        previewModelNode = editorScene.CreateChild("PreviewModel");
        previewModelNode.rotation = Quaternion(0, 0, 0);
        previewModel = previewModelNode.CreateComponent("StaticModel");
        previewModel.model = cache.GetResource("Model", loadFile);
    }
    
    previewModel.material = material;
}

void StartForFile(String fileName)
{
    loadFile = fileName;
    Start();
}