using System;
using System.IO;
using System.Linq;
using SharpCompress.Archives;
using SharpCompress.Common;

namespace RAR
{
    class Program
    {
        static void Main(string[] args)
        {
            string rarFilePath = "2_Nov.rar";  // Replace with the path to your .rar file
            string extractPath = "outputDirectory";  // Replace with the directory where you want to extract the contents

            using (Stream stream = File.OpenRead(rarFilePath))
            using (var archive = ArchiveFactory.Open(stream))
            {
                foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                {
                    entry.WriteToDirectory(extractPath, new ExtractionOptions
                    {
                        ExtractFullPath = true,
                        Overwrite = true
                    });
                }
            }

            Console.WriteLine("Extraction completed.");
        }
    }
}
