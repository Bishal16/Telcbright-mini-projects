using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using PortalApp._myCodes;

namespace excel_parser
{
   public class DataRow
   {
        public DateTime Date { get; set; }
        public string IncomingANS{ get; set; }
        public int IncomingTG { get; set; }
        public string OutGoingANS{ get; set; }
        public int OutGoingTG { get; set; }
        public Decimal TotalCalls { get; set; }
        public Decimal SuccessfullCalls { get; set; }
        public Decimal ActualDutation { get; set; }
        public Decimal BilledDuration { get; set; }
        public Decimal ACD { get; set; }
        public int SwitchId { get; set; }

        public DataRow(string[] row, int multiplyBy60, int switchId)
        {
            this.Date = parseDate(row[0]);
            this.IncomingANS = row[1].Trim();
            this.IncomingTG = Convert.ToInt32(row[2]);
            this.OutGoingANS = row[3].Trim();
            this.OutGoingTG = Convert.ToInt32(row[4]);
            this.TotalCalls = Convert.ToDecimal(row[5].Trim());
            this.SuccessfullCalls = Convert.ToDecimal(row[6].Trim());
            this.ActualDutation = Convert.ToDecimal(row[7].Trim());

            var billedDuration = Convert.ToDecimal(row[8].Trim());
            var duration = multiplyBy60 == 1 ? billedDuration * 60 : billedDuration;

            this.BilledDuration = duration;
            ///this.ACD = Convert.ToDecimal(row[9].Trim());
            this.SwitchId = switchId;
        }

        private DateTime parseDate(string dt)
        {
            //this.Date = DateTime.Parse(row[0].Trim(), CultureInfo.InvariantCulture);

            DateTime dateValue;
            var formatStrings = new string[] { "yyyy-MM-dd", "MMMM dd yyyy" };
            if (DateTime.TryParseExact(dt, formatStrings, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
                return dateValue;
            else
                throw new Exception("Invalid date format");
        }
    }
}
