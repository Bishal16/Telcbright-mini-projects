using Aspose.Zip.SevenZip;
using System;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;


namespace readFromGzFile
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "sdr.bn4k.BorderNet-SBC.dhksbc1.20230531.150020.000005.0.v1.0.csv.gz";
            string[] list = readFromCompressdFile(fileName);

        }
        static string[] readFromCompressdFile(string fileName)
        {
            string compressedFile = fileName;
            string destinationFolder = "Sample_ExtractionFolder";

            //creating temp directory if not exists

            //string curPath = AppDomain.CurrentDomain.BaseDirectory;
            //if (!Directory.Exists(curPath + "\\temp"))
            //{
            //    Directory.CreateDirectory("temp");

            //}
            //string tempFilePath = "temp\\tmpFile.txt";

            //Directory.SetCurrentDirectory(path + "\\temp");


            // Extract the .gz file into the temporary file


            // Load input 7z (7zip) Archive with SevenZipArchive class.
            //using System.Diagnostics;

            string command = $"x \"{fileName}\" -o\"{destinationFolder}\"";            

            string zPath = @"C:\Program Files\7-Zip\7zG.exe";// change the path and give yours 
            try
            {
                ProcessStartInfo pro = new ProcessStartInfo();
                pro.WindowStyle = ProcessWindowStyle.Hidden;
                pro.FileName = zPath;
                pro.Arguments = "x \"" + fileName + "\" -o" + destinationFolder;
                Process x = Process.Start(pro);
                x.WaitForExit();
            }
            catch (System.Exception Ex)
            {
                //DO logic here 
            }
            ;
            // Read all lines from the temporary file
            //string[] lines = File.ReadAllLines(tempFilePath);
            // Delete temp directory
            //Directory.Delete(curPath + "\\temp", true);
            string[] ln = { };
            return ln;
        }
    }
}


/*
using System;
using System.IO;
using System.IO.Compression;

namespace readFromGzFile
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "sdr.bn4k.BorderNet-SBC.dhksbc1.20230531.150020.000005.0.v1.0.csv.gz";
            string[] list = readFromCompressdFile(fileName);
            
        }
        static string[] readFromCompressdFile(string fileName)
        {
            string compressedFile = fileName;            

            //creating temp directory if not exists
            string curPath = AppDomain.CurrentDomain.BaseDirectory;
            if(!Directory.Exists(curPath+"\\temp"))
            {
                Directory.CreateDirectory("temp");
                
            }
            string tempFilePath = "temp\\tmpFile.txt";

            //Directory.SetCurrentDirectory(path + "\\temp");


            // Extract the .gz file into the temporary file
            using (FileStream gzFileStream = File.OpenRead(compressedFile))
            {
                using (FileStream tempFileStream = File.Create(tempFilePath))
                {
                    using (GZipStream gzipStream = new GZipStream(gzFileStream, CompressionMode.Decompress))
                    {
                        gzipStream.CopyTo(tempFileStream);
                    }
                }
            }

            // Read all lines from the temporary file
            string[] lines = File.ReadAllLines(tempFilePath);
            // Delete temp directory
            Directory.Delete(curPath+"\\temp", true);            
            return lines;
        }
    }
}


*/
