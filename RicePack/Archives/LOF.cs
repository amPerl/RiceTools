using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RicePack.Archives
{
    public class LOFFolder : ArchiveFolder
    {
        public LOFFolder(LOF lof) : base(lof) { }
        public LOFFolder(LOF lof, LOFFolder parent, string name) : base(lof, parent, name) { }

        public override ArchiveFile AddFile(string filePath)
        {
            var file = new LOFFile(this, Path.GetFileName(filePath), File.ReadAllBytes(filePath));

            Files.Add(file);
            Archive.FileList.Add(file);

            return file;
        }
    }

    public class LOFFile : ArchiveFile
    {
        public byte[] Data;

        public LOFFile(LOFFolder parent, string name, byte[] data) : base(parent, name)
        {
            Data = data;
        }

        public override void Load(byte[] fileData)
        {
            Data = fileData;
        }

        public override void Load(string loadPath)
        {
            Load(File.ReadAllBytes(loadPath));
        }

        public override void Save(string savePath)
        {
            File.WriteAllBytes(savePath, Data);
        }
    }

    public class LOF : ArchiveBase
    {
        public LOF() : base() { }

        public override void Load(string filePath)
        {
            using (var fs = File.OpenRead(filePath))
            using (var br = new BinaryReader(fs))
            {
                fs.Seek(16, SeekOrigin.Begin);
                int fileCount = br.ReadInt32();
                fs.Seek(4, SeekOrigin.Current);

                RootFolder = new LOFFolder(this);

                for (int i = 0; i < fileCount; i++)
                {
                    fs.Seek(4 * 6 + 1, SeekOrigin.Current); // 6 ints, 1 byte
                    var sb = new StringBuilder();
                    byte lastByte = br.ReadByte();
                    while (lastByte > 0)
                    {
                        sb.Append((char)lastByte);
                        lastByte = br.ReadByte();
                    }
                    string fileName = sb.ToString();
                    fs.Seek(4 * 3, SeekOrigin.Current); // 3 ints
                    int filePos = br.ReadInt32();
                    int fileLen = br.ReadInt32();

                    long tmpPos = fs.Position;
                    fs.Seek(filePos, SeekOrigin.Begin);
                    byte[] data = br.ReadBytes(fileLen);
                    fs.Seek(tmpPos, SeekOrigin.Begin);

                    var newFile = new LOFFile(RootFolder as LOFFolder, fileName, data);
                    RootFolder.Files.Add(newFile);
                    FileList.Add(newFile);
                }
            }
        }
    }
}
