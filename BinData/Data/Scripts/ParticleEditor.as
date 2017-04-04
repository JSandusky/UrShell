
Scene@ editorScene;
Node@ cameraNode;
Node@ effectNode;
ParticleEmitter@ effectEmitter;
ParticleEffect@ editParticleEffect;

Node@ gridNode;
CustomGeometry@ grid;

float cameraPitch = 0.0;
float cameraYaw = 0.0f;

uint gridSize = 16;
uint gridSubdivisions = 3;
float gridScale = 8.0;
Color gridColor(0.1, 0.1, 0.1);
Color gridSubdivisionColor(0.05, 0.05, 0.05);
Color gridXColor(0.5, 0.1, 0.1);
Color gridYColor(0.1, 0.5, 0.1);
Color gridZColor(0.1, 0.1, 0.5);

float cameraBaseRotationSpeed = 0.5f;

void Start()
{
    // Use the first frame to setup when the resolution is initialized
    SubscribeToEvent("Update", "FirstFrame");
    SubscribeToEvent("MouseMove", "HandleMouseMoved");
    SubscribeToEvent("MouseWheel", "HandleMouseWheel");
    SubscribeToEvent(input, "ExitRequested", "HandleExitRequested");

    // Disable Editor auto exit, check first if it is OK to exit
    engine.autoExit = true;
    // Enable console commands from the editor script
    //script.defaultScriptFile = scriptFile;
    // Enable automatic resource reloading
    cache.autoReloadResources = true;
    // Return resources which exist but failed to load due to error, so that we will not lose resource refs
    cache.returnFailedResources = true;
    // Use OS mouse without grabbing it
    input.mouseVisible = true;
    // Use system clipboard to allow transport of text in & out from the editor
    ui.useSystemClipboard = true;
}

String fileToLoad;
void StartForFile(String fileName)
{
    fileToLoad = fileName;
    Start();
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
    
    if (fileToLoad.length > 0)
        LoadParticleEffect(fileToLoad);
        
    VariantMap effectData = VariantMap();
    effectData["Effect"] = Variant(editParticleEffect);
    SendEvent("ParticleData", effectData);
}

void UpdateEffect()
{
    effectEmitter.Reset();
    effectEmitter.ResetEmissionTimer();
    effectEmitter.ApplyEffect();
}

void CreateScene()
{
    editorScene = Scene("ParticlePreview");
    editorScene.CreateComponent("Octree");
    
    Node@ zoneNode = editorScene.CreateChild("Zone");
    Zone@ zone = zoneNode.CreateComponent("Zone");
    zone.boundingBox = BoundingBox(-1000, 1000);
    zone.ambientColor = Color(0.15, 0.15, 0.15);
    zone.fogColor = Color(0, 0, 0);
    zone.fogStart = 10.0;
    zone.fogEnd = 100.0;
    
    cameraNode = editorScene.CreateChild("PreviewCamera");
    cameraNode.position = Vector3(0, 0, -2.5);
    Camera@ camera = cameraNode.CreateComponent("Camera");
    camera.nearClip = 0.1f;
    camera.farClip = 100.0f;
    
    effectNode = editorScene.CreateChild("PreviewEmitterNode");
    effectNode.rotation = Quaternion(0,0,0);
    
    effectEmitter = effectNode.CreateComponent("ParticleEmitter");
    editParticleEffect = CreateNewParticleEffect();
    effectEmitter.effect = editParticleEffect;
}

ParticleEffect@ CreateNewParticleEffect()
{
    ParticleEffect@ effect = ParticleEffect();
    Material@ res = cache.GetResource("Material", "Materials/Particle.xml");
    if (res is null)
        log.Error("Could not load default material for new particle effect.");
    effect.material = res;
    //effect.AddColorTime(Color(1,1,1,1), 0.0f);
    return effect;
}

void LoadParticleEffect(String fileName)
{
    ParticleEffect@ newEffect =  cache.GetResource("ParticleEffect", fileName);
    if (newEffect !is null)
    {
        editParticleEffect = newEffect;
        effectEmitter.effect = newEffect;
    }
}

void HandleKeyDown(StringHash eventType, VariantMap& eventData)
{
    int key = eventData["Key"].GetInt();

    // Close console (if open) or exit when ESC is pressed
    if (key == KEY_ESC)
    {
        if (!console.visible)
            engine.Exit();
        else
            console.visible = false;
    }

    // Toggle console with F1
    else if (key == KEY_F1)
        console.Toggle();

    // Toggle debug HUD with F2
    else if (key == KEY_F2)
        debugHud.ToggleAll();

    // Common rendering quality controls, only when UI has no focused element
    if (ui.focusElement is null)
    {
        // Texture quality
        if (key == '1')
        {
            int quality = renderer.textureQuality;
            ++quality;
            if (quality > QUALITY_HIGH)
                quality = QUALITY_LOW;
            renderer.textureQuality = quality;
        }

        // Material quality
        else if (key == '2')
        {
            int quality = renderer.materialQuality;
            ++quality;
            if (quality > QUALITY_HIGH)
                quality = QUALITY_LOW;
            renderer.materialQuality = quality;
        }

        // Specular lighting
        else if (key == '3')
            renderer.specularLighting = !renderer.specularLighting;

        // Shadow rendering
        else if (key == '4')
            renderer.drawShadows = !renderer.drawShadows;

        // Shadow map resolution
        else if (key == '5')
        {
            int shadowMapSize = renderer.shadowMapSize;
            shadowMapSize *= 2;
            if (shadowMapSize > 2048)
                shadowMapSize = 512;
            renderer.shadowMapSize = shadowMapSize;
        }

        // Shadow depth and filtering quality
        else if (key == '6')
        {
            int quality = renderer.shadowQuality;
            ++quality;
            if (quality > SHADOWQUALITY_HIGH_24BIT)
                quality = SHADOWQUALITY_LOW_16BIT;
            renderer.shadowQuality = quality;
        }

        // Occlusion culling
        else if (key == '7')
        {
            bool occlusion = renderer.maxOccluderTriangles > 0;
            occlusion = !occlusion;
            renderer.maxOccluderTriangles = occlusion ? 5000 : 0;
        }

        // Instancing
        else if (key == '8')
            renderer.dynamicInstancing = !renderer.dynamicInstancing;

        // Take screenshot
        else if (key == '9')
        {
            Image@ screenshot = Image();
            graphics.TakeScreenShot(screenshot);
            // Here we save in the Data folder with date and time appended
            screenshot.SavePNG(fileSystem.programDir + "Data/Screenshot_" +
                time.timeStamp.Replaced(':', '_').Replaced('.', '_').Replaced(' ', '_') + ".png");
        }
    }
}

void HandleMouseMoved(StringHash eventType, VariantMap& eventData)
{
    int buttons = eventData["Buttons"].GetInt();
    if (buttons != MOUSEB_MIDDLE)
        return;
    IntVector2 mouseMove = IntVector2();
    mouseMove.x = eventData["DX"].GetInt();
    mouseMove.y = eventData["DY"].GetInt();
        
    cameraYaw += mouseMove.x * cameraBaseRotationSpeed;
    cameraPitch += mouseMove.y * cameraBaseRotationSpeed;
    
    Quaternion q = Quaternion(cameraPitch, cameraYaw, 0);
    cameraNode.rotation = q;
    
    Vector3 centerPoint = Vector3(0,0,0);
    Vector3 d = cameraNode.worldPosition - centerPoint;
    cameraNode.worldPosition = centerPoint - q * Vector3(0.0, 0.0, d.length);
}

void HandleMouseWheel(StringHash eventType, VariantMap& eventData)
{
    int wheelDelta = eventData["Wheel"].GetInt();
    Vector3 centerPoint = Vector3(0,0,0);
    Vector3 d = cameraNode.worldPosition - centerPoint;
    Quaternion q = cameraNode.rotation;
 
    float mul = input.keyDown[::KEY_SHIFT] ? 3.0 : 1.0;
    if (wheelDelta > 0)
    {
        cameraNode.worldPosition = centerPoint - q * Vector3(0.0, 0.0, d.length - 0.5 * mul);
    }
    else
    {
        cameraNode.worldPosition = centerPoint - q * Vector3(0.0, 0.0, d.length + 0.5 * mul);
    }
}

void HandleSceneUpdate(StringHash eventType, VariantMap& eventData)
{
    if ((input.mouseMoveX != 0 || input.mouseMoveY != 0) && input.mouseButtonDown[2])
    {
        IntVector2 mouseMove = input.mouseMove;
        
        cameraYaw += mouseMove.x * cameraBaseRotationSpeed;
        cameraPitch += mouseMove.y * cameraBaseRotationSpeed;
        
        Quaternion q = Quaternion(cameraPitch, cameraYaw, 0);
        cameraNode.rotation = q;
        
        Vector3 centerPoint = Vector3(0,0,0);
        Vector3 d = cameraNode.worldPosition - centerPoint;
        cameraNode.worldPosition = centerPoint - q * Vector3(0.0, 0.0, d.length);
    }
    
    if (input.mouseMoveWheel != 0)
    {
        Vector3 centerPoint = Vector3(0,0,0);
        Vector3 d = cameraNode.worldPosition - centerPoint;
        Quaternion q = cameraNode.rotation;
     
        float mul = input.keyDown[::KEY_SHIFT] ? 3.0 : 1.0;
        if (input.mouseMoveWheel > 0)
        {
            cameraNode.worldPosition = centerPoint - q * Vector3(0.0, 0.0, d.length - 0.5 * mul);
        }
        else
        {
            cameraNode.worldPosition = centerPoint - q * Vector3(0.0, 0.0, d.length + 0.5 * mul);
        }
    }
}

void CreateGrid()
{
    if (gridNode !is null)
        gridNode.Remove();

    gridNode = editorScene.CreateChild("Grid");
    grid = gridNode.CreateComponent("CustomGeometry");
    grid.numGeometries = 1;
    grid.material = cache.GetResource("Material", "Materials/VColUnlit.xml");
    grid.viewMask = 0x80000000; // Editor raycasts use viewmask 0x7fffffff
    grid.occludee = false;

    UpdateGrid(true);
}

void UpdateGrid(bool updateGridGeometry = true)
{
    bool showGrid = true;    
    gridNode.scale = Vector3(gridScale, gridScale, gridScale);

    if (!updateGridGeometry)
        return;

    uint size = uint(Floor(gridSize / 2) * 2);
    float halfSizeScaled = size / 2;
    float scale = 1.0;
    uint subdivisionSize = uint(Pow(2.0f, float(gridSubdivisions)));

    if (subdivisionSize > 0)
    {
        size *= subdivisionSize;
        scale /= subdivisionSize;
    }

    uint halfSize = size / 2;

    grid.BeginGeometry(0, LINE_LIST);
    float lineOffset = -halfSizeScaled;
    for (uint i = 0; i <= size; ++i)
    {
        bool lineCenter = i == halfSize;
        bool lineSubdiv = !Equals(Mod(i, subdivisionSize), 0.0);

        grid.DefineVertex(Vector3(lineOffset, 0.0, halfSizeScaled));
        grid.DefineColor(lineCenter ? gridZColor : (lineSubdiv ? gridSubdivisionColor : gridColor));
        grid.DefineVertex(Vector3(lineOffset, 0.0, -halfSizeScaled));
        grid.DefineColor(lineCenter ? gridZColor : (lineSubdiv ? gridSubdivisionColor : gridColor));

        grid.DefineVertex(Vector3(-halfSizeScaled, 0.0, lineOffset));
        grid.DefineColor(lineCenter ? gridXColor : (lineSubdiv ? gridSubdivisionColor : gridColor));
        grid.DefineVertex(Vector3(halfSizeScaled, 0.0, lineOffset));
        grid.DefineColor(lineCenter ? gridXColor : (lineSubdiv ? gridSubdivisionColor : gridColor));

        lineOffset  += scale;
    }
    grid.Commit();
}

void SetupViewport()
{
    // Set up a viewport to the Renderer subsystem so that the 3D scene can be seen. We need to define the scene and the camera
    // at minimum. Additionally we could configure the viewport screen size and the rendering path (eg. forward / deferred) to
    // use, but now we just use full screen and default render path configured in the engine command line options
    Viewport@ viewport = Viewport(editorScene, cameraNode.GetComponent("Camera"));
    renderer.viewports[0] = viewport;
}

void SetData(VariantMap effectData)
{
    ParticleEffect@ newEffect = ParticleEffect();
}

/*
void SetEffectInt(String name, int value)
{
    if (name == "NumParticles")
        editParticleEffect.numParticles = value;
        
    effectEmitter.Reset();
    effectEmitter.ResetEmissionTimer();
    effectEmitter.ApplyEffect();
}

void SetEffectFloat(String name, float value)
{
    if (name == "ActiveTime")
        editParticleEffect.activeTime = value;
    else if (name == "AnimLodBias")
        editParticleEffect.animationLodBias = value;
    else if (name == "DampingForce")
        editParticleEffect.dampingForce = value;
    else if (name == "InactiveTime")
        editParticleEffect.inactiveTime = value;
    else if (name == "Type")
    {
        if (value == 0)
            editParticleEffect.emitterType = EmitterType::EMITTER_SPHERE;
        else
            editParticleEffect.emitterType = EmitterType::EMITTER_BOX;
    }
    effectEmitter.Reset();
    effectEmitter.ResetEmissionTimer();
    effectEmitter.ApplyEffect();
}

void SetEffectString(String name, String value)
{
    if (name == "Material")
    {
        editParticleEffect.material = cache.GetResource("Material", value);
    }
    effectEmitter.Reset();
    effectEmitter.ResetEmissionTimer();
    effectEmitter.ApplyEffect();
}

void SetEffectBool(String name, bool value)
{
    if (name == "UpdateInvisible")
        editParticleEffect.updateInvisible = value;
    else if (name == "Relative")
        editParticleEffect.relative = value;
    else if (name == "Scaled")
        editParticleEffect.scaled = value;
    else if (name == "Sorted")
        editParticleEffect.sorted = value;
    effectEmitter.Reset();
    effectEmitter.ResetEmissionTimer();
    effectEmitter.ApplyEffect();
}

void SetEffectVector2(String name, Vector2 value)
{
    if (name == "EmissionRate")
    {
        editParticleEffect.minEmissionRate = value.x;
        editParticleEffect.maxEmissionRate = value.y;
    }
    else if (name == "MinSize")
        editParticleEffect.minParticleSize = value;
    else if (name == "MaxSize")                   
        editParticleEffect.maxParticleSize = value;
    else if (name == "TimeToLive")
    {
        editParticleEffect.minTimeToLive = value.x;
        editParticleEffect.maxTimeToLive = value.y;
    }
    else if (name == "Rotation")
    {
        editParticleEffect.minRotation = value.x;
        editParticleEffect.maxRotation = value.y;
    }
    else if (name == "RotationSpeed")
    {
        editParticleEffect.minRotation = value.x;
        editParticleEffect.maxRotation = value.y;
    }
    else if (name == "SizeDelta")
    {
        editParticleEffect.sizeAdd = value.x;
        editParticleEffect.sizeMul = value.y;
    }
    effectEmitter.Reset();
    effectEmitter.ResetEmissionTimer();
    effectEmitter.ApplyEffect();
}

void SetEffectVector3(String name, Vector3 value)
{
    if (name == "EmitterSize")
        editParticleEffect.emitterSize = value;
    else if (name == "DirectionMin")
        editParticleEffect.minDirection = value;
    else if (name == "DirectionMax")
        editParticleEffect.minDirection = value;
    else if (name == "ConstantForce")
        editParticleEffect.constantForce = value;
        
    effectEmitter.Reset();
    effectEmitter.ResetEmissionTimer();
    effectEmitter.ApplyEffect();
}

void AddColorFrame()
{
    editParticleEffect.numColorFrames = editParticleEffect.numColorFrames + 1;
    editParticleEffect.SortColorFrames();
    effectEmitter.Reset();
}

void SetEffectColorFrame(int index, Color colorValue, float time)
{    
    log.Info("Entered color frame");
    ColorFrame@ frame = editParticleEffect.GetColorFrame(index);
    if (frame is null)
        return;
    log.Info("not null");
    frame.time = time;
    log.Info("set time");
    frame.color = colorValue;
    log.Info("set values");
    editParticleEffect.SetColorFrame(index, frame);
    editParticleEffect.SortColorFrames();
    effectEmitter.ApplyEffect();
    log.Info("left it");
    effectEmitter.Reset();
    effectEmitter.ResetEmissionTimer();
}

void RemoveColorFrame(int index)
{
    editParticleEffect.RemoveColorFrame(index);
    editParticleEffect.SortColorFrames();
    
    effectEmitter.ApplyEffect();
    effectEmitter.Reset();
    effectEmitter.ResetEmissionTimer();
}

void AddTextureFrame()
{
    editParticleEffect.numTextureFrames = editParticleEffect.numTextureFrames + 1;
    effectEmitter.Reset();
    effectEmitter.ResetEmissionTimer();
}

void SetTextureFrame(int index, Vector4 uv, float time)
{
    TextureFrame@ frame = editParticleEffect.GetTextureFrame(index);
    
    if (frame is null)
        return;
    frame.uv.left = uv.x;
    frame.uv.top = uv.y;
    frame.uv.right = uv.z;
    frame.uv.bottom = uv.w;
        
    frame.time = time;
    
    editParticleEffect.SortTextureFrames();
    
    effectEmitter.ApplyEffect();
    effectEmitter.Reset();
    effectEmitter.ResetEmissionTimer();
}

void RemoveTextureFrame(int index)
{
    editParticleEffect.RemoveTextureFrame(index);
    editParticleEffect.SortTextureFrames();
    
    effectEmitter.ApplyEffect();
    effectEmitter.Reset();
    effectEmitter.ResetEmissionTimer();
}*/