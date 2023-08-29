using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDR_range_check
{    
    static class CidrRangeChecker
    {
        static string ConvertIptoBin(string IP)
        {
            string[] ipArr;
            ipArr = IP.Split('.', '/');

            string ip_bin1 = Convert.ToString(Convert.ToInt32(ipArr[0]), 2).PadLeft(8, '0');
            string ip_bin2 = Convert.ToString(Convert.ToInt32(ipArr[1]), 2).PadLeft(8, '0');
            string ip_bin3 = Convert.ToString(Convert.ToInt32(ipArr[2]), 2).PadLeft(8, '0');
            string ip_bin4 = Convert.ToString(Convert.ToInt32(ipArr[3]), 2).PadLeft(8, '0');

            string ipBinConcat = ip_bin1 + ip_bin2 + ip_bin3 + ip_bin4;
            return ipBinConcat;
        }
        static bool isWithinCidrRange(string cidrRange, string ipAddress)
        {
            //processing CIDR range
            string cidrBinStr = ConvertIptoBin(cidrRange); // 32 bit

            //find the number of network bit
            int netBits = Convert.ToInt32(cidrRange.Substring(cidrRange.IndexOf('/') + 1));

            string cidrNetwork = cidrBinStr.Substring(0, netBits);
            string cidrHost = cidrBinStr.Substring(netBits);

            int intCidrNetwork = Convert.ToInt32(cidrNetwork, 2);
            int intCidrHost = Convert.ToInt32(cidrHost, 2);




            // Processing the IP Address            
            string binIPAddress = ConvertIptoBin(ipAddress);

            string ipNetwork = binIPAddress.Substring(0, netBits);
            string IpHost = binIPAddress.Substring(netBits);

            int intIpNetwork = Convert.ToInt32(ipNetwork, 2);
            int intIpHost = Convert.ToInt32(IpHost, 2);



            //checking if NetworkBits matches 
            if (cidrNetwork == ipNetwork)
            {
                return true;
            }
            return false;
        }
        public static void Main()
        {

            string cidrRange = "192.168.0.0/16";
            string ipAddress = "192.168.1.1";
            bool result = isWithinCidrRange(cidrRange, ipAddress);
            Console.WriteLine(result);
            Console.Read();
        }



    }
}
