using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// List of all Urho3D enumerations
namespace UrhoBackend
{
    public enum PickingMode
    {
        PICK_DRAWABLES,
        PICK_PHYSICS,
        PICK_LIGHTS,
        PICK_ZONES,
        PICK_UI
    };

    public enum Intersection
    {
        OUTSIDE,
        INTERSECTS,
        INSIDE
    };

    public enum VariantType
    {
        VAR_NONE,
        VAR_INT,
        VAR_BOOL,
        VAR_FLOAT,
        VAR_VECTOR2,
        VAR_VECTOR3,
        VAR_VECTOR4,
        VAR_QUATERNION,
        VAR_COLOR,
        VAR_STRING,
        VAR_BUFFER,
        VAR_VOIDPTR,
        VAR_RESOURCEREF,
        VAR_RESOURCEREFLIST,
        VAR_VARIANTVECTOR,
        VAR_VARIANTMAP,
        VAR_INTRECT,
        VAR_INTVECTOR2,
        VAR_PTR,
        VAR_MATRIX3,
        VAR_MATRIX3X4,
        VAR_MATRIX4
    };

    public enum InterpolationMode
    {
        BEZIER_CURVE
    };

    public enum FileMode
    {
        FILE_READ,
        FILE_WRITE,
        FILE_READWRITE
    };

    public enum CompressedFormat
    {
        CF_NONE,
        CF_RGBA,
        CF_DXT1,
        CF_DXT3,
        CF_DXT5,
        CF_ETC1,
        CF_PVRTC_RGB_2BPP,
        CF_PVRTC_RGBA_2BPP,
        CF_PVRTC_RGB_4BPP,
        CF_PVRTC_RGBA_4BPP
    };

    public enum JSONValueType
    {
        JSON_ANY,
        JSON_OBJECT,
        JSON_ARRAY
    };

    public enum InterpMethod
    {
        IM_LINEAR,
        IM_SPLINE
    };

    public enum WrapMode
    {
        WM_LOOP,
        WM_ONCE,
        WM_CLAMP
    };

    public enum CreateMode
    {
        REPLICATED,
        LOCAL
    };

    public enum TransformSpace
    {
        TS_LOCAL,
        TS_PARENT,
        TS_WORLD
    };

    public enum LoadMode
    {
        LOAD_RESOURCES_ONLY,
        LOAD_SCENE,
        LOAD_SCENE_AND_RESOURCES
    };

    public enum FillMode
    {
        FILL_SOLID,
        FILL_WIREFRAME,
        FILL_POINT
    };

    public enum CubeMapFace
    {
        FACE_POSITIVE_X,
        FACE_NEGATIVE_X,
        FACE_POSITIVE_Y,
        FACE_NEGATIVE_Y,
        FACE_POSITIVE_Z,
        FACE_NEGATIVE_Z
    };

    public enum RenderCommandType
    {
        CMD_NONE,
        CMD_CLEAR,
        CMD_SCENEPASS,
        CMD_QUAD,
        CMD_FORWARDLIGHTS,
        CMD_LIGHTVOLUMES,
        CMD_RENDERUI
    };

    public enum RenderCommandSortMode
    {
        SORT_FRONTTOBACK,
        SORT_BACKTOFRONT
    };

    public enum RenderTargetSizeMode
    {
        SIZE_ABSOLUTE,
        SIZE_VIEWPORTDIVISOR,
        SIZE_VIEWPORTMULTIPLIER
    };

    public enum TextureUnit
    {
        TU_DIFFUSE,
        TU_ALBEDOBUFFER,
        TU_NORMAL,
        TU_NORMALBUFFER,
        TU_SPECULAR,
        TU_EMISSIVE,
        TU_ENVIRONMENT,
        TU_LIGHTRAMP,
        TU_LIGHTSHAPE,
        TU_SHADOWMAP,
        TU_CUSTOM1,
        TU_CUSTOM2,
        TU_VOLUMEMAP,
        TU_FACESELECT,
        TU_INDIRECTION,
        TU_DEPTHBUFFER,
        TU_LIGHTBUFFER,
        TU_ZONE,
        MAX_MATERIAL_TEXTURE_UNITS,
        MAX_TEXTURE_UNITS
    };

    public enum TextureUsage
    {
        TEXTURE_STATIC,
        TEXTURE_DYNAMIC,
        TEXTURE_RENDERTARGET,
        TEXTURE_DEPTHSTENCIL
    };

    public enum TextureFilterMode
    {
        FILTER_NEAREST,
        FILTER_BILINEAR,
        FILTER_TRILINEAR,
        FILTER_ANISOTROPIC,
        FILTER_DEFAULT
    };

    public enum TextureAddressMode
    {
        ADDRESS_WRAP,
        ADDRESS_MIRROR,
        ADDRESS_CLAMP,
        ADDRESS_BORDER
    };

    public enum TextureCoordinate
    {
        COORD_U,
        COORD_V,
        COORD_W
    };

    public enum RenderSurfaceUpdateMode
    {
        SURFACE_MANUALUPDATE,
        SURFACE_UPDATEVISIBLE,
        SURFACE_UPDATEALWAYS
    };

    public enum BlendMode
    {
        BLEND_REPLACE,
        BLEND_ADD,
        BLEND_MULTIPLY,
        BLEND_ALPHA,
        BLEND_ADDALPHA,
        BLEND_PREMULALPHA,
        BLEND_INVDESTALPHA,
        BLEND_SUBTRACT,
        BLEND_SUBTRACTALPHA
    };

    public enum CompareMode
    {
        CMP_ALWAYS,
        CMP_EQUAL,
        CMP_NOTEQUAL,
        CMP_LESS,
        CMP_LESSEQUAL,
        CMP_GREATER,
        CMP_GREATEREQUAL
    };

    public enum CullMode
    {
        CULL_NONE,
        CULL_CCW,
        CULL_CW
    };

    public enum PassLightingMode
    {
        LIGHTING_UNLIT,
        LIGHTING_PERVERTEX,
        LIGHTING_PERPIXEL
    };

    public enum PrimitiveType
    {
        TRIANGLE_LIST,
        LINE_LIST,
        POINT_LIST,
        TRIANGLE_STRIP,
        LINE_STRIP,
        TRIANGLE_FAN
    };

    public enum LightType
    {
        LIGHT_DIRECTIONAL,
        LIGHT_SPOT,
        LIGHT_POINT
    };

    public enum FaceCameraMode
    {
        FC_NONE,
        FC_ROTATE_XYZ,
        FC_ROTATE_Y,
        FC_LOOKAT_XYZ,
        FC_LOOKAT_Y
    };

    public enum EmitterType
    {
        EMITTER_SPHERE,
        EMITTER_BOX
    };

    public enum RayQueryLevel
    {
        RAY_AABB,
        RAY_OBB,
        RAY_TRIANGLE
    };

    public enum MouseMode
    {
        MM_ABSOLUTE,
        MM_RELATIVE,
        MM_WRAP
    };

    public enum HorizontalAlignment
    {
        HA_LEFT,
        HA_CENTER,
        HA_RIGHT
    };

    public enum VerticalAlignment
    {
        VA_TOP,
        VA_CENTER,
        VA_BOTTOM
    };

    public enum Corner
    {
        C_TOPLEFT,
        C_TOPRIGHT,
        C_BOTTOMLEFT,
        C_BOTTOMRIGHT
    };

    public enum Orientation
    {
        O_HORIZONTAL,
        O_VERTICAL
    };

    public enum FocusMode
    {
        FM_NOTFOCUSABLE,
        FM_RESETFOCUS,
        FM_FOCUSABLE,
        FM_FOCUSABLE_DEFOCUSABLE
    };

    public enum LayoutMode
    {
        LM_FREE,
        LM_HORIZONTAL,
        LM_VERTICAL
    };

    public enum TraversalMode
    {
        TM_BREADTH_FIRST,
        TM_DEPTH_FIRST
    };

    public enum CursorShape
    {
        CS_NORMAL,
        CS_IBEAM,
        CS_CROSS,
        CS_RESIZEVERTICAL,
        CS_RESIZEDIAGONAL_TOPRIGHT,
        CS_RESIZEHORIZONTAL,
        CS_RESIZEDIAGONAL_TOPLEFT,
        CS_RESIZE_ALL,
        CS_ACCEPTDROP,
        CS_REJECTDROP,
        CS_BUSY,
        CS_BUSY_ARROW
    };

    public enum HighlightMode
    {
        HM_NEVER,
        HM_FOCUS,
        HM_ALWAYS
    };

    public enum TextEffect
    {
        TE_NONE,
        TE_SHADOW,
        TE_STROKE
    };

    public enum HttpRequestState
    {
        HTTP_INITIALIZING,
        HTTP_ERROR,
        HTTP_OPEN,
        HTTP_CLOSED
    };

    public enum ShapeType
    {
        SHAPE_BOX,
        SHAPE_SPHERE,
        SHAPE_STATICPLANE,
        SHAPE_CYLINDER,
        SHAPE_CAPSULE,
        SHAPE_CONE,
        SHAPE_TRIANGLEMESH,
        SHAPE_CONVEXHULL,
        SHAPE_TERRAIN
    };

    public enum CollisionEventMode
    {
        COLLISION_NEVER,
        COLLISION_ACTIVE,
        COLLISION_ALWAYS
    };

    public enum ConstraintType
    {
        CONSTRAINT_POINT,
        CONSTRAINT_HINGE,
        CONSTRAINT_SLIDER,
        CONSTRAINT_CONETWIST
    };

    public enum CrowdTargetState
    {
        CROWD_AGENT_TARGET_NONE,
        CROWD_AGENT_TARGET_FAILED,
        CROWD_AGENT_TARGET_VALID,
        CROWD_AGENT_TARGET_REQUESTING,
        CROWD_AGENT_TARGET_WAITINGFORPATH,
        CROWD_AGENT_TARGET_WAITINGFORQUEUE,
        CROWD_AGENT_TARGET_VELOCITY,
        CROWD_AGENT_TARGET_ARRIVED
    };

    public enum CrowdAgentState
    {
        CROWD_AGENT_INVALID,
        CROWD_AGENT_READY,
        CROWD_AGENT_TRAVERSINGLINK
    };

    public enum NavigationAvoidanceQuality
    {
        NAVIGATIONQUALITY_LOW,
        NAVIGATIONQUALITY_MEDIUM,
        NAVIGATIONQUALITY_HIGH
    };

    public enum NavigationPushiness
    {
        PUSHINESS_LOW,
        PUSHINESS_MEDIUM,
        PUSHINESS_HIGH
    };

    public enum LoopMode2D
    {
        LM_DEFAULT,
        LM_FORCE_LOOPED,
        LM_FORCE_CLAMPED
    };

    public enum EmitterType2D
    {
        EMITTER_TYPE_GRAVITY,
        EMITTER_TYPE_RADIAL
    };

    public enum Orientation2D
    {
        O_ORTHOGONAL,
        O_ISOMETRIC,
        O_STAGGERED
    };

    public enum TileMapLayerType2D
    {
        LT_TILE_LAYER,
        LT_OBJECT_GROUP,
        LT_IMAGE_LAYER,
        LT_INVALID
    };

    public enum TileObjectType2D
    {
        OT_RECTANGLE,
        OT_ELLIPSE,
        OT_POLYGON,
        OT_POLYLINE,
        OT_TILE,
        OT_INVALID
    };

    public enum BodyType2D
    {
        BT_STATIC,
        BT_DYNAMIC,
        BT_KINEMATIC
    };

    public enum DumpMode
    {
        DOXYGEN,
        C_HEADER
    };
}
