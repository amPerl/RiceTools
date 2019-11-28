using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiceConvert
{
    class CHPATHFile
    {
        public struct Point
        {
            public float Distance, X, Y, Z;
            public static Point Read(BinaryReader br)
            {
                float t, x, y, z;
                t = br.ReadSingle();
                x = br.ReadSingle();
                y = br.ReadSingle();
                z = br.ReadSingle();
                return new Point { Distance = t, X = x, Y = y, Z = z };
            }
        }

        public struct Path
        {
            public float TotalDistance;
            public Point[] Points;
            public static Path Read(BinaryReader br)
            {
                br.ReadBytes(18 * 4); // unknown

                float totalDistance = br.ReadSingle();
                br.ReadSingle();

                br.ReadBytes(2 * 4); // unknown

                int pointCount = br.ReadInt32();
                Point[] points = new Point[pointCount];
                for (int i = 0; i < pointCount; i++)
                {
                    points[i] = Point.Read(br);
                }

                br.ReadBytes(3 * 4); // unknown

                return new Path { TotalDistance = totalDistance, Points = points };
            }
        }

        public Path[] Paths;

        public static CHPATHFile LoadCHPATH(string path)
        {
            using (var br = new BinaryReader(File.OpenRead(path)))
            {
                br.ReadBytes(37);

                int pathCount = br.ReadInt32();
                Path[] paths = new Path[pathCount];
                
                for (int i = 0; i < pathCount; i++)
                {
                    paths[i] = Path.Read(br);
                }

                return new CHPATHFile { Paths = paths };
            }
        }

        public void SaveOBJ(string objPath)
        {
            using (var writer = new StreamWriter(File.OpenWrite(objPath)))
            {
                int vertCount = 1;
                foreach (var path in Paths)
                {
                    foreach (var point in path.Points)
                        writer.WriteLine($"v {point.X} {point.Y} {point.Z}");

                    int vertsStart = vertCount;
                    writer.Write("l");
                    foreach (var point in path.Points)
                        writer.Write($" {vertCount++}");
                    writer.WriteLine();
                }
            }
        }
    }
}
