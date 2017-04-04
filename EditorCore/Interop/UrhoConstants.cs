using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UrhoBackend;

namespace EditorCore.Interop
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ResourceExtensions : Attribute
    {
        public ResourceExtensions(string filter, string[] extensions) { Filter = filter;  Extensions = extensions; }

        public string[] Extensions { get; set; }
        public string Filter { get; set; }
    }

    public static class UrhoConstants
    {
        [ResourceExtensions("Package Files (*.pak)|*.pak", new string[] { ".pak" })]
        public static readonly StringHash RES_LIGHTMAP = new StringHash("Package");

        [ResourceExtensions("XML Files (*.xml)|*.xml", new string[] { ".xml" })]
        public static readonly StringHash RES_XML = new StringHash("XMLFile");

        [ResourceExtensions("Models (*.mdl)|*.mdl", new string[] { ".mdl" })]
        public static readonly StringHash RES_MODEL = new StringHash("Model");

        [ResourceExtensions("Shaders (*.hlsl, *.glsl|*.hlsl;*.glsl", new string[]{ "*.hlsl", "*.glsl" })]
        public static readonly StringHash RES_SHADER = new StringHash("Shader");

        [ResourceExtensions("Techniques (*.xml)|*.xml", new string[] { "*.xml" })]
        public static readonly StringHash RES_TECHNIQUE = new StringHash("Technique");

        [ResourceExtensions("Model Animations (*.ani)|*.ani", new string[] { ".ani" })]
        public static readonly StringHash RES_ANIMATION = new StringHash("Animation");

        [ResourceExtensions("All Images|*.png;*.jpg;*.jpeg;*.bmp;*.tga;*.dds|DDS Images (*.dds)|*.dds", new string[] { ".png", ".jpg", ".jpeg", ".bmp", ".tga", ".dds" })]
        public static readonly StringHash RES_TEXTURE2D = new StringHash("Texture2D");

        [ResourceExtensions("All Images|*.png;*.jpg;*.jpeg;*.bmp;*.tga;*.dds|DDS Images (*.dds)|*.dds", new string[] { ".png", ".jpg", ".jpeg", ".bmp", ".tga", ".dds" })]
        public static readonly StringHash RES_TEXTURE3D = new StringHash("Texture3D");

        [ResourceExtensions("All Images|*.png;*.jpg;*.jpeg;*.bmp;*.tga;*.dds|DDS Images (*.dds)|*.dds", new string[] { ".png", ".jpg", ".jpeg", ".bmp", ".tga", ".dds" })]
        public static readonly StringHash RES_IMAGE = new StringHash("Texture3D");

        [ResourceExtensions("Materials (*.xml)|*.xml", new string[] { ".xml" })]
        public static readonly StringHash RES_MATERIAL = new StringHash("Material");

        [ResourceExtensions("Particle Effects (*.xml)|*.xml", new string[] { ".xml" })]
        public static readonly StringHash RES_PARTICLEEFFECT = new StringHash("ParticleEffect");

        [ResourceExtensions("Scripts (*.as)|*.as", new string[] { ".as" })]
        public static readonly StringHash RES_SCRIPTFILE = new StringHash("ScriptFile");

        [ResourceExtensions("Scripts (*.lua)|*.lua", new string[] { ".lua" })]
        public static readonly StringHash RES_LUASCRIPTFILE = new StringHash("LuaFile");

        [ResourceExtensions("Audio Files (*.wav, *.ogg)|*.wav;*.ogg|WAV Files (*.wav)|*.wav|OGG Files (*.ogg)|*.ogg", new string[] { ".wav" })]
        public static readonly StringHash RES_SOUNDFILE = new StringHash("Sound");

        [ResourceExtensions("PList Files (*.plist)|*.plist", new string[] { ".plist" })]
        public static readonly StringHash RES_PLIST = new StringHash("PList");

        [ResourceExtensions("JSON FIles (*.json)|*.json", new string[] { ".json" })]
        public static readonly StringHash RES_JSONFILE = new StringHash("JSONFile");

        [ResourceExtensions("2D Particle Effects (*.xml)|*.xml", new string[] { ".xml" })]
        public static readonly StringHash RES_PARTICLEEFFECT2D = new StringHash("ParticleEffect2D");

        [ResourceExtensions("Sprite Sheets (*.xml)|*.xml", new string[] { ".xml" })]
        public static readonly StringHash RES_SPRITESHEET2D = new StringHash("SpriteSheet2D");

        [ResourceExtensions("Tiled Maps (*.tmx)|*.tmx", new string[] { ".tmx" })]
        public static readonly StringHash RES_TMXFILE2D = new StringHash("TmxFile2D");

        [ResourceExtensions("Sprites (*.xml)|*.xml", new string[] { ".xml" })]
        public static readonly StringHash RES_SPRITE2D = new StringHash("Sprite2D");

        [ResourceExtensions("Spriter Animations (*.scml, *.xml)|*.scml;*.xml", new string[] { ".scml", ".xml" })]
        public static readonly StringHash RES_ANIMATIONSET2D = new StringHash("AnimationSet2D");

        public static string GetFilters(string resType)
        {
            StringHash me = new StringHash(resType);
            foreach (FieldInfo fi in typeof(UrhoConstants).GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                StringHash val = fi.GetValue(null) as StringHash;
                if (val.Value == me.Value)
                {
                    ResourceExtensions ext = fi.GetCustomAttribute<ResourceExtensions>();
                    if (ext != null)
                        return ext.Filter;
                    return null;
                }
            }
            return null;
        }

        public static string GetFilters(string[] resTypes)
        {
            StringBuilder ret = new StringBuilder();
            foreach (string s in resTypes)
            {
                StringHash me = new StringHash(s);
                foreach (FieldInfo fi in typeof(UrhoConstants).GetFields(BindingFlags.Static | BindingFlags.Public))
                {
                    StringHash val = fi.GetValue(null) as StringHash;
                    if (val.Value == me.Value)
                    {
                        ResourceExtensions ext = fi.GetCustomAttribute<ResourceExtensions>();
                        if (ret.Length > 0)
                            ret.Append("|");
                        ret.Append(ext.Filter);
                    }
                }
            }
            return ret.ToString();
        }

        public static string GetFilters(ResourceRef resource)
        {
            foreach (FieldInfo fi in typeof(UrhoConstants).GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                StringHash val = fi.GetValue(null) as StringHash;
                if (val.Value == (uint)resource.GetResourceTypeInt())
                {
                    ResourceExtensions ext = fi.GetCustomAttribute<ResourceExtensions>();
                    if (ext != null)
                        return ext.Filter;
                    return null;
                }
            }
            return null;
        }

        public static string GetFilters(ResourceRefList refList)
        {
            foreach (FieldInfo fi in typeof(UrhoConstants).GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                StringHash val = fi.GetValue(null) as StringHash;
                if (val.Value == refList.GetResourceType())
                {
                    ResourceExtensions ext = fi.GetCustomAttribute<ResourceExtensions>();
                    if (ext != null)
                        return ext.Filter;
                    return null;
                }
            }
            return null;
        }
    }
}
