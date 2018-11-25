using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RicePack.Archives
{
    public class ArchiveFolder
    {
        public ArchiveBase Archive;
        public ArchiveFolder ParentFolder;
        public string Name;

        public List<ArchiveFolder> SubFolders = new List<ArchiveFolder>();
        public List<ArchiveFile> Files = new List<ArchiveFile>();

        public ArchiveFolder(ArchiveBase archive)
        {
            Archive = archive;
            ParentFolder = null;
            Name = "";
        }

        public ArchiveFolder(ArchiveBase archive, ArchiveFolder parent, string name)
        {
            Archive = archive;
            ParentFolder = parent;
            Name = name;
        }

        public string FullPath
        {
            get
            {
                if (ParentFolder == null) return "";
                return ParentFolder.FullPath + Name + "\\";
            }
        }

        public virtual ArchiveFile AddFile(string filePath)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class ArchiveFile
    {
        public ArchiveFolder ParentFolder;
        public string Name;

        public ArchiveFile(ArchiveFolder parent, string name)
        {
            ParentFolder = parent;
            Name = name;
        }

        public string GetFullPath()
        {
            return Path.Combine(ParentFolder.FullPath, Name);
        }

        public virtual void Load(string loadPath)
        {
            throw new NotImplementedException();
        }

        public virtual void Load(byte[] fileData)
        {
            throw new NotImplementedException();
        }

        public virtual void Save(string savePath)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class ArchiveBase
    {
        public List<ArchiveFile> FileList;

        public ArchiveFolder RootFolder;

        public ArchiveBase()
        {
            FileList = new List<ArchiveFile>();
        }

        public virtual void Load(string filePath)
        {
            throw new NotImplementedException();
        }

        public virtual void Save(string savePath)
        {
            throw new NotImplementedException();
        }
    }
}
