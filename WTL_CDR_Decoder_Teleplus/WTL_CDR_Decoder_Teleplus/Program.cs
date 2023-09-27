using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTL_CDR_Decoder_Teleplus
{
    class Program
    {
        public static void Main()
        {
            string fileLocation = "20230904";            
            string[] linesAsString = File.ReadAllLines(fileLocation);
            string[] line;

            foreach (string ln in linesAsString)
            {
                //lines.Add(line.Split(','));
                line = ln.Split(',');
                string unixCallTime = line[1].Trim();      //unix 
                string callTime = line[4].Trim();
                string callDate = line[5].Trim();
                string duration = line[2].Trim();
                string bLegTelephoneNumber = line[8].Trim();
                string cost_of_the_call = line[12].Trim();
                string bLegRoutingID = line[38].Trim();
                string callID = line[44].Trim();
                //string exactCallDuration = line[60].Trim();
                string ipAddrsOfCaller = line[61].Trim();
                string originalDDInumber = line[62].Trim();

                //string originatingCallingNumber = line[66];  
                //string originatingCalledNumber = line[68];  

                //string terminatingCallingNUmber = line[82];
                //string terminatingCalledNUmber = line[84];
            }


            
        }
    }
}
