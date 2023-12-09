using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace getAllFilesFromGivenDirectory
{
    class VaultFileMover
    {
        public string RootDir;
        public string Prefix;
        public string Extension;

        public List<FileInfo> AllFileInfos { get; } = new List<FileInfo>();
        public List<FileInfo> ZipFileInfos { get; } = new List<FileInfo>();
        public List<FileInfo> CdrFileInfos { get; } = new List<FileInfo>();
        public List<FileInfo> UnwantedFileInfos { get; } = new List<FileInfo>();

        private static HashSet<string> extensions = new HashSet<string> { ".gz", ".7z", ".zip", ".tar" };
        

        public VaultFileMover(string prefix, string extension, string rootDir)
        {
            this.Prefix = prefix;
            this.Extension = extension;
            this.RootDir = rootDir;
            getFilesInfoRecursively(RootDir);   // this will populate the AllFileInfos member variable
            getZipFiles();
            getCdrFiles(Prefix, Extension);
            getUnwantedFile();
        }        

        private void getFilesInfoRecursively(string parentDir)
        {
            string[] filePahts = Directory.GetFiles(parentDir);

            foreach (string filePath in filePahts)
            {
                FileInfo fileInfo = new FileInfo(filePath);
                AllFileInfos.Add(fileInfo);                
            }

            string[] subDirs = Directory.GetDirectories(parentDir);
            foreach (string subDir in subDirs)
            {
                if(!Directory.GetFileSystemEntries(subDir).Any())   // if a sub directory is empty then it will be deleted
                {
                    Directory.Delete(subDir);
                    continue;
                }
                getFilesInfoRecursively(subDir);
            }            
        }        

        public void getZipFiles()
        {
            foreach(FileInfo fileInfo in AllFileInfos)
            {
                if (extensions.Contains(fileInfo.Extension))
                {
                    ZipFileInfos.Add(fileInfo);
                }
            }
        }

        public void getCdrFiles(string prefix, string extension)
        {
            foreach (FileInfo fileInfo in AllFileInfos)
            {
                if(fileInfo.Name.StartsWith(prefix) && fileInfo.Extension == extension)
                {
                    CdrFileInfos.Add(fileInfo);     // this will populate the CdrFileInfos member variable
                }
            }
        }

        public void getUnwantedFile()
        {
            foreach(FileInfo fileInfo in AllFileInfos)
            {
                if(!ZipFileInfos.Contains(fileInfo) && !CdrFileInfos.Contains(fileInfo))
                {
                    UnwantedFileInfos.Add(fileInfo);
                }
            }
        }

        public void moveFiles(string targetDir, List<FileInfo> filesToMove)
        {
            foreach (FileInfo fileInfo in filesToMove)
            {
                string originalFile = Path.Combine(fileInfo.DirectoryName, fileInfo.Name);
                string tmpFile = Path.Combine(targetDir, fileInfo.Name + ".tmp");

                File.Copy(originalFile, tmpFile);      // file copying here

                FileInfo originalFileInfo = new FileInfo(originalFile);
                FileInfo tempFileInfo = new FileInfo(tmpFile);

                if (originalFileInfo.Length == tempFileInfo.Length)
                {
                    File.Delete(originalFile);          //deleting original file
                    File.Move(tmpFile, tmpFile.Remove(tmpFile.Length - 4, 4));  //renaming tmp file to its original name
                }
            }
        }


        

        static void Main(string[] args)
        {
            string parentDir = "C:/Users/Mahathir/Desktop/test";
            //string targetDir = "C:/Users/Mahathir/Desktop/test";
            //getFilesInfoRecursively(parentDir);

            //moveFiles(parentDir);

            VaultFileMover obj = new VaultFileMover("esdr", ".dat", parentDir);
            obj.moveFiles(parentDir, obj.CdrFileInfos);
        }
    }
}
