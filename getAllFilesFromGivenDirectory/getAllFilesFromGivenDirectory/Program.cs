using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace getAllFilesFromGivenDirectory
{
    class FileMover
    {
        private static HashSet<string> extensions = new HashSet<string> { ".gz", ".7z", ".zip", ".tar" };
        

        private static List<FileInfo> getFilesInfoRecursively(string parentDir, List<FileInfo> fileInfoList)
        {
            string[] filePahts = Directory.GetFiles(parentDir);

            foreach (string filePath in filePahts)
            {                
                string fileName = Path.GetFileName(filePath);
                string fileExt = Path.GetExtension(fileName);

                FileInfo fileInfo = new FileInfo(filePath);
                if (extensions.Contains(fileExt))
                {   
                    fileInfoList.Add(fileInfo);
                }
            }

            string[] subDirs = Directory.GetDirectories(parentDir);
            foreach (string subDir in subDirs)
            {
                getFilesInfoRecursively(subDir, fileInfoList);
            }
            return fileInfoList;
        }


        public static void moveFiles(string parentDir)
        {
            List<FileInfo> fileInfoList = new List<FileInfo>();
            getFilesInfoRecursively(parentDir, fileInfoList);

            foreach (FileInfo fileInfo in fileInfoList)
            {
                string originalFile = Path.Combine(fileInfo.DirectoryName, fileInfo.Name);
                string tmpFile = Path.Combine(parentDir, fileInfo.Name + ".tmp");

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
            moveFiles(parentDir);
            
        }
    }
}
