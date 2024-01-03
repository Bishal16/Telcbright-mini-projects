using System;
using System.IO;
//using PortalApp;
using System.Collections.Generic;
//using PortalApp._myCodes;
using OfficeOpenXml;
using excel_parser;
using System.Linq;
using System.Text;
namespace excel_pasrer
{
    public class TrafficData
    {
        int MultiplyBy60 { get; set; }
        public int Type { get; set; }
        public List<DataRow> DataRows { get; set; }
        public List<string> DeleteSqls { get; set; }
        public List<string> InsertSqls { get; set; }
        public TrafficData(int type, List<DataRow> dataRows, int multiplyBy60)
        {
            this.MultiplyBy60 = multiplyBy60;
            this.Type = type;
            this.DataRows = dataRows;
            this.GenerateScripts();
        }
        public string GetDeleteSql()
        {
            return string.Join("\r\n", DeleteSqls);
        }
        public string GetInsertSql()
        {
            return string.Join("\r\n", InsertSqls);
        }
        void GenerateScripts()
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
                this.DataRows.Select( (dr,index) =>
                {
                    Decimal percent = Convert.ToDecimal(0.99);
                    //string durationFldName = this.Type == 2 ? "duration3" : "duration1";
                    //    return $"INSERT INTO sum_voice_day_0{this.Type} ( id, tup_switchid, tup_inpartnerid, tup_outpartnerid, tup_incomingroute, tup_outgoingroute, tup_customerrate, tup_supplierrate, tup_starttime, totalcalls, connectedcalls,	connectedcallsCC, successfulcalls, actualduration, roundedduration, duration1, duration2, duration3, PDD, customercost, suppliercost, tax1, tax2, vat, intAmount1, intAmount2, longAmount1, longAmount2, longDecimalAmount1, longDecimalAmount2, intAmount3, longAmount3, longDecimalAmount3, decimalAmount1, decimalAmount2, decimalAmount3)" +
                    //                        $" VALUES ({-1 * (index + 1)}, 1, {dr.IncomingTG}, {dr.OutGoingTG}, (select max(routename) from route where idpartner = {dr.IncomingTG} and switchid = {dr.SwitchId}), (select max(routename) from route where idpartner= {dr.OutGoingTG} and switchid= {dr.SwitchId}), 0, 0, '{dr.Date.ToString("yyyy-MM-dd")}', {dr.TotalCalls}, 0, 0, {dr.SuccessfullCalls}, 0, {dr.BilledDuration}, {dr.BilledDuration}, 0, {dr.BilledDuration}, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);";
                    //}).ToList();

                    return $@"INSERT INTO sum_voice_day_0{this.Type} ( id, tup_switchid, tup_inpartnerid, tup_outpartnerid, tup_incomingroute, tup_outgoingroute, tup_customerrate, tup_supplierrate, tup_starttime, totalcalls, connectedcalls,	connectedcallsCC, successfulcalls, actualduration, roundedduration, duration1, duration2, duration3, PDD, customercost, suppliercost, tax1, tax2, vat, intAmount1, intAmount2, longAmount1, longAmount2, longDecimalAmount1, longDecimalAmount2, intAmount3, longAmount3, longDecimalAmount3, decimalAmount1, decimalAmount2, decimalAmount3)
                                        VALUES ({-1 * (index + 1)}, 1, {dr.IncomingTG}, {dr.OutGoingTG}, (select max(routename) from route where idpartner = {dr.IncomingTG} and switchid = {dr.SwitchId}), (select max(routename) from route where idpartner= {dr.OutGoingTG} and switchid= {dr.SwitchId}), 0, 0, '{dr.Date.ToString("yyyy-MM-dd")}', {dr.TotalCalls}, 0, 0, {dr.SuccessfullCalls}, {dr.BilledDuration * percent}, {dr.BilledDuration}, {dr.BilledDuration}, 0, {dr.BilledDuration}, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);";
                }).ToList();
        }
    }


    class Program
    {
        
        static void Main(string[] args)
        {

            Console.WriteLine("Press 1 if minutes need to be converted to seconds.");
            int multiplyBy60 = Console.Read()==49?1:0;
            string mainPath = "C:/Users/Mahathir/Documents/Visual Studio 2015/Projects/Telcbright-mini-projects/excel_parser/excel_parer/bin/Debug/New_folder";

            string[] directiries = Directory.GetDirectories(mainPath);


            foreach (string path in directiries)
            {
                string[] fileNames = Directory.GetFiles(path);
                foreach (string fileName in fileNames)
                {
                    //string fileName = "(Agni )Genbend Swith & TB CAS  data mismatch report for 20 24 25 oct'23 (Recovered).xlsx";
                    string targetFile = fileName + ".txt";

                    int skipFirst = 1;  // skip first n row
                    List<TrafficData> trafficDatas = ExcelHelper.ParseExcelFile(fileName, skipFirst,multiplyBy60);
                    if (!File.Exists(targetFile))
                    {
                        File.Create(targetFile).Close();
                    }

                    string headerForBkp = @"create table sum_voice_day_01bkp select * from sum_voice_day_01 ;
                                            create table sum_voice_day_02bkp select * from sum_voice_day_02 ;
                                            create table sum_voice_day_03bkp select * from sum_voice_day_03 ;
                                            create table sum_voice_day_04bkp select * from sum_voice_day_04 ;";
                    StringBuilder sb = new StringBuilder(headerForBkp);
                    foreach (TrafficData td in trafficDatas)
                    {
                        sb.Append(Environment.NewLine).Append(td.GetDeleteSql()).Append(Environment.NewLine)
                            .Append(td.GetInsertSql()).Append(Environment.NewLine);
                        var sum = td.DataRows.Sum(r => r.BilledDuration);
                        //File.WriteAllText(targetFile, sb.ToString());
                    }
                    File.WriteAllText(targetFile, sb.ToString());

                }
            }
        }
    }
}
