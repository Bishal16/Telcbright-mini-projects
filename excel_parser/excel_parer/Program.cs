using System;
using System.IO;
using PortalApp;
using System.Collections.Generic;
using PortalApp._myCodes;
using OfficeOpenXml;
using excel_parser;

namespace excel_pasrer
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "(Agni )Genbend Swith & TB CAS  data mismatch report for 20 24 25 oct'23 (Recovered).xlsx";
            string filePath = fileName.Split('.')[0]+".txt";

            int skipFirst = 1;  // skip first n row
            List<DayWiseData> data = ExcelHelper.parseExcellRows(fileName, skipFirst);
            List<string> sqls1 = new List<string>();
            List<string> sqls2 = new List<string>();
            List<string> sqls3 = new List<string>();
            List<string> sqls4 = new List<string>();

            foreach (DayWiseData d in data)
            {
                string deleteSql1 = $@"delete from sum_voice_day_01 where tup_starttime = '{d.Date.ToString("yyyy-MM-dd")}' and tup_inpartnerid = {d.IncomingTG} and tup_outpartnerid = {d.OutGoingTG}  and tup_switchid = {d.SwitchId};";
                string deleteSql2 = $@"delete from sum_voice_day_02 where tup_starttime = '{d.Date.ToString("yyyy-MM-dd")}' and tup_inpartnerid = {d.IncomingTG} and tup_outpartnerid = {d.OutGoingTG}  and tup_switchid = {d.SwitchId};";
                string deleteSql3 = $@"delete from sum_voice_day_03 where tup_starttime = '{d.Date.ToString("yyyy-MM-dd")}' and tup_inpartnerid = {d.IncomingTG} and tup_outpartnerid = {d.OutGoingTG}  and tup_switchid = {d.SwitchId};";
                string deleteSql4 = $@"delete from sum_voice_day_04 where tup_starttime = '{d.Date.ToString("yyyy-MM-dd")}' and tup_inpartnerid = {d.IncomingTG} and tup_outpartnerid = {d.OutGoingTG}  and tup_switchid = {d.SwitchId};";


                sqls1.Add(deleteSql1);
                sqls2.Add(deleteSql2);
                sqls3.Add(deleteSql3);
                sqls4.Add(deleteSql4);
                
            }

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            
            //File.WriteAllLines(filePath, sqls1);
            File.AppendAllLines(filePath, sqls1);
            File.AppendAllLines(filePath, sqls2);
            File.AppendAllLines(filePath, sqls3);
            File.AppendAllLines(filePath, sqls4);

        }
    }
}
