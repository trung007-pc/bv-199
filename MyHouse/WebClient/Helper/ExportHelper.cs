using System.Collections.Generic;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Volo.Abp;
using ObjectHelper = Core.Helper.ObjectHelper;

namespace WebClient.Helper
{
    public class ExportHelper
    {
          public static byte[] GeneratePostsExcelBytes<T>( List<T> rows, string sheetName = "Sheet1") where T : class
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var excel = new ExcelPackage();
            
            var workSheet = excel.Workbook.Worksheets.Add(sheetName);
            workSheet.DefaultRowHeight = 12;
            workSheet.Row(1).Height = 20;
            workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            workSheet.Row(1).Style.Font.Bold = true;

            workSheet.Cells.LoadFromCollection(rows, true);

            int i = 1;
            var headers = ObjectHelper.GetPropDescsOrNames<T>();
            foreach (var header in headers)
            {
                workSheet.Cells[1, i].Value = $"{header}";
                workSheet.Column(i).AutoFit();
                i++;
            }

            // Link
            workSheet.Column(3).AutoFit(0, 80);
            // Like Comment Share Total
            workSheet.Column(6).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            workSheet.Column(7).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            workSheet.Column(8).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            workSheet.Column(9).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

       

            var data = excel.GetAsByteArray();
            excel.Dispose();

            return data;
        }
    }
}