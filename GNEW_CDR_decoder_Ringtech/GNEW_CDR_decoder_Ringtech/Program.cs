using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNEW_CDR_decoder_Ringtech
{
    class Program
    {
        public static void Main()
        {
            string fileLocation = "000197-20230904124050.cdr";
            string[] linesAsString = File.ReadAllLines(fileLocation);
            string[] line;

            foreach (string ln in linesAsString)
            {
                //lines.Add(line.Split(','));
                line = ln.Split(',');

                string startTime = line[11].Trim();
                if (!string.IsNullOrEmpty(startTime))
                {
                    startTime = parseStringToDate(startTime).ToString("yyyy-MM-dd HH:mm:ss");
                }

                string endTime = line[11].Trim();
                if (!string.IsNullOrEmpty(endTime))
                {
                    endTime = parseStringToDate(endTime).ToString("yyyy-MM-dd HH:mm:ss");
                }

                string originatingCallingNumber = line[9];
                string originatingCalledNumber = line[10];

                string terminatingCallingNUmber = line[9];
                string terminatingCalledNUmber = line[10];

                string startTime1 = startTime;
                string endTime1 = endTime;

                string callinigIp = line[13];
                string calledIp = line[14];

                string TrunkGroupName = line[47];

                string duration = line[53].Trim();
                ;
            }



        }
        private static DateTime parseStringToDate(string timestamp)  //20181028051316400 yyyyMMddhhmmssfff
        {
            DateTime dateTime;
            if (DateTime.TryParseExact("20230904123527", "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime)) 
                Console.WriteLine("valid date time format");
            return dateTime;
        }
    }
}
