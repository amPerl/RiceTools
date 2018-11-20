using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace RiceConvert
{
    class HITFile
    {
        public struct Vertex
        {
            public float X, Y, Z, W;
        }
        public Vertex[] Vertices;
        public uint[] Indices;

        public static HITFile LoadHIT(string path)
        {
            var model = new HITFile();
            using (var br = new BinaryReader(File.OpenRead(path)))
            {
                byte[] hit = br.ReadBytes(4);

                uint version = br.ReadUInt32();
                bool extraCoord = version != 20060720;
                bool swapIndices = version >= 20090629;

                uint indicesLength = br.ReadUInt32();
                model.Indices = new uint[indicesLength];

                for (uint i = 0; i < indicesLength; i += 3)
                {
                    model.Indices[i] = br.ReadUInt32();
                    model.Indices[i + 1] = br.ReadUInt32();
                    model.Indices[i + 2] = br.ReadUInt32();
                    if (swapIndices)
                    {
                        uint temp = model.Indices[i + 1];
                        model.Indices[i + 1] = model.Indices[i + 2];
                        model.Indices[i + 2] = temp;
                    }
                }

                uint verticesLength = br.ReadUInt32();
                model.Vertices = new Vertex[verticesLength];

                for (uint i = 0; i < verticesLength; i++)
                {
                    float x = br.ReadSingle();
                    float y = br.ReadSingle();
                    float z = br.ReadSingle();
                    float w = extraCoord ? br.ReadSingle() : 0;
                    model.Vertices[i] = new Vertex
                    {
                        X = x,
                        Y = y,
                        Z = z,
                        W = w,
                    };
                }
            }
            return model;
        }

        public void SaveHIT(string path)
        {
            using (var writer = new BinaryWriter(File.OpenWrite(path)))
            {
                writer.Write(Encoding.ASCII.GetBytes("HIT\0"));
                writer.Write((uint)20060720);

                writer.Write((uint)Indices.Length);
                foreach (var index in Indices)
                    writer.Write(index);

                writer.Write((uint)Vertices.Length);
                foreach (var vertex in Vertices)
                {
                    writer.Write(vertex.X);
                    writer.Write(vertex.Y);
                    writer.Write(vertex.Z);
                }
            }
        }

        public static HITFile LoadOBJ(string path)
        {
            var vertices = new List<Vertex>();
            var indices = new List<uint>();

            using (var reader = new StreamReader(File.OpenRead(path)))
            {
                var lines = reader.ReadToEnd().Split('\n');
                foreach (var line in lines)
                {
                    string[] split = line.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (split.Length <= 1)
                        continue;

                    if (split[0] == "v")
                    {
                        float x = float.Parse(split[1], NumberStyles.Any, CultureInfo.InvariantCulture);
                        float y = float.Parse(split[2], NumberStyles.Any, CultureInfo.InvariantCulture);
                        float z = float.Parse(split[3], NumberStyles.Any, CultureInfo.InvariantCulture);
                        float w = split.Length > 4 ? float.Parse(split[4], NumberStyles.Any, CultureInfo.InvariantCulture) : 0f;
                        vertices.Add(new Vertex
                        {
                            X = x,
                            Y = y,
                            Z = z,
                            W = w,
                        });
                    }
                    else if (split[0] == "f")
                    {
                        indices.Add(uint.Parse(split[1].Split('/')[0]) - 1);
                        indices.Add(uint.Parse(split[2].Split('/')[0]) - 1);
                        indices.Add(uint.Parse(split[3].Split('/')[0]) - 1);
                    }
                }
            }
            return new HITFile
            {
                Vertices = vertices.ToArray(),
                Indices = indices.ToArray(),
            };
        }

        public void SaveOBJ(string path)
        {
            using (var writer = new StreamWriter(File.OpenWrite(path)))
            {
                foreach (var vertex in Vertices)
                    writer.WriteLine($"v {vertex.X} {vertex.Y} {vertex.Z} {vertex.W}");

                for (int i = 0; i < Indices.Length; i += 3)
                {
                    uint first = Indices[i];
                    uint second = Indices[i + 1];
                    uint third = Indices[i + 2];
                    writer.WriteLine($"f {first + 1} {second + 1} {third + 1}");
                }
            }
        }
    }
}
