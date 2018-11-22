using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RicePack.Archives
{

    public class NTXFolder : ArchiveFolder
    {
        public NTXFolder(NTX lof) : base(lof) { }
        public NTXFolder(NTX lof, NTXFolder parent, string name) : base(lof, parent, name) { }

        public override ArchiveFile AddFile(string filePath)
        {
            var file = new NTXFile(this, Path.GetFileName(filePath), File.ReadAllBytes(filePath));

            Files.Add(file);
            Archive.FileList.Add(file);

            return file;
        }
    }

    public class NTXFile : ArchiveFile
    {
        public byte[] Data;

        public NTXFile(NTXFolder parent, string name, byte[] data) : base(parent, name)
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

    public class NTX : ArchiveBase
    {
        public NTX() : base() { }

        private string readNullTerminated(BinaryReader br)
        {
            var sb = new StringBuilder();
            byte lastByte = br.ReadByte();
            while (lastByte > 0)
            {
                sb.Append((char)lastByte);
                lastByte = br.ReadByte();
            }
            return sb.ToString();
        }

        public override void Load(string filePath)
        {
            RootFolder = new NTXFolder(this);

            using (var fs = File.OpenRead(filePath))
            using (var br = new BinaryReader(fs))
            {
                while (br.PeekChar() >= 0)
                {
                    string name = Encoding.ASCII.GetString(br.ReadBytes(64)).Split('\0')[0];
                    int length = br.ReadInt32();
                    fs.Seek(4, SeekOrigin.Current);
                    byte[] data = br.ReadBytes(length);
                    
                    var newFile = new NTXFile(RootFolder as NTXFolder, name, data);
                    RootFolder.Files.Add(newFile);
                    FileList.Add(newFile);
                }
            }
        }

        public override void Save(string filePath)
        {
            RootFolder = new NTXFolder(this);

            using (var fs = File.OpenWrite(filePath))
            using (var bw = new BinaryWriter(fs))
            {
                foreach (var file in FileList)
                {
                    var ntxFile = file as NTXFile;

                    var nameBlock = new byte[64];
                    var nameBytes = Encoding.ASCII.GetBytes(file.Name);
                    Buffer.BlockCopy(nameBytes, 0, nameBlock, 0, nameBytes.Length);
                    bw.Write(nameBlock);

                    bw.Write(ntxFile.Data.Length);
                    bw.Write(0);
                    bw.Write(ntxFile.Data);
                }
            }
        }
    }
}
