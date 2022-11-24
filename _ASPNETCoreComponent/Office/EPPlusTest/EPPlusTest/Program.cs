using OfficeOpenXml;
using System.IO;

var path = Path.Combine(AppContext.BaseDirectory, "test.xlsx");

//using var stream = File.OpenRead(path);
//using var package = new ExcelPackage(stream);
using var package = new ExcelPackage(new FileInfo(path));

var sheet = package.Workbook.Worksheets[0];
var rowCount = sheet.Dimension.Rows;

// 读取

for(var i = 1; i < rowCount; i++)
{
    Console.WriteLine($"1:{sheet.Cells[i, 1].Value}  2:{sheet.Cells[i, 2].Value}  3:{sheet.Cells[i, 3].Value}  ");
}

// 写入
sheet.Cells[1, 4].Value = "Column4";

package.Save();
