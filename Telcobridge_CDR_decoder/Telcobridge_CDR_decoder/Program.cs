using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telcobridge_CDR_decoder
{
    class Program
    {
        public static void Main()
        {
            string fileLocation = "GNL_BOG-B_CDR_2023-08-20_06-25-00.log";
            string[] linesAsString = File.ReadAllLines(fileLocation);
            string[] line;

            foreach (string ln in linesAsString)
            {
                //lines.Add(line.Split(','));
                line = ln.Split(',');

                string startTime = line[4].Trim();
                if (!string.IsNullOrEmpty(startTime))
                {
                    startTime = parseStringToDate(startTime).ToString("yyyy-MM-dd HH:mm:ss");
                }

                
                string endTime = line[6].Trim();
                if (!string.IsNullOrEmpty(endTime))
                {
                    endTime = parseStringToDate(endTime).ToString("yyyy-MM-dd HH:mm:ss");
                }

                string connectTime = line[5].Trim();
                if (!string.IsNullOrEmpty(connectTime))
                {
                    connectTime = parseStringToDate(connectTime).ToString("yyyy-MM-dd HH:mm:ss");
                }

                string originatingCallingNumber = line[8];
                string originatingCalledNumber = line[9];

                string terminatingCallingNUmber = line[8];
                string terminatingCalledNUmber = line[9];

                string startTime1 = startTime;
                string endTime1 = endTime;
                string connectTime1 = connectTime;

               

                string TrunkGroupName = line[47];

                string duration = line[7].Trim();
                ;
            }



        }
        private static DateTime parseStringToDate(string timestamp)  
        {
            DateTime dateTime;
            if (DateTime.TryParseExact("20230904123527", "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                Console.WriteLine("valid date time format");
            return dateTime;
        }
    }
}
