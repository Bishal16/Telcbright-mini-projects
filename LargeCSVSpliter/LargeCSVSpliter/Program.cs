using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        
        string large_file_dir = "C:/Users/Mahathir/Documents/visual studio 2015/Projects/LargeCSVSpliter/LargeCSVSpliter/bin/Debug/large_file";
        string split_file_dir = "C:/Users/Mahathir/Documents/visual studio 2015/Projects/LargeCSVSpliter/LargeCSVSpliter/bin/Debug/split_file";


        Directory.CreateDirectory(split_file_dir);

        string[] fileNames = Directory.GetFiles(large_file_dir);

        foreach (string fileName in fileNames)
        {
            string inputFilePath = fileName;
            SplitFile(inputFilePath, split_file_dir, 20000);
        }

        
        Console.WriteLine("CSV file has been successfully split into smaller files.");
    }

    static void SplitFile(string inputFilePath, string outputDirectory, int linesPerFile)
    {
        string[] allLines = File.ReadAllLines(inputFilePath);
        string largeFileName = Path.GetFileNameWithoutExtension(inputFilePath);
        string s = largeFileName + " " + (allLines.Length / 20000).ToString();
        int fileIndex = 1;
        int linesWritten = 0;

        while (linesWritten < allLines.Length)
        {
            int linesToWrite = Math.Min(linesPerFile, allLines.Length - linesWritten);

            string outputFileName = $"{largeFileName}_{fileIndex}";
            string outputFilePath = Path.Combine(outputDirectory, outputFileName);

            File.WriteAllLines(outputFilePath, allLines.Skip(linesWritten).Take(linesToWrite).ToArray());

            linesWritten += linesToWrite;
            fileIndex++;
        }
    }
}