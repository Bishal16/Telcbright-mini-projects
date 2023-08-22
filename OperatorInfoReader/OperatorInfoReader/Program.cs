using System.Collections.Generic;
using System.Linq;
using OfficeOpenXml;
using System.IO;


class Program
{
    static void Main(string[] args)
    {
        string fileLocation = "C:/Users/Mahathir/Documents/Visual Studio 2015/Projects/Telcbright-mini-projects/OperatorInfoReader/OperatorInfoReader/bin/Debug/Book1.xlsx";

        List<List<string>> AllRows = parseExcellRows(fileLocation);
        Dictionary<string, List<string>> operatorInfoDict = convertRowsToDict(AllRows);
        ;
    }

    static List<List<string>> parseExcellRows(string fileLocation)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;        
        List<List<string>> AllRows = new List<List<string>>();

        using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(fileLocation)))
        {
            var myWorksheet = xlPackage.Workbook.Worksheets.First();
            int rowCount = myWorksheet.Dimension.Columns;
            int columnCount = myWorksheet.Dimension.Columns;
           
            for (int rowNum = 1; rowNum <= rowCount; rowNum++)
            {
                List<string> rowi = myWorksheet.Cells[rowNum, 1, rowNum, columnCount].Select(c => c.Value == null ? string.Empty : c.Value.ToString()).ToList();
                AllRows.Add(rowi);
            }
        }
        return AllRows;
    }
    static Dictionary<string, List<string>> convertRowsToDict(List<List<string>>AllRows){
        Dictionary<string, List<string>> operatorInfo = new Dictionary<string, List<string>>();
        foreach(var r in AllRows)
        {
            operatorInfo[r.ToList()[0]] = r;
        }

        return operatorInfo;
    }
}


