using System;
using System.IO;
using System.Linq;
using SharpCompress.Archives;
using SharpCompress.Common;
using ICSharpCode.SharpZipLib.GZip;
using System.Diagnostics;

namespace unzip_tar_Z_file
{
    class Program
    {
        static void Main(string[] args)
        {
            string tarzFilePath = "C://Users//Mahathir//Documents//Visual Studio 2015//Projects//unzip_tar_Z_file//unzip_tar_Z_file//bin//Debug//bt0104.tar.Z";  
            string targetPath = "C://Users//Mahathir//Documents//Visual Studio 2015//Projects//unzip_tar_Z_file//unzip_tar_Z_file//bin//Debug//outputDirectory"; 
            string Command = "tar -zxvf " + "\""+ tarzFilePath + "\"" + " -C " + "\"" + targetPath + "\""; 

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "cmd.exe", 
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                UseShellExecute = false,
            };

            Process process = new Process { StartInfo = psi };

            process.Start();
            process.StandardInput.WriteLine(Command);
            process.StandardInput.WriteLine("exit");
            process.WaitForExit();

            if (process.ExitCode == 0)
            {
                Console.WriteLine("Extraction completed.");
            }
            else
            {
                Console.WriteLine("Extraction Failed.");
            }


            //// Specify the command to extract the files (tar -zxvf)
            //string command = "tar -zxvf bt0104.tar.Z";

            //// Specify the directory where you want to extract the files
            //string extractDirectory = "outputDirectory"; // Replace with the desired output directory

            //// Create a process start info
            //ProcessStartInfo psi = new ProcessStartInfo
            //{
            //    FileName = "cmd.exe", // Use "/bin/bash" on Linux, "sh" is used for a generic shell
            //    RedirectStandardInput = true,
            //    RedirectStandardOutput = true,
            //    CreateNoWindow = true,
            //    UseShellExecute = false,
            //    WorkingDirectory = extractDirectory // Set the working directory to the extraction directory
            //};

            //// Create a process
            //Process process = new Process { StartInfo = psi };

            //// Start the process
            //process.Start();

            //// Send the command to the shell
            //process.StandardInput.WriteLine(command);

            //// Exit the shell
            //process.StandardInput.WriteLine("exit");

            //// Wait for the process to exit
            //process.WaitForExit();

            //// Check if the extraction was successful
            //if (process.ExitCode == 0)
            //{
            //    Console.WriteLine("Extraction completed successfully.");
            //}
            //else
            //{
            //    Console.WriteLine("Extraction failed. Check the command or file paths.");
            //}



        }


    }
}
