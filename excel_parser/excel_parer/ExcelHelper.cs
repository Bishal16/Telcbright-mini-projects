using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.IO;
using excel_parser;

namespace excel_pasrer
{
    public class ExcelHelper
    {
        //public static List<DayWiseData> parseExcellRows(string fileLocation, int skipFirst)
        //{
        //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        //    List<DayWiseData> AllRows = new List<DayWiseData>();

        //    using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(fileLocation)))
        //    {
        //        var myWorksheet = xlPackage.Workbook.Worksheets.First();
        //        int rowCount = myWorksheet.Dimension.Rows;
        //        int columnCount = myWorksheet.Dimension.Columns;

        //        for (int rowNum = 1; rowNum <= rowCount; rowNum++)
        //        {
        //            string[] rowi = myWorksheet.Cells[rowNum, 1, rowNum, columnCount].Select(c => c.Value == null ? string.Empty : c.Value.ToString()).ToArray();
        //            if(rowi.Length < 8 || rowNum <= skipFirst)                    
        //                continue;

        //            DayWiseData data = new DayWiseData(rowi);
        //            AllRows.Add(data);
        //        }
        //    }
        //    return AllRows;
        //}


        public static List<TrafficData> ParseExcelFile(string fileLocation, int skipFirst, int multiplyby60)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            List<List<DataRow>> ListOfTypeWiseRows = new List<List<DataRow>>();

            List<TrafficData> traffics = new List<TrafficData>();

            using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(fileLocation)))
            {
                int sheetCount = xlPackage.Workbook.Worksheets.Count;
                for (int sheetIndex = 0; sheetIndex < sheetCount-1; sheetIndex++)
                {
                    var worksheet = xlPackage.Workbook.Worksheets[sheetIndex];
                    int rowCount = worksheet.Dimension.Rows;
                    int switchId = int.Parse(worksheet.Name.Split('_')[0]);
                    int columnCount = worksheet.Dimension.Columns;
                    List<DataRow> dataRows = new List<DataRow>();
                    for (int rowNum = 1; rowNum <= rowCount; rowNum++)
                    {
                       string[] strRow = worksheet.Cells[rowNum, 1, rowNum, columnCount]
                            .Select((c, ind) =>
                                            c.Value == null ? string.Empty
                                        : (ind == 0 ? c.Text : c.Value.ToString())).ToArray();
                        
                        if (strRow.Length < 5 || strRow[0] == "" || rowNum <= skipFirst)
                            continue;
                        DataRow dataRow = new DataRow(strRow,multiplyby60, switchId);
                        dataRows.Add(dataRow);
                    }
                    traffics.Add(new TrafficData(sheetIndex + 1, dataRows,multiplyby60));
                }
            }
            return traffics;
        }

    }
}