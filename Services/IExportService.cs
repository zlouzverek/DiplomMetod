
using DiplomMetod.Data.Entites;
using DiplomMetod.Data.Enums;
using DiplomMetod.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiplomMetod.Services
{
    public interface IFormExportService
    {
        FileContentResult Export(IEnumerable<FormExportModel> forms, FormExportType type = FormExportType.Excel);
    }
}
