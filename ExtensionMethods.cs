using DiplomMetod.Data.Entites;
using DiplomMetod.Models;

namespace DiplomMetod
{
    public static class ExtensionMethods
    {

        public static IEnumerable<FormExportModel> ToExportModel(this IEnumerable<Form> forms)
        {
            var exportModels = forms.Select(form => new FormExportModel
            {
                InventoryNumber = form.InventoryNumber,
                FormTypeName = form.FormType?.Name,
                RequisiteNumber = form.RequisiteNumber,
                Code = form.Code,
                ReferenceBookName = form.ReferenceBook?.Name,
                KeyWords = string.Join(", ", form.KeyWords.Select(kw => kw.Name)),
                ExplanationNumber = form.Explanation.Number,
                ExplanationDate = form.Explanation.Date,
                OrganizationName = form.Explanation.Organization.Name,
                IsAgreedGenProk = form.Explanation.IsAgreedGenProk ? "да" : "нет",
                ApproveLevel = form.Explanation.ApproveLevel.ToString(),
                IsRevelant = form.Explanation.IsRevelant ? "актуально" : "не актуально",
                RegionDivisionName = form.RegionsDivision?.Name,
                Comment = form.Explanation.Comment,
                IsFavorites = form.Explanation.IsFavorites ? "да" : "нет"
            });

            return exportModels;
        }
    }
}
