using System;
using Microsoft.Office.Interop.Excel;

class Program
{
    static void Main(string[] args)
    {
        string excelFilePath = "C:/Users/Mahathir/Documents/Visual Studio 2015/Projects/Telcbright-mini-projects/excel file reder/excel file reder/bin/Debug/Day-To-Day_Expenses (2).xlsx"; // Replace with the actual path to your Excel file

        Application excelApp = new Application();
        Workbook workbook = excelApp.Workbooks.Open(excelFilePath);
        Worksheet worksheet = workbook.Sheets[1]; // Assuming you want to read from the first sheet

        // Find the used range of cells in the worksheet
        Range usedRange = worksheet.UsedRange;

        int rowCount = usedRange.Rows.Count;
        int columnCount = usedRange.Columns.Count;

        object[,] dataArray = (object[,])usedRange.Value2;

        // Convert the 2D array to a jagged array for easier handling
        object[][] rows = new object[rowCount][];
        for (int i = 0; i < rowCount; i++)
        {
            rows[i] = new object[columnCount];
            for (int j = 0; j < columnCount; j++)
            {
                rows[i][j] = dataArray[i + 1, j + 1];
            }
        }

        // Print the rows to the console
        int sum = 0, count = 0, x = 0;
            ;
        foreach (var row in rows)
        {
            //Console.WriteLine(string.Join("\t", row));
            if (Convert.ToString(row[1]).Contains("িম   িবল"))
            {
                string mbc = Convert.ToString(row[1]);
                mbc = mbc.Substring(mbc.Length - 2);
                sum = sum + Convert.ToInt32(row[3]);
                DateTime dt = DateTime.FromOADate(Convert.ToInt32(row[0]));
                Console.WriteLine("MB " + count + " " + mbc + "  Date:" + dt.Date + " = " + Convert.ToInt32(row[3]));
                count++;
                if (count == 32 && x == 0)
                {
                    count--; x++;
                }
            }
        }
        Console.WriteLine("Total = " + sum);

        // Close Excel application
        workbook.Close(false);
        excelApp.Quit();

        // Release COM objects
        System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
        System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
    }
}
