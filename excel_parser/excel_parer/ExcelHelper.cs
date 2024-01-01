using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.IO;
using excel_parser;

namespace PortalApp._myCodes
{
    public class ExcelHelper
    {
        public static List<DayWiseData> parseExcellRows(string fileLocation, int skipFirst)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            List<DayWiseData> AllRows = new List<DayWiseData>();

            using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(fileLocation)))
            {
                var myWorksheet = xlPackage.Workbook.Worksheets.First();
                int rowCount = myWorksheet.Dimension.Rows;
                int columnCount = myWorksheet.Dimension.Columns;

                for (int rowNum = 1; rowNum <= rowCount; rowNum++)
                {
                    string[] rowi = myWorksheet.Cells[rowNum, 1, rowNum, columnCount].Select(c => c.Value == null ? string.Empty : c.Value.ToString()).ToArray();
                    if(rowi.Length < 8 || rowNum <= skipFirst)                    
                        continue;
                    
                    DayWiseData data = new DayWiseData(rowi);
                    AllRows.Add(data);
                }
            }
            return AllRows;
        }
    }
}