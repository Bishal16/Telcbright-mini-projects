using System;
using System.Collections.Generic;

namespace CIDR_range_check

{
    
    static class Program
    {
        //static Tuple<string, bool> makeTuple(string val, bool result)
        //{
        //    return new Tuple<string, bool>(val, result);
        //}
        

        static string ConvertIptoBin(string IP)
        {
            string[] IPArr;
            IPArr = IP.Split('.', '/');

            string ip_bin1 = Convert.ToString(Convert.ToInt32(IPArr[0]), 2).PadLeft(8, '0');
            string ip_bin2 = Convert.ToString(Convert.ToInt32(IPArr[1]), 2).PadLeft(8, '0');
            string ip_bin3 = Convert.ToString(Convert.ToInt32(IPArr[2]), 2).PadLeft(8, '0');
            string ip_bin4 = Convert.ToString(Convert.ToInt32(IPArr[3]), 2).PadLeft(8, '0');

            string IpBinConcat = ip_bin1 + ip_bin2 + ip_bin3 + ip_bin4;
            return IpBinConcat;
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
            string binIPAddress = ConvertIptoBin(cidrRange);

            string IpNetwork = binIPAddress.Substring(0, netBits);
            string IpHost = binIPAddress.Substring(netBits);

            int intIpNetwork = Convert.ToInt32(IpNetwork, 2);
            int intIpHost = Convert.ToInt32(IpHost, 2);



            //checking if NetworkBits matches 
            if (cidrNetwork == IpNetwork)
            {
                Console.WriteLine("----------- network bit matched-----------");

                //checking if ip host bit inside cidr range
                if (intIpHost > 0 && intIpHost < Math.Pow(2, 32 - netBits))
                {
                    Console.WriteLine("----------- host bit in the range -----------");
                    return true;
                }
                Console.WriteLine("----------- host bit outside the range -----------");
            }
            return false;
        }
        public static void Main()
        {





            foreach (var cidrResult in getTestData())
            {
                var range = cidrResult.cidr;
                var gptResult = cidrResult.result;
                var ipAddr = cidrResult.ipAddr;
                Console.WriteLine(isWithinCidrRange(range, ipAddr));
            }

            Console.Read();

            string cidrRange = "192.168.0.0/24";
            string ipAddress = "192.168.1.1";
            bool result = isWithinCidrRange(cidrRange, ipAddress);



        }


        static List<CidrResult> getTestData() {
            return new List<CidrResult>
            {
                {  new CidrResult("192.168.0.1",  "192.168.0.0/24", true) },
                {  new CidrResult("10.0.0.5",     "10.0.0.0/8", true) },
                {  new CidrResult("172.16.1.100", "172.16.0.0/16", true) },
                {  new CidrResult("10.10.10.10",  "10.0.0.0/8", true) },
                {  new CidrResult("192.168.10.50","192.168.10.0/24", true) },
                {  new CidrResult("172.16.1.1",   "172.16.0.0/16", true) },
                {  new CidrResult("10.1.1.1",     "10.0.0.0/8", true) },
                {  new CidrResult("192.168.1.1",  "192.168.0.0/24", true) },
                {  new CidrResult("172.16.0.5",   "172.16.0.0/16", true) },
                {  new CidrResult("10.0.1.1",     "10.0.0.0/8", true) },
                {  new CidrResult("10.0.0.5",     "172.16.0.0/16", false) },
                {  new CidrResult("10.10.10.10",  "172.16.0.0/16", false) },
                {  new CidrResult("192.168.10.50","10.0.0.0/8", false) },
                {  new CidrResult("192.168.1.1",  "10.0.0.0/8", false) },
                {  new CidrResult("172.16.0.5",   "192.168.0.0/24", false) },
            };

        }

    }
}
