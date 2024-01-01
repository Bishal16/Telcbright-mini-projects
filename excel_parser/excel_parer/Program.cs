using System;
using System.IO;
//using PortalApp;
using System.Collections.Generic;
//using PortalApp._myCodes;
using OfficeOpenXml;
using excel_parser;
using System.Linq;
namespace excel_pasrer
{
    public class TrafficData
    {
        public int Type { get; set; }
        public List<DataRow> DataRows { get; set; }
        public List<string> DeleteSqls { get; set; }
        public List<string> InsertSqls { get; set; }
        public void GenerateScripts()
        {
            this.DeleteSqls = createDelScript();
            this.InsertSqls = CreateInsertSqlsScripts();
        }
        List<string> createDelScript()
        {
            return
            this.DataRows.Select(dr =>
                $"delete from sum_voice_day_0{this.Type} where tup_starttime = '{dr.Date.ToString("yyyy-MM-dd")}' and tup_inpartnerid = {dr.IncomingTG} and tup_outpartnerid = {dr.OutGoingTG}  and tup_switchid = {dr.SwitchId};").ToList();
        }
        List<string> CreateInsertSqlsScripts()
        {
            return
                this.DataRows.Select(dr =>
                $@"INSERT INTO sum_voice_day_04 ( id, tup_switchid, tup_inpartnerid, tup_outpartnerid, tup_incomingroute, tup_outgoingroute, tup_customerrate, tup_supplierrate, tup_starttime, totalcalls, connectedcalls,	connectedcallsCC, successfulcalls, actualduration, roundedduration, duration1, duration2, duration3, PDD, customercost, suppliercost, tax1, tax2, vat, intAmount1, intAmount2, longAmount1, longAmount2, longDecimalAmount1, longDecimalAmount2, intAmount3, longAmount3, longDecimalAmount3, decimalAmount1, decimalAmount2, decimalAmount3)
                                        VALUES (-1, 1, 69, 24, (select max(routename) from route where idpartner = {dr.IncomingTG} and switchid = {dr.SwitchId}), (select max(routename) from route where idpartner= {dr.OutGoingTG} and switchid= {dr.SwitchId}), 0, 0, '{dr.Date.ToString("yyyy-MM-dd")}', 0, 0, 0, 0, 0, 0, {dr.BilledDuration}, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);").ToList();
        }
    }


    class Program
    {
        static void Main(string[] args)
        {

            string mainPath = "C:/Users/Mahathir/Documents/Visual Studio 2015/Projects/Telcbright-mini-projects/excel_parser/excel_parer/bin/Debug/New_folder";

            string[] directiries = Directory.GetDirectories(mainPath);
            foreach (string path in directiries)
            {
                string[] fileNames = Directory.GetFiles(path);
                foreach (string fileName in fileNames)
                {

                    //string fileName = "(Agni )Genbend Swith & TB CAS  data mismatch report for 20 24 25 oct'23 (Recovered).xlsx";
                    string filePath = fileName + ".txt";

                    int skipFirst = 1;  // skip first n row
                    List<TrafficData> trafficDatas = ExcelHelper.ParseExcelFile(fileName, skipFirst);
                    foreach (TrafficData td in trafficDatas)
                    {
                        td.GenerateScripts();
                    }
                    


                    if (!File.Exists(filePath))
                    {
                        File.Create(filePath).Close();
                    }
                    string del = $@"create table sum_voice_day_01bkp select * from sum_voice_day_01 ;
create table sum_voice_day_02bkp select * from sum_voice_day_02 ;
create table sum_voice_day_03bkp select * from sum_voice_day_03 ;
create table sum_voice_day_04bkp select * from sum_voice_day_04 ;
";
                    File.AppendAllText(filePath, del);
                    File.AppendAllLines(filePath, delsqls1);
                    File.AppendAllLines(filePath, delsqls2);
                    File.AppendAllLines(filePath, delsqls3);
                    File.AppendAllLines(filePath, delsqls4);

                    File.AppendAllLines(filePath, inssqls1);
                    File.AppendAllLines(filePath, inssqls2);
                    File.AppendAllLines(filePath, inssqls3);
                    File.AppendAllLines(filePath, inssqls4);
                }
            }
        }

       

        void createSqlPerSheetOrTrafficType(int trafficType, List<DataRow> dataRows)
        {
            List<string> delsqls1 = new List<string>();
            List<string> delsqls2 = new List<string>();
            List<string> delsqls3 = new List<string>();
            List<string> delsqls4 = new List<string>();

            List<string> inssqls1 = new List<string>();
            List<string> inssqls2 = new List<string>();
            List<string> inssqls3 = new List<string>();
            List<string> inssqls4 = new List<string>();

            foreach (DataRow d in dataRows)
            {
                string insertSql1 = $@"INSERT INTO sum_voice_day_04 ( id, tup_switchid, tup_inpartnerid, tup_outpartnerid, tup_incomingroute, tup_outgoingroute, tup_customerrate, tup_supplierrate, tup_starttime, totalcalls, connectedcalls,	connectedcallsCC, successfulcalls, actualduration, roundedduration, duration1, duration2, duration3, PDD, customercost, suppliercost, tax1, tax2, vat, intAmount1, intAmount2, longAmount1, longAmount2, longDecimalAmount1, longDecimalAmount2, intAmount3, longAmount3, longDecimalAmount3, decimalAmount1, decimalAmount2, decimalAmount3)
                                        VALUES (-1, 1, 69, 24, (select max(routename) from route where idpartner = {d.IncomingTG} and switchid = {d.SwitchId}), (select max(routename) from route where idpartner= {d.OutGoingTG} and switchid= {d.SwitchId}), 0, 0, '{d.Date.ToString("yyyy-MM-dd")}', 0, 0, 0, 0, 0, 0, {d.BilledDuration}, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);";
                string insertSql2 = $@"INSERT INTO sum_voice_day_04 ( id, tup_switchid, tup_inpartnerid, tup_outpartnerid, tup_incomingroute, tup_outgoingroute, tup_customerrate, tup_supplierrate, tup_starttime, totalcalls, connectedcalls,	connectedcallsCC, successfulcalls, actualduration, roundedduration, duration1, duration2, duration3, PDD, customercost, suppliercost, tax1, tax2, vat, intAmount1, intAmount2, longAmount1, longAmount2, longDecimalAmount1, longDecimalAmount2, intAmount3, longAmount3, longDecimalAmount3, decimalAmount1, decimalAmount2, decimalAmount3)
                                        VALUES (-1, 1, 69, 24, (select max(routename) from route where idpartner = {d.IncomingTG} and switchid = {d.SwitchId}), (select max(routename) from route where idpartner= {d.OutGoingTG} and switchid= {d.SwitchId}), 0, 0, '{d.Date.ToString("yyyy-MM-dd")}', 0, 0, 0, 0, 0, 0, {d.BilledDuration}, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);";
                string insertSql3 = $@"INSERT INTO sum_voice_day_04 ( id, tup_switchid, tup_inpartnerid, tup_outpartnerid, tup_incomingroute, tup_outgoingroute, tup_customerrate, tup_supplierrate, tup_starttime, totalcalls, connectedcalls,	connectedcallsCC, successfulcalls, actualduration, roundedduration, duration1, duration2, duration3, PDD, customercost, suppliercost, tax1, tax2, vat, intAmount1, intAmount2, longAmount1, longAmount2, longDecimalAmount1, longDecimalAmount2, intAmount3, longAmount3, longDecimalAmount3, decimalAmount1, decimalAmount2, decimalAmount3)
                                        VALUES (-1, 1, 69, 24, (select max(routename) from route where idpartner = {d.IncomingTG} and switchid = {d.SwitchId}), (select max(routename) from route where idpartner= {d.OutGoingTG} and switchid= {d.SwitchId}), 0, 0, '{d.Date.ToString("yyyy-MM-dd")}', 0, 0, 0, 0, 0, 0, {d.BilledDuration}, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);";
                string insertSql4 = $@"INSERT INTO sum_voice_day_04 ( id, tup_switchid, tup_inpartnerid, tup_outpartnerid, tup_incomingroute, tup_outgoingroute, tup_customerrate, tup_supplierrate, tup_starttime, totalcalls, connectedcalls,	connectedcallsCC, successfulcalls, actualduration, roundedduration, duration1, duration2, duration3, PDD, customercost, suppliercost, tax1, tax2, vat, intAmount1, intAmount2, longAmount1, longAmount2, longDecimalAmount1, longDecimalAmount2, intAmount3, longAmount3, longDecimalAmount3, decimalAmount1, decimalAmount2, decimalAmount3)
                                        VALUES (-1, 1, 69, 24, (select max(routename) from route where idpartner = {d.IncomingTG} and switchid = {d.SwitchId}), (select max(routename) from route where idpartner= {d.OutGoingTG} and switchid= {d.SwitchId}), 0, 0, '{d.Date.ToString("yyyy-MM-dd")}', 0, 0, 0, 0, 0, 0, {d.BilledDuration}, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);";

                delsqls1.Add(deleteSql1);
                delsqls2.Add(deleteSql2);
                delsqls3.Add(deleteSql3);
                delsqls4.Add(deleteSql4);

                inssqls1.Add(insertSql1);
                inssqls2.Add(insertSql2);
                inssqls3.Add(insertSql3);
                inssqls4.Add(insertSql4);

            }
        }

    }
}
