using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;

namespace UrhoInterop.Model
{
    public static class ModelLoader
    {
        public static Model ReadModel(BinaryReader rdr)
        {
            byte[] magic = rdr.ReadBytes(4);
            if (!BitConverter.ToString(magic, 0).Equals("UMDL"))
                return null;

            Model ret = new Model();

            uint vertexBufferCt = rdr.ReadUInt32();
            //Read the vertex buffers
            for (uint i = 0; i < vertexBufferCt; ++i)
            {
                uint vertCt = rdr.ReadUInt32();
                uint vertMask = rdr.ReadUInt32();
                uint vertexSize = GetVertexSize(vertMask);
                uint morphStart = rdr.ReadUInt32();
                uint morphCt = rdr.ReadUInt32();
                byte[] vertexData = rdr.ReadBytes((int)(vertCt * vertexSize));
                ret.VertexBuffers.Add(new VertexBuffer(vertexData, (int)vertexSize));
            }

            uint indexBufferCt = rdr.ReadUInt32();
            for (uint i = 0; i < indexBufferCt; ++i)
            {
                uint indexCt = rdr.ReadUInt32();
                uint indexSize = rdr.ReadUInt32();
                byte[] indexData = rdr.ReadBytes((int)(indexCt * indexSize));
                ret.IndexBuffers.Add(new IndexBuffer(indexData));
            }

            uint geomCt = rdr.ReadUInt32();
            for (uint i = 0; i < geomCt; ++i)
            {
                Geometry geom = new Geometry();

                uint boneMapCt = rdr.ReadUInt32();
                byte[] boneMappingBytes = rdr.ReadBytes((int)(sizeof(uint) * boneMapCt));
                uint[] boneMapping = new uint[boneMapCt];
                for (int idx = 0, boneIdx = 0; idx < boneMappingBytes.Length; idx += 4, boneIdx += 1)
                {
                    boneMapping[boneIdx] = BitConverter.ToUInt32(boneMappingBytes, idx);
                }
                geom.BoneMapping = boneMapping;

                uint lodCt = rdr.ReadUInt32();

                for (int lodIdx = 0; lodIdx < lodCt; ++lodIdx)
                {
                    float dist = rdr.ReadSingle();
                    uint prim = rdr.ReadUInt32();
                    uint vertBufferIndex = rdr.ReadUInt32();
                    uint indexBufferIndex = rdr.ReadUInt32();
                    uint drawRangeStart = rdr.ReadUInt32();
                    uint drawRangeEnd = rdr.ReadUInt32();

                    LOD lod = new LOD();
                    lod.VertexBuffer = ret.VertexBuffers[(int)vertBufferIndex];
                    lod.IndexBuffer = ret.IndexBuffers[(int)indexBufferIndex];
                    lod.IndexStart = drawRangeStart;
                    lod.IndexEnd = drawRangeEnd;
                    lod.Distance = dist;
                    lod.Primitive = prim;

                    geom.LODs.Add(lod);
                }

                ret.Geometries.Add(geom);
            }

            uint vertexMorphCt = rdr.ReadUInt32();

            for (int i = 0; i < vertexMorphCt; ++i)
            {
                string name = ReadCString(rdr);
                uint affectedBuffers = rdr.ReadUInt32();

                VertexMorph morph = new VertexMorph();
                morph.Name = name;

                for (int bufIdx = 0; bufIdx < affectedBuffers; ++i)
                {
                    MorphAffectedBuffer buff = new MorphAffectedBuffer();
                    buff.Idx = rdr.ReadUInt32();
                    buff.Mask = rdr.ReadUInt32();
                    buff.VertCt = rdr.ReadUInt32();
                    buff.Buffer = ret.VertexBuffers[(int)buff.Idx];
                    morph.Buffers.Add(buff);

                    for (int vertIdx = 0; vertIdx < buff.VertCt; ++vertIdx)
                    {
                        uint morphVertexIndex = rdr.ReadUInt32();
                        //\todo check mask
                        MorphVertex vert = new MorphVertex();

                        if ((buff.Mask & 0x1) != 0)
                            vert.Position = ReadVector3(rdr);
                        if ((buff.Mask & 0x2) != 0)
                            vert.Normal = ReadVector3(rdr);
                        if ((buff.Mask & 0x80) != 0)
                            vert.Tangent = ReadVector3(rdr);
                        buff.Vertices.Add(vert);
                    }
                }
            }

            //Skeleton data
            Skeleton skel = new Skeleton();

            uint boneCount = rdr.ReadUInt32();
            for (int i = 0; i < boneCount; ++i)
            {
                Bone bone = new Bone();
                bone.Name = ReadCString(rdr);
                bone.Parent = rdr.ReadUInt32();
                bone.Position = ReadVector3(rdr);
                bone.Rotation = ReadVector4(rdr);
                bone.Scale = ReadVector3(rdr);

                byte[] offsetTrans = rdr.ReadBytes(sizeof(float) * 12);
                bone.ColMask = rdr.ReadByte();
                if ((bone.ColMask & Bone.BONECOLLISION_SPHERE) != 0)
                {
                    bone.ColRadius = rdr.ReadSingle();
                }
                else if ((bone.ColMask & Bone.BONECOLLISION_BOX) != 0)
                {
                    bone.BoundsMin = ReadVector3(rdr);
                    bone.BoundsMax = ReadVector3(rdr);
                }
                skel.Bones.Add(bone);
            }

            ret.Skeleton = skel;

            ret.BoundsMin = ReadVector3(rdr);
            ret.BoundsMax = ReadVector3(rdr);

            for (int i = 0; i < geomCt; ++i)
            {
                ret.Geometries[i].Center = ReadVector3(rdr);
            }

            return ret;
        }

        static uint[] elementSize =
            {
                3 * sizeof(float), // Position
                3 * sizeof(float), // Normal
                4 * sizeof(char), // Color
                2 * sizeof(float), // Texcoord1
                2 * sizeof(float), // Texcoord2
                3 * sizeof(float), // Cubetexcoord1
                3 * sizeof(float), // Cubetexcoord2
                4 * sizeof(float), // Tangent
                4 * sizeof(float), // Blendweights
                4 * sizeof(char), // Blendindices
                4 * sizeof(float), // Instancematrix1
                4 * sizeof(float), // Instancematrix2
                4 * sizeof(float) // Instancematrix3
            };

        static uint GetVertexSize(uint elementMask)
        {
            uint vertexSize = 0;
    
            for (int i = 0; i < 13; ++i) //13, magic max vertex elements
            {
                if ((elementMask & (1 << i)) != 0)
                    vertexSize += elementSize[i];
            }
    
            return vertexSize;
        }

        public static string ReadCString(BinaryReader rdr)
        {
            string ret = "";
            char c = rdr.ReadChar();
            while (c != '\0')
            {
                ret += c;
                c = rdr.ReadChar();
            }
            return "";
        }

        public static Vector3 ReadVector3(BinaryReader rdr)
        {
            Vector3 ret = new Vector3();
            ret.X = rdr.ReadSingle();
            ret.Y = rdr.ReadSingle();
            ret.Z = rdr.ReadSingle();
            return ret;
        }

        public static Vector4 ReadVector4(BinaryReader rdr)
        {
            Vector4 ret = new Vector4();
            ret.X = rdr.ReadSingle();
            ret.Y = rdr.ReadSingle();
            ret.Z = rdr.ReadSingle();
            ret.W = rdr.ReadSingle();
            return ret;
        }

        public static void WriteModel(Model model, BinaryWriter writer)
        {
            writer.Write('U'); writer.Write('M'); writer.Write('D'); writer.Write('L');
            writer.Write((uint)model.VertexBuffers.Count);
                foreach (VertexBuffer buffer in model.VertexBuffers)
                {
                    
                }

            writer.Write((uint)model.IndexBuffers.Count);
                foreach (IndexBuffer buffer in model.IndexBuffers)
                {

                }

            writer.Write((uint)model.Geometries.Count);
                foreach (Geometry geom in model.Geometries)
                {

                }
        }
    }
}
