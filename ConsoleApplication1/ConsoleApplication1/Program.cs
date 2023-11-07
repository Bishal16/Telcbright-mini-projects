using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            string folder = @"E:\telcobright\vault\resources\CDR\teleplusnewyork\ip";
            DirectoryInfo dir = new DirectoryInfo(folder);
            foreach(FileInfo fInfo in dir.GetFiles())
            {
                List<string> lines = File.ReadAllLines(fInfo.FullName).ToList();
                int splitCount = 1;
                int segmentSize = 20000;
                int skip = 0;
                int linesSoFar = 0;
                List<string> newSegment= lines.Skip(skip).Take(segmentSize).ToList();
                while (true)
                {
                    string splitFileName = fInfo.DirectoryName + Path.DirectorySeparatorChar +"split"
                        +Path.DirectorySeparatorChar + fInfo.Name + "_" + splitCount;
                    File.WriteAllLines(splitFileName, newSegment);
                    skip += segmentSize;
                    splitCount += 1;
                    newSegment = lines.Skip(skip).Take(segmentSize).ToList();
                    if (newSegment.Any() == false) break;
                }
            }
        }
    }
}
