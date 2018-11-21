using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ionic.Zlib;

namespace RicePack.Archives
{
    public class AGTFolder : ArchiveFolder
    {
        public AGTFolder(AGT agt) : base(agt) { }
        public AGTFolder(AGT agt, AGTFolder parent, string name) : base(agt, parent, name) { }

        public override ArchiveFile AddFile(string filePath)
        {
            var file = new AGTFile(this, Path.GetFileName(filePath), filePath);

            Files.Add(file);
            Archive.FileList.Add(file);

            return file;
        }
    }

    public class AGTFile : ArchiveFile
    {
        public byte[][] CompressedChunks;
        public int DecompressedLength;

        public AGTFile(AGTFolder parent, string name, byte[][] compressedChunks, int decompressedLength) : base(parent, name)
        {
            CompressedChunks = compressedChunks;
            DecompressedLength = decompressedLength;
        }

        public AGTFile(AGTFolder parent, string name, string filePath) : base(parent, name)
        {
            Load(filePath);
        }

        public byte[] Decompress()
        {
            byte[] decompressed = new byte[DecompressedLength];
            int currentFileBufPos = 0;
            foreach (var chunk in CompressedChunks)
            {
                byte[] decompressedChunk = DeflateStream.UncompressBuffer(chunk);
                if (currentFileBufPos + decompressedChunk.Length > DecompressedLength)
                {
                    byte[] newDecomp = new byte[currentFileBufPos + decompressedChunk.Length];
                    Buffer.BlockCopy(decompressed, 0, newDecomp, 0, currentFileBufPos);
                    decompressed = newDecomp;
                }
                Buffer.BlockCopy(decompressedChunk, 0, decompressed, currentFileBufPos, decompressedChunk.Length);
                currentFileBufPos += decompressedChunk.Length;
            }
            return decompressed;
        }

        public override void Save(string savePath) => File.WriteAllBytes(savePath, Decompress());

        public override void Load(byte[] fileData)
        {
            CompressedChunks = GetCompressed(fileData);
            DecompressedLength = fileData.Length;
        }

        public override void Load(string loadPath) => Load(File.ReadAllBytes(loadPath));

        public static byte[][] GetCompressed(byte[] decompressed)
        {
            const int chunkSize = 16384;
            int chunkCount = decompressed.Length / chunkSize + 1;

            byte[][] compressedChunks = new byte[chunkCount][];
            for (int i = 0; i < chunkCount; i++)
            {
                int pos = i * chunkSize;
                int size = pos + chunkSize > decompressed.Length ? decompressed.Length - pos : chunkSize;

                byte[] decompressedChunk = new byte[size];
                Buffer.BlockCopy(decompressed, pos, decompressedChunk, 0, size);

                byte[] compressedChunk;
                using (var memoryStream = new MemoryStream())
                {
                    Stream stream = new DeflateStream(memoryStream, CompressionMode.Compress, CompressionLevel.Default);
                    using (stream)
                        stream.Write(decompressedChunk, 0, decompressedChunk.Length);
                    compressedChunk = memoryStream.ToArray();
                }

                byte[] adler = BitConverter.GetBytes(Util.Adler32(decompressedChunk));
                Array.Reverse(adler);

                byte[] finalChunk = new byte[compressedChunk.Length + 4];
                Buffer.BlockCopy(compressedChunk, 0, finalChunk, 0, compressedChunk.Length);
                Buffer.BlockCopy(adler, 0, finalChunk, compressedChunk.Length, 4);

                compressedChunks[i] = finalChunk;
            }

            return compressedChunks;
        }
    }

    public class AGT : ArchiveBase
    {
        private readonly byte[] xorKey;

        public AGT(byte[] key = null) : base()
        {
            xorKey = key ?? Util.GamesCampusKey;
        }

        public override void Load(string filePath)
        {
            byte[] fileBuf = File.ReadAllBytes(filePath);

            for (int i = 32; i < fileBuf.Length; i++)
                fileBuf[i] ^= xorKey[i % xorKey.Length];
            
            var paths = new List<string>();

            using (var ms = new MemoryStream(fileBuf))
            using (var br = new BinaryReader(ms))
            {
                ms.Position = 16;
                int fileCount = br.ReadInt32();

                ms.Position = 32;
                
                RootFolder = new AGTFolder(this);

                for (int i = 0; i < fileCount; i++)
                {
                    int chunkHeadersPos = br.ReadInt32();
                    int chunkCount = br.ReadInt32();
                    int decompressedLength = br.ReadInt32();
                    int pathLength = br.ReadInt32();
                    string path = Encoding.GetEncoding("ISO-8859-1").GetString(br.ReadBytes(pathLength));

                    string[] dirSplit = path.Split('\\');
                    string filename = dirSplit[dirSplit.Length - 1];

                    ushort[] chunkLengths = new ushort[chunkCount];
                    byte[][] compressedChunks = new byte[chunkCount][];

                    using (var chunkms = new MemoryStream(fileBuf))
                    using (var chunkbr = new BinaryReader(chunkms))
                    {
                        chunkms.Position = chunkHeadersPos;

                        for (int chunki = 0; chunki < chunkCount; chunki++)
                            chunkLengths[chunki] = chunkbr.ReadUInt16();

                        for (int chunki = 0; chunki < chunkCount; chunki++)
                        {
                            ushort zlibHeader = chunkbr.ReadUInt16();
                            compressedChunks[chunki] = chunkbr.ReadBytes(chunkLengths[chunki] - 2);
                        }
                    }

                    var activeFolder = RootFolder as AGTFolder;
                    for (int j = 0; j < dirSplit.Length - 1; j++)
                    {
                        string curName = dirSplit[j];

                        var folder = activeFolder.SubFolders.FirstOrDefault(f => f.Name == curName) as AGTFolder;
                        if (folder == null)
                        {
                            folder = new AGTFolder(this, activeFolder, curName);
                            activeFolder.SubFolders.Add(folder);
                        }
                        activeFolder = folder;
                    }

                    var newFile = new AGTFile(activeFolder, filename, compressedChunks, decompressedLength);
                    activeFolder.Files.Add(newFile);
                    FileList.Add(newFile);
                }
            }
        }

        public override void Save(string path)
        {
            int chunkHeadersPos = 32 + FileList.Sum(f => 16 + f.GetFullPath().Length);

            byte[] fileBuf;
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(Encoding.ASCII.GetBytes("NayaPack"));
                bw.Write(0);
                bw.Write((ushort)1);
                bw.Write((ushort)1);
                bw.Write(FileList.Count);
                bw.Write(new byte[12]);

                int chunkHeadersPosOffset = 0;
                foreach (var file in FileList)
                {
                    var agtFile = file as AGTFile;
                    bw.Write(chunkHeadersPos + chunkHeadersPosOffset);
                    chunkHeadersPosOffset += agtFile.CompressedChunks.Sum(chunk => chunk.Length + 4);
                    bw.Write(agtFile.CompressedChunks.Length);
                    bw.Write(agtFile.DecompressedLength);

                    string fullPath = agtFile.GetFullPath();
                    bw.Write(fullPath.Length);
                    bw.Write(Encoding.GetEncoding("ISO-8859-1").GetBytes(fullPath));
                }

                foreach (var file in FileList)
                {
                    var agtFile = file as AGTFile;
                    foreach (var chunk in agtFile.CompressedChunks)
                        bw.Write((ushort)(chunk.Length + 2));
                    foreach (var chunk in agtFile.CompressedChunks)
                    {
                        bw.Write((byte)(120));
                        bw.Write((byte)(156));
                        bw.Write(chunk);
                    }
                }
                fileBuf = ms.ToArray();
            }

            for (int i = 32; i < fileBuf.Length; i++)
                fileBuf[i] ^= xorKey[i % xorKey.Length];

            File.WriteAllBytes(path, fileBuf);
        }
    }
}
