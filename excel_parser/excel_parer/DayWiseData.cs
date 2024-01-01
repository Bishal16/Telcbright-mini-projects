using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalApp._myCodes;

namespace excel_parser
{
   public class DayWiseData
   {
        public DateTime Date { get; set; }
        public string IncomingANS{ get; set; }
        public int IncomingTG { get; set; }
        public string OutGoingANS{ get; set; }
        public int OutGoingTG { get; set; }
        public Int32 TotalCalls { get; set; }
        public Int32 SuccessfullCalls { get; set; }
        public Decimal ActualDutation { get; set; }
        public Decimal BilledDuration { get; set; }
        public Decimal ACD { get; set; }
        public int SwitchId { get; set; }

        public DayWiseData(string[] row)
        {            
            this.Date = DateTime.Parse(row[0].Trim(), CultureInfo.InvariantCulture);
            this.IncomingANS = row[1].Trim();
            this.IncomingTG =Convert.ToInt32(row[2]);
            this.OutGoingANS = row[3].Trim();
            this.OutGoingTG = Convert.ToInt32(row[4]);
            this.TotalCalls = Convert.ToInt32(row[5].Trim());
            this.SuccessfullCalls = Convert.ToInt32(row[6].Trim());
            this.ActualDutation = Convert.ToDecimal(row[7].Trim());
            this.BilledDuration = Convert.ToDecimal(row[8].Trim());
            this.ACD = Convert.ToDecimal(row[9].Trim());
            this.SwitchId = Convert.ToInt32(row[10].Trim());
        }
    }
}
