using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopySourceToDestinationBaseFoder
{
    class CopyingResourcesToAllTheIcxs
    {
        static void Main(string[] args)
        {
            string sourceFolder = "c_temp_telcobright";
            string destinationBase = "Destination Base path list";
            string[] operatorList = {"ICX1", "ICX2", "ICX3", "ICX4", "ICX5", "ICX6", "ICX7", "ICX8", "ICX9", "ICX10", "ICX11", "ICX12", "ICX13", "ICX14", "ICX15", "ICX16", "ICX17", "ICX18", "ICX19", "ICX20", "ICX21", "ICX22", "ICX23", "ICX24", "ICX25"};
            CopyResources(sourceFolder, destinationBase, operatorList);            
        }

        public static void CopyResources(string sourceFolder, string destinationBase, string[] operatorList)
        {
            Directory.CreateDirectory(destinationBase);
            foreach (string opt in operatorList)
            {
                string dest = destinationBase+"/"+opt;
                recursiveCopy(sourceFolder, dest);
            }
            //Console.WriteLine("Folder copy complete.");
        }
        public static void recursiveCopy(string sourceFolder, string Destination)
        {
            if (!Directory.Exists(sourceFolder))
            {
                Console.WriteLine($"Source folder '{sourceFolder}' does not exist.");
                return;
            }
            

            //string newDirectoryPath = Path.Combine("DestinationBasePath", Destination);
            Directory.CreateDirectory(Destination);

            string[] files = Directory.GetFiles(sourceFolder);

            
            //copying all the files to destination icx folder
            foreach (string file in files)
            {
                string destinationFilePath = Path.Combine(Destination, Path.GetFileName(file));
                if(!File.Exists(destinationFilePath))
                    File.Copy(file, destinationFilePath);
            }

            //copying all the subFolder to destination folder 
            string[] subfolders = Directory.GetDirectories(sourceFolder);
            foreach (string subfolder in subfolders)
            {
                string destinationSubfolder = Path.Combine(Destination, Path.GetFileName(subfolder));
                recursiveCopy(subfolder, destinationSubfolder);
            }                        
        }
    }
}
