using System.ComponentModel;
using DiplomMetod.Data.Entites;
using DiplomMetod.Data.Enums;
using DiplomMetod.Models;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;


namespace DiplomMetod.Services
{
    public class FormExportService : IFormExportService
    {
        public FileContentResult Export(IEnumerable<FormExportModel> forms, FormExportType type = FormExportType.Excel)
        {

            return type switch
            {
                FormExportType.Excel => ToExcel(forms),
                FormExportType.Pdf => throw new NotImplementedException(),
                _ => throw new NotImplementedException()
            };

        }


        private static FileContentResult ToExcel(IEnumerable<FormExportModel> forms)
        {
            ExcelPackage.License.SetNonCommercialPersonal("test");

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Forms");

            // Получение заголовков
            var properties = typeof(FormExportModel).GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                var displayName = properties[i].GetCustomAttributes(typeof(DescriptionAttribute), true)
                               .Cast<DescriptionAttribute>()
                               .FirstOrDefault()?.Description ?? properties[i].Name;
                
                var cell = worksheet.Cells[1, i + 1];
                
                cell.Value = displayName;

                // жирный заголовок
                cell.Style.Font.Bold = true;
            }

            // Загрузка данных, начиная со второй строки
            worksheet.Cells[2, 1].LoadFromCollection(forms);


            var stream = new MemoryStream();
            package.SaveAs(stream);

            var content = stream.ToArray();
            return new FileContentResult(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = $"Forms-{DateTime.Now:yyyy-MM-dd}.xlsx"
            };
        }

    }
}
