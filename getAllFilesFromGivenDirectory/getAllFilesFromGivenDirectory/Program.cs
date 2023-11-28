using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace getAllFilesFromGivenDirectory
{
    class Program
    {
        public static List<FileInfo> getFilesInfoRecursively(string targetDir, List<FileInfo> fileInfoList)
        {
            
            HashSet<string> extensions = new HashSet<string> { ".gz", ".7z", ".zip", ".tar" };
            string[] filePahts = Directory.GetFiles(targetDir);

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
            string[] subDirs = Directory.GetDirectories(targetDir);
            foreach (string subDir in subDirs)
            {
                getFilesInfoRecursively(subDir, fileInfoList);
            }
            return fileInfoList;
        }

        static void Main(string[] args)
        {
            string targetDir = "C:/Users/Mahathir/Desktop/test";
            List<FileInfo> fileInfoList = new List<FileInfo>();
            getFilesInfoRecursively(targetDir, fileInfoList);
            foreach(FileInfo info in fileInfoList)
            {
                Console.WriteLine(info);
            }
            ;
        }
    }
}
